using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.IApplication.UtilDtos
{
    public class JwtAuthorizationDto
    {
        public string Token { get; set; }
        public bool Success { get; set; }
    }
    public class JwtAuthorizationTokenInput
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string Name { get; set; }
        /// 联系方式
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual Guid UserId { get; set; }
    }
    public class CurrentUserDto
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string Name { get; set; }
        /// 联系方式
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual Guid UserId { get; set; }
    }
}
