using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Enum;
using Yun.Share.Voice.Utils;

namespace Yun.Share.Voice.IApplication.Dtos
{
    public class UserDto: BaseModelDto
    {
        /// <summary>
        /// ѧԱ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// �ֻ���
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public UserTypeEnum UserTypeEnum { get; set; }

        /// <summary>
        /// �˺�״̬
        /// </summary>
        public UserStatusTypeEnum UserStatusTypeEnum { get; set; }

        public string UserTypeEnumName => EnumHelper.GetDescription(UserTypeEnum);
        public string UserStatusTypeEnumName => EnumHelper.GetDescription(UserStatusTypeEnum);

        public DateTime StrTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string StrTimeName => StrTime.ToString("yyyy-MM-dd HH:mm:ss");

        public string EndTimeName => EndTime.HasValue ? EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "������Ч";

        public decimal? Price { get; set; }
        public string CreateRemarks { get; set; }

        public string CreateUserName { get; set; }
    }

    public class CreateUserDto
    {
        public virtual Guid? Id { get; set; }
        /// <summary>
        /// �ֻ���
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public UserTypeEnum? UserTypeEnum { get; set; }

        /// <summary>
        /// �˺�״̬
        /// </summary>
        public UserStatusTypeEnum? UserStatusTypeEnum { get; set; }

        public DateTime? EndTime { get; set; }

        /// <summary>
        /// ���˻�ʵ�ʽ��
        /// </summary>
        public decimal? Price { get; set; }
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

    public class LoginDto
    {
        public virtual string Token { get; set; }
        public virtual UserDto UserDto { get; set; }
    }


    
}
