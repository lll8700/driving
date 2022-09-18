using System;
using System.Collections.Generic;
using System.Text;

namespace Yun.Share.Voice.IApplication.UtilDtos
{
    public class AppOssSettings
    {
        /// <summary>
        /// <yourEndpoint>
        /// </summary>
        public string endpoint { get; set; } = "oss-cn-shanghai.aliyuncs.com";

        /// <summary>
        /// <yourAccessKeyId>
        /// </summary>
        public string accessKeyId { get; set; } = "LTAI5tBZaSfayVxyHUozMgnL";

        /// <summary>
        /// <yourAccessKeySecret>
        /// </summary>
        public string accessKeySecret { get; set; } = "sNmQg9mcrAz9vharcTs1h8IILhks3i";
        /// <summary>
        /// Oss储存桶名称
        /// </summary>
        public string bucketName { get; set; } = "17-oss-prd";
        /// <summary>
        /// 桶下的一级文件夹
        /// </summary>
        public string tempDir { get; set; } = "synicview";

    }
}
