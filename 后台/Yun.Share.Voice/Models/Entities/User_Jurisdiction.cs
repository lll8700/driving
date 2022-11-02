using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Enum;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.Models.Entities
{
    /// <summary>
    /// 用户权限表
    /// </summary>
    public class User_Jurisdiction : BaseModel,UserId
    {
        public Guid UserId { get; set; }

        public Guid TableId { get; set; }

        public User_JurisdictionTypeEnum User_JurisdictionTypeEnum { get; set; }
    }
}
