
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using Yun.Share.Voice.IApplication.Input;

namespace Yun.Share.Voice.Utils
{
    public class PdfUtils
    {

        /// <summary>
        /// 根据上传的Pdf文件转换成一张图的流
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<Stream> GetImageStreamByPDFChange(IFormFile file)
        {
            List<Stream> list = new List<Stream>();
            //pdfPath = @"D:\杂项\趣同屏产品思维导图.pdf";
            //string imagePath = @"D:\杂项\";

            MagickReadSettings settings = new MagickReadSettings();
            settings.Density = new Density(300, 300); //设置质量
            using (MagickImageCollection images = new MagickImageCollection())
            {
                Stream stream = new MemoryStream();
                try
                {
                    images.Read(file.OpenReadStream(), settings);
                    for (int i = 0; i < images.Count; i++)
                    {
                        MagickImage image = (MagickImage)images[i];
                        image.Write(stream);
                        list.Add(stream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据上传的Pdf文件转换成一张图的流
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<OssImageDto> GetImageStreamByPDFChange(string filePath)
        {
            List<OssImageDto> list = new List<OssImageDto>();
            //pdfPath = @"D:\杂项\趣同屏产品思维导图.pdf";
            //string imagePath = @"D:\杂项\";

            var strem = GetUrlMemoryStream(filePath);

            MagickReadSettings settings = new MagickReadSettings();
            settings.Density = new Density(300, 300); //设置质量
            using (MagickImageCollection images = new MagickImageCollection())
            {
                images.Read(strem, settings);
                for (int i = 0; i < images.Count; i++)
                {
                    OssImageDto ossImage = new OssImageDto();
                    MemoryStream stream = new MemoryStream();
                    MagickImage image = (MagickImage)images[i];
                    image.Write(stream, MagickFormat.Jpg);
                    ossImage.Index = i;
                    ossImage.FileStream = stream.ToArray();
                    list.Add(ossImage);
                    stream.Close();
                }

            }
            return list;
        }

        /// <summary>
        /// 根据上传的Pdf文件转换成一张图的流
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<OssImageDto> GetImageStreamByPDFChangeDll(string filePath)
        {
            List<OssImageDto> list = new List<OssImageDto>();
            //pdfPath = @"D:\杂项\趣同屏产品思维导图.pdf";
            //string imagePath = @"D:\杂项\";

            int desired_x_dpi = 300;
            int desired_y_dpi = 300;

            //本地安装版本代码
            //var _lastInstalledVersion =
            //      GhostscriptVersionInfo.GetLastInstalledVersion(
            //              GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
            //              GhostscriptLicense.GPL);

            //拷贝到项目版本
            var _lastInstalledVersion = new GhostscriptVersionInfo($"{System.Environment.CurrentDirectory}\\gsdll64.dll");

            var _rasterizer = new GhostscriptRasterizer();


            var strem = GetUrlMemoryStream(filePath);

            _rasterizer.Open(strem, _lastInstalledVersion, false);

            for (int pageNumber = 0; pageNumber < _rasterizer.PageCount; pageNumber++)
            {
                Image img = _rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber + 1);
                OssImageDto ossImage = new OssImageDto();
                MemoryStream stream = new MemoryStream();

                img.Save(stream, ImageFormat.Jpeg);

                ossImage.Index = pageNumber;
                ossImage.FileStream = stream.ToArray();

                list.Add(ossImage);
                stream.Close();
            }
            _rasterizer.Close();

            return list;

        }
        
        /// <summary>
        /// 获取远程服务器内容，并转换成流
        /// </summary>
        /// <param name="path">http://localhost:51573</param>
        /// <returns></returns>
        private MemoryStream GetUrlMemoryStream(string path)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            List<byte> btlst = new List<byte>();
            int b = responseStream.ReadByte();
            while (b > -1)
            {
                btlst.Add((byte)b);
                b = responseStream.ReadByte();
            }
            byte[] bts = btlst.ToArray();
            var ms = new MemoryStream();
            ms.Seek(0, SeekOrigin.Begin);
            ms.Write(bts);

            return ms;
        }
    }
}
