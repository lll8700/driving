using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Yun.Share.Voice.Enum
{
    /// <summary>
    /// 文件类型
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// 普通文件
        /// </summary>
        [Description("普通文件")]
        Regular = 0,

        /// <summary>
        /// 压缩文件
        /// </summary>
        [Description("压缩文件")]
        Compressed = 1,

        /// <summary>
        /// 图片文件
        /// </summary>
        [Description("图片")]

        Images = 2,
        /// <summary>
        /// Pdf文件
        /// </summary>
        [Description("Pdf文件")]
        Pdf = 3,
        /// <summary>
        /// Excel文件
        /// </summary>
        [Description("Excel文件")]
        Excel = 4
    }
}
