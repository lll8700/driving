using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.IApplication.Dtos.Models
{
    public class ErrorPracticeIdDto: BaseModelDto
    {
        public Guid ErrorPracticeLogId { get; set; }

        public Guid OptionId { get; set; }
        public OptionDto Option { get; set; }

        /// <summary>
        /// 是否正确
        /// </summary>
        public bool IsCorrect { get; set; }
    }
}
