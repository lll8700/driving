using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Yun.Share.Voice.IApplication;
namespace Yun.Share.Voice.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/voice/OssUtil")]
    public class OssUtilController : AuthorizeController
    {
        private readonly IAliyunOssStorageServer _appService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public OssUtilController(IAliyunOssStorageServer appService,IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _appService = appService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("upload")]
        [RequestSizeLimit(1024 * 1024 * 200)]
        /// <summary>
        /// 目前RestApi应该不支持多文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string UploadFile(IFormFile file)
        {
            return _appService.UploadFile(file);
        }
        [HttpGet]
        [Route("uploadPath")]
        public string UploadFilePath(string url)
        {
            return _appService.PutObject(url);
        }
        [HttpPost("WeChatFile")]
        public string UploadWeChatFile()
        {
            var type = _configuration["Authentication:Oss:UploadType"];
            var file = _httpContextAccessor.HttpContext.Request.Form.Files[type];
            //do something

            return _appService.UploadFile(file);
        }
        [HttpDelete("delete")]
        public void DeleteObject(string url)
        {
            _appService.DeleteObject(url);
        }
    }
}
