using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Enum
{
    public enum ScreenStatusTypeNum
    {
        /// <summary>
        /// 等待
        /// </summary>
        [Description("等待")]
        Wait = 0,

        /// <summary>
        /// 同屏中
        /// </summary>
        [Description("同屏中")]
        Doing = 10,

        /// <summary>
        /// 同屏结束
        /// </summary>
        [Description("同屏结束")]
        End = 20,
    }
}
