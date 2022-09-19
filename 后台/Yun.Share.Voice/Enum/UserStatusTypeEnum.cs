using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Enum
{
    public enum UserStatusTypeEnum
    {

        /// <summary>
        ///10试用期
        /// </summary>
        [Description("试用")]
        Apply = 10,

        /// <summary>
        ///20VIP
        /// </summary>
        [Description("VIP")]
        VIP = 20,

        /// <summary>
        ///30失效
        /// </summary>
        [Description("失效")]
        Invalid = 30,
    }

    public enum UserTypeEnum
    {

        /// <summary>
        ///普通会员
        /// </summary>
        [Description("普通会员")]
        Empty = 0,

        /// <summary>
        ///管理员
        /// </summary>
        [Description("管理员")]
        Admin = 10,


        /// <summary>
        ///其他
        /// </summary>
        [Description("其他")]
        Other = 20,
    }

    /// <summary>
    /// 选择类型
    /// </summary>
    public enum ChoiceTyope
    {
        /// <summary>
        ///单选
        /// </summary>
        [Description("单选")]
        Single = 10,

        /// <summary>
        ///多选
        /// </summary>
        [Description("多选")]
        More = 20
    }
}
