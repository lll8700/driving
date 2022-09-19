using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Enum;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.Models.Entities
{
    /// <summary>
    /// 题库
    /// </summary>
    public class Practice : BaseModel, ModelTitle
    {
        public string Title { get; set; }
        /// <summary>
        /// 科类Id
        /// </summary>
        public Guid SubjectTypeId { get; set; }

        public SubjectType SubjectType  { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public Guid CarTypeId { get; set; }

        public CarType CarType { get; set; }

        /// <summary>
        /// 题目状态
        /// </summary>
        public StatusTypeEnum StatusTypeEnum { get; set; }

        public List<PracticeImage> PracticeImageslist { get; set; }

        /// <summary>
        /// 关键字 ，分隔
        /// </summary>
        public string KeyWordls { get; set; }

        /// <summary>
        /// 答题技巧
        /// </summary>
        public string Skill { get; set; }
        
        /// <summary>
        /// 单选/多选
        /// </summary>
        public ChoiceTyope ChoiceTyope { get; set; }
    }
}
