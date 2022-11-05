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
    public interface IConfigServer : IDependency
    {
        Task<ConfigDto> GetConfigDtoAsync();

        Task<HomeUser_JurisdictionListDto> GetHomeUser_Jurisdiction();

        Task<ConfigDto> UpdateConfigDtoAsync(ConfigDto input);
    }
}
