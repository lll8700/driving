using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.IApplication.Dtos.Models
{
    public class PracticeImageDto : BaseModelDto, ModelPracticeId
    {
        public Guid PracticeId { get; set; }

        public PracticeDto Practice { get; set; }

        public string Url { get; set; }
    }
}
