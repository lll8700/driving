using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Enum
{
    /// <summary>
    /// 用户题库权限
    /// </summary>
    public enum User_JurisdictionTypeEnum
    {

        /// <summary>
        ///10车型
        /// </summary>
        [Description("车型")]
        Car = 10,

        /// <summary>
        ///20科目
        /// </summary>
        [Description("科目")]
        SubjectType = 20,

    }
}
