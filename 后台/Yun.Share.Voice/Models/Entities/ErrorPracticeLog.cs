using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.Models.Entities
{
    /// <summary>
    /// 错题库
    /// </summary>
    public class ErrorPracticeLog : BaseModel, ModelPracticeId
    {

        public virtual Guid PracticeId { get; set; }

        public virtual Practice Practice { get; set; }

    }
}
