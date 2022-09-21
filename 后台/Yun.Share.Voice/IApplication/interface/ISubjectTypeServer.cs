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
    public interface ISubjectTypeServer : IDependency
    {
        Task<SubjectTypeDto> CreateAsync(SubjectTypeDto input);

        Task<SubjectTypeDto> GetAsync(Guid Id);
        Task<SubjectTypeDto> UpdateAsync(SubjectTypeDto input);
        Task<PagedResultDto<SubjectTypeDto>> GetListAsync(SubjectTypeListInput input);
    }
}
