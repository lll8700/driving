using Microsoft.AspNetCore.Http;
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
    public interface IPracticeServer : IDependency
    {
        Task<PracticeDto> CreateAsync(PracticeDto input);

        Task<PracticeDto> GetAsync(Guid Id);
        Task<PracticeDto> UpdateAsync(PracticeDto input);
        Task<PagedResultDto<PracticeDto>> GetListAsync(PracticeListInput input);
        Task<bool> UploadFilePath(IFormFile file);
    }
}
