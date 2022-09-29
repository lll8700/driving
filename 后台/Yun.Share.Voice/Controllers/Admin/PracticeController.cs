
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.Controllers.Admin
{
    [ApiController]
    [Route("api/practice")]
    public class PracticeController : AuthorizeController
    {
        private readonly IPracticeServer _server;

        public PracticeController(IPracticeServer practiceServer)
        {
            _server = practiceServer;
        }
        [HttpPost]
        [Route(RouteName.CreateAction)]
        public Task<PracticeDto> CreateAsync(PracticeDto input)
        {
            return _server.CreateAsync(input);
        }
        [HttpGet]
        [Route(RouteName.GetAction)]
        public Task<PracticeDto> GetAsync(Guid Id)
        {
            return _server.GetAsync(Id);
        }
        [HttpGet]
        [Route(RouteName.GetListAction)]
        public Task<PagedResultDto<PracticeDto>> GetListAsync(PracticeListInput input)
        {
            return _server.GetListAsync(input);
        }
        [HttpPost]
        [Route(RouteName.UpdateAction)]
        public Task<PracticeDto> UpdateAsync(PracticeDto input)
        {
            return _server.UpdateAsync(input);
        }
        [HttpPost]
        [Route("excel")]
        public Task<bool> UploadFilePath(IFormFile file)
        {
            return _server.UploadFilePath(file);
        }

        /// <summary>
        ///  按顺序获取下一个
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("next")]
        public Task<PracticeDto> GetNextAsync(PracticeListInput input)
        {
            return _server.GetNextAsync(input);
        }

        /// <summary>
        /// 获取排除ids的随机一个题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Random")]
        public Task<PracticeDto> GetRandomAsync(PracticeListInput input)
        {
            return _server.GetRandomAsync(input);
        }
    }
}
