using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Yun.Share.Voice.Enum
{
    /// <summary>
    /// 文件类型
    /// </summary>
    public enum PayOrderStatusTypeEnum
    {
        /// <summary>
        /// 待支付
        /// </summary>
        [Description("待支付")]
        Wait = 10,

        /// <summary>
        /// 支付成功
        /// </summary>
        [Description("支付成功")]
        Succeed = 20,

        /// <summary>
        /// 支付失败
        /// </summary>
        [Description("支付失败")]
        Error = 30,
    }
}
