using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Enum
{
    public enum UserGroupStatusTypeEnum
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]

        Wait = 10,

        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]

        Succeed = 20,

        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]

        Error = 30,

    }

    public enum StatusTypeEnum
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]

        Wait = 10,

        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]

        Succeed = 20,

        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]

        Error = 30,

    }
}
