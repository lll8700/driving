using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Enum;

namespace Yun.Share.Voice.IApplication.Input
{
    public class UserListInput: ListInputBaseDto
    {
        /// <summary>
        /// 身份类型
        /// </summary>
        public UserTypeEnum? UserTypeEnum { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public UserStatusTypeEnum? UserStatusTypeEnum { get; set; }

        public bool? IsSelfCreate { get; set; }

        public DateTime? StrTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
