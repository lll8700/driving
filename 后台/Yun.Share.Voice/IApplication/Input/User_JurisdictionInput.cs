using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Enum;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.IApplication.Input
{
    /// <summary>
    /// 用户权限表
    /// </summary>
    public class User_JurisdictionInput
    {
        public Guid UserId { get; set; }

        public UserStatusTypeEnum? UserStatusTypeEnum { get; set; }
        public List<Guid> Cars { get; set; }

        public List<Guid> SubjectTypes { get; set; }
    }
}
