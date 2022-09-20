using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.IApplication.UtilDtos
{
    public class RequestData
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public virtual bool Success { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public virtual object Data { get; set; }

        /// <summary>
        /// 如果出错 这里将有信息
        /// </summary>
        public virtual string Message { get; set; }
    }

    public class TokenData
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public virtual Token data { get; set; }

        /// <summary>
        /// 如果出错 这里将有信息
        /// </summary>
        public virtual int code { get; set; }
    }
    public class Token
    {
        public virtual string token { get; set; }
    }
}
