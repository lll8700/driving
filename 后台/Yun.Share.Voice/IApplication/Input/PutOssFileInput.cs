using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.IApplication.Input
{
    public class PutOssFileInput
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件流
        /// </summary>
        public byte[] FileStream { get; set; }
    }

    public class OssImageDto
    {
        public virtual int Index { get; set; }

        public byte[] FileStream { get; set; }
    }

    public class UploadFileOutDto
    {
        public Guid Id { get; internal set; }

        public string Name { get; internal set; }

        public string Ext { get; internal set; }

        public long Size { get; internal set; }

        public string AliOssKey { get; internal set; }

        public string Url { get; internal set; }

        public DateTime CreationTime { get; internal set; }

        public string UserName { get; internal set; }

        public string ContentType { get; internal set; }


    }
}
