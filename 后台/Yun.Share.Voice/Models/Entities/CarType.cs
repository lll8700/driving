﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.Models.Entities
{
    /// <summary>
    /// 车类型
    /// </summary>
    public class CarType : BaseModel, ModelName
    {
        public string Name { get ; set ; }
    }
}
