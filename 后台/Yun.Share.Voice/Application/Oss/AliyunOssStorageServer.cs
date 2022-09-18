


using Aliyun.OSS;
using Aliyun.OSS.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.Utils;

namespace Yun.Share.Voice.Application.Oss
{
    public class AliyunOssStorageServer : IAliyunOssStorageServer
    {
        private readonly IConfiguration _configuration;
        private readonly PdfUtils pdfUtils;
        private OssData Oss;


        public AliyunOssStorageServer(
              IConfiguration configuration
            )
        {
            _configuration = configuration;
            pdfUtils = new PdfUtils();
            Oss = InitOssData();
        }

        public string PutObject(Stream fileStream, string fileName)
        {
            OssClient client = new OssClient(Oss.Endpoint, Oss.AccessKeyId , Oss.AccessKeySecret);
            var key = $"{Oss.TempDir}/{DateTime.Now:yyyyMMdd}/{Guid.NewGuid()}";
            int extentionIndex = fileName.LastIndexOf('.');
            if (extentionIndex > 0)
            {
                key += $"{fileName.Substring(extentionIndex)}";
            }
            var result = client.PutObject(Oss.BucketName, key, fileStream);
            //return $"{_bucketDomain}/{key}";
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return $"https://{Oss.BucketName}.{Oss.Endpoint}/{key}".ToLower();
            }
            else
            {
                throw new InvalidOperationException("上传文件到OSS发生错误！");
            }
        }

        /// <summary>
        /// 上传本地文件到oss
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string PutObject(string filePath)
        {
            OssClient client = new OssClient(Oss.Endpoint, Oss.AccessKeyId, Oss.AccessKeySecret);
            string fileKey = $"{Oss.TempDir}/{DateTime.Now:yyyyMMdd}/{Guid.NewGuid()}";
            int extentionIndex = filePath.LastIndexOf('.');
            if (extentionIndex > 0)
            {
                fileKey += $"{filePath.Substring(extentionIndex)}";
            }
            using (var fileStream = File.OpenRead(filePath))
            {
                var result = client.PutObject(Oss.BucketName, fileKey, fileStream);
                //return $"{_bucketDomain}/{key}";
                
                if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return $"https://{Oss.BucketName}.{Oss.Endpoint}/{fileKey}".ToLower();
                }
                else
                {
                    throw new InvalidOperationException("上传文件到OSS发生错误！");
                }
            }
        }

        public void DeleteObject(string url)
        {
            var key = url.Substring(url.IndexOf(Oss.Endpoint) + Oss.Endpoint.Length + 1);

            OssClient client = new OssClient(Oss.Endpoint, Oss.AccessKeyId, Oss.AccessKeySecret);
            client.DeleteObject(Oss.BucketName, key);
        }

        /// <summary>
        /// 负责将文件上传到oss，不负责保存到数据库
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isFromWX"></param>
        /// <returns></returns>
        public string UploadFile(IFormFile file)
        {
            if (file.Length > 1024 * 1024 * 100) //200MB.
            {
                throw new Exception("最大只允许上传100MB");
            }
            //pdfUtils.PdftoImages(file);
            //var fileName = "oss上传.jpg";
            //return PutObject(stream, fileName);
            var url = UploadToOSS(file);
           
            return url;   
        }

        /// <summary>
        /// oss模式
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="bytes"></param>
        /// <param name="g_entry"></param>
        private string UploadToOSS(IFormFile file)
        {
            OssClient client = new OssClient(Oss.Endpoint, Oss.AccessKeyId, Oss.AccessKeySecret);

            var fileInfo = new FileInfo(file.FileName);
            var ext = fileInfo.Extension.ToLower();
            var fileGuid = Guid.NewGuid();
            string ossFileName = $"{fileGuid}{ext}".ToLower();

            using (var stream = file.OpenReadStream())
            {
                var md5 = OssUtils.ComputeContentMd5(stream, stream.Length);
                var meta = new ObjectMetadata
                {
                    ContentDisposition = string.Format("attachment;filename*=utf-8''{0}", HttpUtils.EncodeUri(ossFileName, "utf-8")),
                    ContentType = file.ContentType,
                    ContentLength = stream.Length,
                    ContentMd5 = md5,
                };
                meta.UserMetadata.Add("fileName", Path.GetFileName(HttpUtils.EncodeUri(file.FileName, "utf-8")));
                meta.UserMetadata.Add("fileLength", file.Length.ToString());
                meta.UserMetadata.Add("orginFileName", HttpUtils.EncodeUri(file.FileName, "utf-8"));

                //string pattern_img = @"^(\s|\S)+(dwg|dxf|exb|jpg|png|jpeg|gif|dxf|bmp|tiff)+$";
                //string pattern_pdf = @"^(\s|\S)+(pdf)+$";
                //string pattern_compress = @"^(\s|\S)+(zip|gz|rar|tar)+$";
              
                var AliOssKey = $"{Oss.TempDir}/{DateTime.Now.ToString("yyyyMMdd")}/{fileGuid}{ext }";
                var request = new PutObjectRequest(Oss.BucketName, AliOssKey, stream, meta);
                var result = client.PutObject(request);
                if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return GetUrl(AliOssKey);
                }
                else
                {
                    throw new InvalidOperationException("上传文件到OSS发生错误！");
                }
            }
        }

        /// <summary>
        /// 根据本地文件地址路径上传到OSS并返回OSS路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string UploadFile(PutOssFileInput input)
        {
            OssClient client = new OssClient(Oss.Endpoint, Oss.AccessKeyId, Oss.AccessKeySecret);
            var fileGuid = Guid.NewGuid();
            var ext = Path.GetExtension(input.FileName).ToLower();
            var AliOssKey = $"{Oss.TempDir}/{DateTime.Now.ToString("yyyyMMdd")}/{fileGuid}{ext }";

            //using (var stream = new MemoryStream(input.FileStream))
            //{
            //    client.PutObject(Oss.BucketName, AliOssKey, stream);
            //}
            //DateTime expiration = DateTime.Now.AddYears(20);

            //var url = client.GeneratePresignedUri(Oss.BucketName, AliOssKey, expiration);

            //return GetUrl(AliOssKey);







            string ossFileName = $"{fileGuid}{ext}".ToLower();
            using (var stream = new MemoryStream(input.FileStream))
            {
                var md5 = OssUtils.ComputeContentMd5(stream, stream.Length);
                var meta = new ObjectMetadata
                {
                    ContentDisposition = string.Format("attachment;filename*=utf-8''{0}", HttpUtils.EncodeUri(ossFileName, "utf-8")),
                    ContentType = "image/jpeg", // 仅是上传图片
                    ContentLength = stream.Length,
                    ContentMd5 = md5,
                };
                meta.UserMetadata.Add("fileName", Path.GetFileName(HttpUtils.EncodeUri(input.FileName, "utf-8")));
                meta.UserMetadata.Add("fileLength", stream.Length.ToString());
                meta.UserMetadata.Add("orginFileName", HttpUtils.EncodeUri(input.FileName, "utf-8"));

                var request = new PutObjectRequest(Oss.BucketName, AliOssKey, stream, meta);
                try
                {
                    var result = client.PutObject(request);

                    if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return GetUrl(AliOssKey);
                    }
                    else
                    {
                        throw new InvalidOperationException("上传文件到OSS发生错误！");
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("上传文件到OSS发生错误！" + e.Message);
                }

            }
        }

    

        private string GetUrl(string aliKey)
        {
            return $"https://{Oss.BucketName}.{Oss.Endpoint}/{aliKey}".ToLower();
        }
        private OssData InitOssData()
        {
            return new OssData
            {
                Endpoint = _configuration["Authentication:Oss:Endpoint"],
                AccessKeySecret = _configuration["Authentication:Oss:AccessKeySecret"],
                TempDir = _configuration["Authentication:Oss:TempDir"],
                BucketName = _configuration["Authentication:Oss:BucketName"],
                AccessKeyId = _configuration["Authentication:Oss:AccessKeyId"]
            };
        }

        private class OssData
        {
            public string Endpoint  { get; set; } // _configuration["Authentication:Oss:Endpoint"];
            public string AccessKeySecret { get; set; } //  _configuration["Authentication:Oss:AccessKeySecret"];
            public string TempDir { get; set; } // = _configuration["Authentication:Oss:TempDir"];
            public string BucketName { get; set; } // = _configuration["Authentication:Oss:BucketName"];
            public string AccessKeyId { get; set; } // = _configuration["Authentication:Oss:AccessKeyId"];
        }

       

    }

}
