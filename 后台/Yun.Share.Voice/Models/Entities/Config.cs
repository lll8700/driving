using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.Models.Entities
{
    /// <summary>
    /// 基础配置
    /// </summary>
    public class Config : BaseModel
    {
        /// <summary>
        ///  开账户基础金额
        /// </summary>
        public decimal? CreateUserBasePrice { get; set; }

        
        public DateTime? StrTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
