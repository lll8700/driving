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

         /// <summary>
        /// 微信ID
        /// </summary>
        public virtual string OpenId { get; set; }

        public List<Guid> CarsIds { get; set; }

        public List<Guid> SubjectTypes { get; set; }

        public decimal? Price { get; set; }
    }
    public class TellPhonenumberInputDto
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public string EncryptedData { get; set; }
        public string Iv { get; set; }

    }
}
