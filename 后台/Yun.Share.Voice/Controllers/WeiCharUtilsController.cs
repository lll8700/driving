
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Yun.Share.Voice.Controllers
{
    [ApiController]
    [Authorize]
    [Route("")]
    public class WeiCharUtilsController : AuthorizeController
    {
        private readonly IConfiguration _configuration;
        public WeiCharUtilsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("dXL6d9PRsq.txt")] // 微信验证的端口
        [AllowAnonymous]
        public string Get()
        {
            return _configuration["Authentication:Wechat:WeiCharAuStr"];
        }
    }
}
