using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Enum;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;
using Yun.Share.Voice.Utils;

namespace Yun.Share.Voice.IApplication.Dtos
{
    /// <summary>
    /// 用户权限表
    /// </summary>
    public class User_JurisdictionDto : BaseModel, UserId
    {
        public Guid UserId { get; set; }

        public Guid TableId { get; set; }

        public User_JurisdictionTypeEnum User_JurisdictionTypeEnum { get; set; }

        public string User_JurisdictionTypeEnumName => EnumHelper.GetDescription(User_JurisdictionTypeEnum);
    }
}
