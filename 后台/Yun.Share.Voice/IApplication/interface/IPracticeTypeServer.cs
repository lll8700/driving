using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.BaseInterface;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.IApplication
{
    public interface IPracticeTypeServer : IDependency
    {
        Task<PracticeTypeDto> CreateAsync(PracticeTypeDto input);

        Task<PracticeTypeDto> GetAsync(Guid Id);
        Task<PracticeTypeDto> UpdateAsync(PracticeTypeDto input);
        Task<PagedResultDto<PracticeTypeDto>> GetListAsync(PracticeTypeListInput input);

        Task<bool> DeleteAsync(Guid id);
    }
}
