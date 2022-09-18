using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.IApplication.Dtos
{
    public class BaseModelDto
    {
        public virtual Guid? Id { get;  set; }
        public virtual DateTime? CreationTime { get;  set; }
        public virtual Guid? CreatorId { get;  set; }
    }
}
