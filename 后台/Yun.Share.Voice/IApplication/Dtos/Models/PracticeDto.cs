using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Enum;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Entities;
using Yun.Share.Voice.Models.Interface;
using Yun.Share.Voice.Utils;

namespace Yun.Share.Voice.IApplication.Dtos.Models
{
    public class PracticeDto : BaseModelDto, ModelTitle
    {
        public string Title { get; set; }
        /// <summary>
        /// 科类Id
        /// </summary>
        public Guid SubjectTypeId { get; set; }

        public SubjectTypeDto SubjectType { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public Guid CarTypeId { get; set; }

        public CarTypeDto CarType { get; set; }

        /// <summary>
        /// 题库类型
        /// </summary>
        public Guid? PracticeTypeId { get; set; }

        public PracticeTypeDto PracticeType { get; set; }

        /// <summary>
        /// 题目状态
        /// </summary>
        public StatusTypeEnum StatusTypeEnum { get; set; }

        public List<PracticeImageDto> PracticeImages { get; set; }

        public List<OptionDto> Options { get; set; }
        /// <summary>
        /// 关键字 ，分隔
        /// </summary> 
        public string KeyWordls { get; set; }

        /// <summary>
        /// 答题技巧
        /// </summary>
        public string Skill { get; set; }

        /// <summary>
        /// 答题技巧2
        /// </summary>
        public string SkillLast { get; set; }

        /// <summary>
        /// 试题详解
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 单选/多选
        /// </summary>
        public ChoiceTyope ChoiceTyope { get; set; }

        public string ChoiceTyopeEnmName => EnumHelper.GetDescription(ChoiceTyope);

        /// <summary>
        /// 图片集
        /// </summary>
        public string ImageSrc { get; set; }
    }

    public class ExcelData
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
    }
}
