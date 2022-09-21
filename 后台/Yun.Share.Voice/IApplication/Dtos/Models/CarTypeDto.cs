using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.IApplication.Dtos.Models
{
    public class CarTypeDto : BaseModelDto, ModelName
    {
        public string Name { get; set; }
    }
}
