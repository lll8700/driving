using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.IApplication.Input
{
    public class LoginInputDto
    {
        /// <summary>
        /// 联系方式
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }
    }
    
}
