using Microsoft.AspNetCore.Http;
using System.IO;
using Yun.Share.Voice.BaseInterface;
using Yun.Share.Voice.IApplication.Input;

namespace Yun.Share.Voice.IApplication
{
    public interface IAliyunOssStorageServer: IDependency
    {
        string UploadFile(IFormFile file);
        string PutObject(Stream fileStream, string fileName);

        /// <summary>
        /// 上传本地文件到oss
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        string PutObject(string filePath);

        void DeleteObject(string url);

        string UploadFile(PutOssFileInput input);
    }
}
