using Microsoft.AspNetCore.Http;
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
        /// 题库
        /// </summary>
        public Guid? PracticeTypeId { get; set; }
        
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

    public class PracticeTestListInput
    {
        /// <summary>
        /// 判断题数量
        /// </summary>
        public int ChoiceCount { get; set; } = 40;
        /// <summary>
        /// 单选数量
        /// </summary>
        public int SingleCount { get; set; } = 0;

        /// <summary>
        /// 多选数量
        /// </summary>
        public int MoreCount { get; set; } = 0;

        /// <summary>
        /// 选择题
        /// </summary>
        public int UnChoiceCount { get; set; } = 60;
    }

    
    public class PracticeFileInput
    {

        /// <summary>
        /// 科类Id
        /// </summary>
        public Guid SubjectTypeId { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public Guid CarTypeId { get; set; }

        public List<ResultsList> ResultsLists { get; set; }

    }

    public class ResultsList
    {

        public string Images { get; set; } // "11.jpg,22.png"
        public string Introduce { get; set; } // "《机动车驾驶证申领和使用规定》规定：机动车驾驶人在实习期内不得驾驶公共汽车、营运客车或者执行任务的警车、消防车、救护车、工程救险车以及载有爆炸物品、易燃易爆化学物品、剧毒或者放射性等危险物品的机动车；驾驶的机动车不得牵引挂车。"

        public string Skill { get; set; } // "实习期禁止牵引挂车。"
        public string SkillLast { get; set; } // "题干说实习期不得牵引挂车，所以选对。"
        public string Title { get; set; } // "机动车驾驶人在实习期内驾驶机动车不得牵引挂车。"
        public string TypeName { get; set; } // "判断/新规题"

        public List<OptionItem> Options { get; set; }
    }
    public class OptionItem
    {
        public bool IsCorrect { get; set; }
        public string Title{ get; set; }
        public int Index{ get; set; }
    }
}
