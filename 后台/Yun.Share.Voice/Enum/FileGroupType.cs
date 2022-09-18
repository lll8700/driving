using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Yun.Share.Voice.Enum
{
    public enum FileGroupType
    {
        /// <summary>
        /// 计划书
        /// </summary>
        [Description("计划书")]
        Plan = 10,

        /// <summary>
        /// 压缩文件
        /// </summary>
        [Description("培训资料")]
        Study = 20,

        /// <summary>
        /// 图片文件
        /// </summary>
        [Description("其他")]

        Other = 30
    }
}
