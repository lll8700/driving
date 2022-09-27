using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.IApplication.Dtos.Models
{
    public class CarTypeDto : BaseModelDto, ModelName
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 简写
        /// </summary>
        public string Subname { get; set; }
        /// <summary>
        /// 图形标签
        /// </summary>
        public string Icon { get; set; }
    }
}
