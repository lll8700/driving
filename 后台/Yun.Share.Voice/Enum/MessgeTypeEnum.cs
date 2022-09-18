using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Enum
{
    public enum MessgeTypeEnum
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        [Description("系统消息")]
        System = 10,

        /// <summary>
        /// 人员通知
        /// </summary>
        [Description("人员通知")]
        User = 20,
    }
}
