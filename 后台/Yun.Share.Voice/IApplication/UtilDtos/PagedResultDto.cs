using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.IApplication.UtilDtos
{
    public class PagedResultDto<TDto>
    {
        public PagedResultDto(int totalCount, List<TDto> items)
        {
            this.TotalCount = totalCount;
            this.Items = items;
        }
        public long TotalCount { get; set; }

        public List<TDto> Items { get; set; }
    }
}
