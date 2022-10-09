using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Enum;

namespace Yun.Share.Voice.IApplication.Input
{
    public class ErrorPracticeListInput : ListInputBaseDto
    {
        /// <summary>
        /// 科类Id
        /// </summary>
        public Guid? PracticeId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public Guid? UserId { get; set; }

    }
}
