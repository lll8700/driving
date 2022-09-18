using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Models.Base
{
    /// <summary>
    /// 实体对象基础类  必须继承
    /// </summary>
    public class BaseModel
    {
        [Key]
        public virtual Guid Id { get; protected set; }
        public virtual DateTime CreationTime { get; protected set; }
        public virtual Guid? CreatorId { get; protected set; }
        public virtual DateTime? LastModificationTime { get; protected set; }
        public virtual Guid? LastModifierId { get; protected set; }

        public virtual DateTime? DeletedTime { get; protected set; }
        public virtual bool IsDeleted { get; protected set; }
        public virtual Guid? DeletedId { get; protected set; }
    }
}
