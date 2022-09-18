using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Enum
{
    public enum FileTypeEnum
    {
        /// <summary>
        ///PDF
        /// </summary>
        [Description("PDF")]
        PDF = 10,
        /// <summary>
        ///图片
        /// </summary>
        [Description("图片")]
        Image = 20,
    }

    /// <summary>
    /// 共享类型
    /// </summary>
    public enum ShareTypeEnum
    {
        /// <summary>
        ///个人
        /// </summary>
        [Description("个人")]
        Self = 10,
        /// <summary>
        ///团队
        /// </summary>
        [Description("团队")]
        Group = 20,
    }
}
