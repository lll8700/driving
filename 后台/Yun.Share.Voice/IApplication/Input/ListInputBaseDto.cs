using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.IApplication.Input
{
    public class ListInputBaseDto: TSearchDto
    {
        public List<Guid> Ids { get; set; }
        public  string Name { get; set; }
        public List<Guid> UnIds { get; set; }
        public string Sorting { get; set; }
        public int? SkipCount { get; set; }
        public int? MaxResultCount { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
    }
}
