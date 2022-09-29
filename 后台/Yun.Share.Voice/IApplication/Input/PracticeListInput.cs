using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Enum;

namespace Yun.Share.Voice.IApplication.Input
{
    public class PracticeListInput: ListInputBaseDto
    {
        /// <summary>
        /// 科类Id
        /// </summary>
        public Guid? SubjectTypeId { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public Guid? CarTypeId { get; set; }

        /// <summary>
        /// 查询ID
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 题目状态
        /// </summary>
        public StatusTypeEnum? StatusTypeEnum { get; set; }


        /// <summary>
        /// 关键字 ，分隔
        /// </summary>
        public string KeyWordls { get; set; }

        /// <summary>
        /// 单选/多选
        /// </summary>
        public ChoiceTyope? ChoiceTyope { get; set; }
    }
}
