using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.Models.Entities
{
    /// <summary>
    /// 题目选项
    /// </summary>
    public class Option : BaseModel, ModelTitle, ModelPracticeId
    {
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Index { get; set; }
        /// <summary>
        /// 选项题目
        /// </summary>
        public virtual string Title { get; set; }

        public virtual Guid PracticeId { get; set; }

        public virtual Practice Practice { get; set; }

        /// <summary>
        /// 是否正确
        /// </summary>
        public bool IsCorrect { get; set; }
    }
}
