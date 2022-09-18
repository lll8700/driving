using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Enum
{
    /// <summary>
    /// 同屏参与人类型
    /// </summary>
    public enum ScreenUserTypeEnum
    {

        /// <summary>
        ///10发起者
        /// </summary>
        [Description("发起者")]
        Send = 10,

        /// <summary>
        ///10接入者
        /// </summary>
        [Description("接入者")]
        Manage = 20,

        /// <summary>
        ///30第三方参与者
        /// </summary>
        [Description("参与者")]
        Apply = 30,
    }
}
