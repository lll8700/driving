using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.Models.Entities
{
    /// <summary>
    /// 车类型
    /// </summary>
    public class CarType : BaseModel, ModelName
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get ; set ; }
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
