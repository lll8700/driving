using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Base;

namespace Yun.Share.Voice.Models.Entities
{
    public class ErrorPracticeId : BaseModel
    {
        public Guid ErrorPracticeLogId { get; set; }
        public ErrorPracticeLog ErrorPracticeLog { get; set; }

        public Guid OptionId { get; set; }
        public Option Option { get; set; }

        /// <summary>
        /// 是否正确
        /// </summary>
        public bool IsCorrect { get; set; }
    }
}
