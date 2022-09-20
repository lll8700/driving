
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Yun.Share.Voice.IApplication;
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
    }
}
