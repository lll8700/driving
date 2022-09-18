using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Enum;

namespace Yun.Share.Voice.IApplication.Input
{
    public class AuthorizeUserInput
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 微信头像
        /// </summary>
        public virtual string AvatarUrl { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        public virtual string WeChatOpenId { get; set; }
        
        /// <summary>
        /// 联系方式
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 获取OpenId的Code
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 他的经理Id（是他把我拉新过来的）
        /// </summary>
        public virtual Guid? ManageUserId { get; set; }

    }

    public class BackAuthorizeUserInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }
    }

    public class PhoneNumberInput
    {
        public virtual string EncryptedData { get; set; }

        public virtual string IV { get; set; }

        public virtual string SessionKey { get; set; }
    }

}
