using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.Models.Entities
{
    /// <summary>
    /// 题目图片
    /// </summary>
    public class PracticeImage : BaseModel, ModelPracticeId
    {
        public Guid PracticeId { get; set; }

        public Practice Practice { get; set; }

        public string Url { get; set; }
    }
}
