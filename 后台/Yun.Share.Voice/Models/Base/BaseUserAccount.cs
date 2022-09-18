using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Models.Base
{
    /// <summary>
    /// 基础账号数据
    /// </summary>
    public interface BaseUserAccount
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
