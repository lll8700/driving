
using Microsoft.AspNetCore.Authorization;
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
    }
}
