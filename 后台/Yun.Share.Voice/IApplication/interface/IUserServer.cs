using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.BaseInterface;
using Yun.Share.Voice.IApplication.Dtos;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.IApplication
{
    public interface IUserServer : IDependency
    {
        Task<UserDto> CreateAsync(CreateUserDto input);

        Task<UserDto> GetAsync(Guid Id);
        Task<UserDto> UpdateAsync(CreateUserDto input);
        Task<PagedResultDto<UserDto>> GetListAsync(UserListInput input);
        Task<bool> Delete(CreateUserDto input);
    }
}
