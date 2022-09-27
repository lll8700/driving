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
    /// 用户表
    /// </summary>
    public class User : BaseModel, ModelName
    {
        /// <summary>
        /// 学员名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 微信Id
        /// </summary>
        public string WeChatId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 身份类型
        /// </summary>
        public UserTypeEnum UserTypeEnum { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public UserStatusTypeEnum UserStatusTypeEnum { get; set; }


        public DateTime StrTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
