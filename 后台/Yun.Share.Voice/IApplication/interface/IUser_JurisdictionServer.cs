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
    public interface IUser_JurisdictionServer : IDependency
    {
        Task<bool> CreateAsync(User_JurisdictionInput input);

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        Task<List<User_JurisdictionDto>> GetJurisdictionListAsync(Guid UserId);

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        Task<List<User_JurisdictionDto>> GetJurisdictionListAsync(List<Guid> UserIds);

    }
}
