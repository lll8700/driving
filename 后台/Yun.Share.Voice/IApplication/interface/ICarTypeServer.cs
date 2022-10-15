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
    public interface ICarTypeServer : IDependency
    {
        Task<CarTypeDto> CreateAsync(CarTypeDto input);

        Task<CarTypeDto> GetAsync(Guid Id);
        Task<CarTypeDto> UpdateAsync(CarTypeDto input);
        Task<PagedResultDto<CarTypeDto>> GetListAsync(CarTypeListInput input);

        Task<bool> DeleteAsync(Guid id);
    }
}
