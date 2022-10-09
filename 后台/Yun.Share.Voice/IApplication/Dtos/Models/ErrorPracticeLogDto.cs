using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.IApplication.Dtos.Models
{
    public class ErrorPracticeLogDto : BaseModelDto, ModelPracticeId, UserId
    {
        public virtual Guid UserId { get; set; }

        public virtual Guid PracticeId { get; set; }

        public virtual PracticeDto Practice { get; set; }

        public virtual List<ErrorPracticeIdDto> ErrorPracticeIdDtos { get; set; }
    }

    public class CreateErrorPracticeLogDto : BaseModelDto, ModelPracticeId
    {
        public virtual Guid PracticeId { get; set; }

        public virtual List<IsOption> OptionIds { get; set; }
    }

    public class IsOption
    {
        public virtual Guid OptionId { get; set; }

        /// <summary>
        /// 是否正确
        /// </summary>
        public virtual bool IsCorrect { get; set; }
    }
}
