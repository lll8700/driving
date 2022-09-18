using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Enum
{
    public enum DateTypeEnum
    {
        /// <summary>
        /// 年
        /// </summary>
        [Description("年")]
        Year = 10,

        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        Months = 20,
        /// <summary>
        /// 周
        /// </summary>
        [Description("周")]
        Week =30,

        /// <summary>
        /// 天
        /// </summary>
        [Description("天")]
        Day = 40,
    }
}
