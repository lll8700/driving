using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.Models.Interface;

namespace Yun.Share.Voice.IApplication.Dtos.Models
{
    public class ConfigDto : BaseModelDto
    {
        /// <summary>
        ///  开账户基础金额
        /// </summary>
        public decimal? CreateUserBasePrice { get; set; }


        public DateTime? StrTime { get; set; }

        public DateTime? EndTime { get; set; }
    }

    public class HomeUser_JurisdictionListDto
    {
        public List<CarTypeDto> CarTypeDtos { get; set; }

        public List<SubjectTypeDto> SubjectTypeDtos { get; set; }
    }
}
