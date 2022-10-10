
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Dtos;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.Controllers.Admin
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : AuthorizeController
    {
        private readonly ILoginServer _loginServer;

        public LoginController(ILoginServer loginServer)
        {
            _loginServer = loginServer;
        }
        [HttpPost]
        [Route("token")]
        public async Task<TokenData> GetToken(LoginInputDto input)
        {
            var str = await _loginServer.Login(input);
            TokenData data = new TokenData { code = 40000 };
            if (!string.IsNullOrEmpty(str))
            {
                data.code = 20000;
                data.data = new Token { token = str };
            }
            return data;
        }

        [HttpPost]
        [Route("gettoken")]
        public async Task<WxTokenData> WeChatLogin(LoginInputDto input)
        {
            var dto = await _loginServer.WeChatLogin(input);
            WxTokenData data = new WxTokenData { code = 40000 };
            if (dto != null)
            {
                data.code = 20000;
                data.data = dto;
            }
            return data;
        }

        [HttpPost]
        [Route("weblogin")]
        public async Task<WxTokenData> WebLogin(LoginInputDto input)
        {
            var dto = await _loginServer.WebLogin(input);
            WxTokenData data = new WxTokenData { code = 40000 };
            if (dto.UserDto!=null)
            {
                data.code = 20000;
                data.data = dto;
            }
            return data;
        }


        [HttpPost]
        [Route("phone")]
        public Task<UserDto> TellPhoneNumber(TellPhonenumberInputDto input)
        {
            return _loginServer.TellPhoneNumber(input);
           
        }
        /// <summary>
        /// ÐÞ¸ÄÊÖ»úºÅ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public Task<bool> SavePhoneNumber(LoginInputDto input)
        {
            return _loginServer.SavePhoneNumber(input);
        }

        [HttpPost]
        [Route(RouteName.CreateAction)]
        public async Task<TokenData> Create(LoginInputDto input)
        {
            var str = await _loginServer.Create(input);
            TokenData data = new TokenData { code = 40000 };
            if (!string.IsNullOrEmpty(str))
            {
                data.code = 20000;
                data.data = new Token { token = str };
            }
            return data;
        }
    }
}
