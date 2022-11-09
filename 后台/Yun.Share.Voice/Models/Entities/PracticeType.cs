using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.Models.Entities
{
    /// <summary>
    /// 题库类型
    /// </summary>
    public class PracticeType : BaseModel, ModelName
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get ; set ; }
    }
}
