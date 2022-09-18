using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.IApplication.Dtos
{
    public class UserDto
    {
        public virtual Guid? Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string AvatarUrl { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string Token { get; set; }

        public virtual string SessionKey { get; set; }

        public virtual string Messge { get; set; }
    }

    public class FileGroupTableDto
    {
        public virtual Guid Id { get; set; }
        public virtual string Lable { get; set; }
    }

    public class UserFileGroupItemDto
    {
        public virtual Guid Id { get; set; }
        public virtual string Lable { get; set; }

        public virtual string CreateTimeStr { get; set; }

        public virtual int FileCount { get; set; }
    }

    public class WechatDto
    {
        public virtual string SessionKey { get; set; }

        public virtual string OpenId { get; set; }
    }
}
