using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yun.Share.Voice.BaseInterface;
using Yun.Share.Voice.IApplication.Dtos;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.IApplication
{
    public interface ILoginServer : IDependency
    {
        /// <summary>
        /// 根据用户Id获取Token
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<string> Login(LoginInputDto input);

        Task<string> Create(LoginInputDto input);

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LoginDto> WeChatLogin(LoginInputDto input);

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SavePhoneNumber(LoginInputDto input);
        /// <summary>
        /// 获取并保存手机号
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<UserDto> TellPhoneNumber(TellPhonenumberInputDto inputDto);

    }
}
