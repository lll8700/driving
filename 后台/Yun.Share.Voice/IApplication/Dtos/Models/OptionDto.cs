using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.IApplication.Dtos.Models
{
    public class OptionDto : BaseModelDto, ModelTitle, ModelPracticeId
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

        /// <summary>
        /// 是否正确
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        public virtual string Seq => string.IsNullOrEmpty(Title) ? "" : Title.Split(' ')[0];
    }
}
