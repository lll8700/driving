using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.IApplication.UtilDtos
{
    public interface TSearchDto
    {
        string Sorting { get; set; }
        int? SkipCount { get; set; } 
        int? MaxResultCount { get; set; }

        int? Page { get; set; }

        int? Limit { get; set; }
    }
}
