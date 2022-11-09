
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
    [Route("api/PracticeType")]
    public class PracticeTypeController : AuthorizeController
    {
        private readonly IPracticeTypeServer _server;

        public PracticeTypeController(IPracticeTypeServer PracticeTypeServer)
        {
            _server = PracticeTypeServer;
        }
        [HttpPost]
        [Route(RouteName.CreateAction)]
        public Task<PracticeTypeDto> CreateAsync(PracticeTypeDto input)
        {
            return _server.CreateAsync(input);
        }
        [HttpGet]
        [Route(RouteName.GetAction)]
        public Task<PracticeTypeDto> GetAsync(Guid Id)
        {
            return _server.GetAsync(Id);
        }
        [HttpPost]
        [Route(RouteName.GetListAction)]
        public Task<PagedResultDto<PracticeTypeDto>> GetListAsync(PracticeTypeListInput input)
        {
            return _server.GetListAsync(input);
        }
        [HttpPost]
        [Route(RouteName.UpdateAction)]
        public Task<PracticeTypeDto> UpdateAsync(PracticeTypeDto input)
        {
            return _server.UpdateAsync(input);
        }
        [HttpPost]
        [Route(RouteName.DeleteAction)]
        public Task<bool> DeleteAsync(Guid Id)
        {
            return _server.DeleteAsync(Id);
        }
    }
}
