using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yun.Share.Voice.BaseInterface;
using Yun.Share.Voice.IApplication.Dtos;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.IApplication
{
    public interface IJwtTokenServer: IDependency
    {
        /// <summary>
        /// 根据用户Id获取Token
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        string GetToken(JwtAuthorizationTokenInput input);


        /// <summary>
        /// 获取当前登录的用户Id
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        Guid? GetCurrentUserId();
        /// <summary>
        /// 获取当前登录用户的信息
        /// </summary>
        /// <returns></returns>
        CurrentUserDto GetCurrentUser();
    }
}
