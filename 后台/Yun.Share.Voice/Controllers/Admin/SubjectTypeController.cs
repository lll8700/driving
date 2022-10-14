
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
    [Route("api/SubjectType")]
    public class SubjectTypeController : AuthorizeController
    {
        private readonly ISubjectTypeServer _server;

        public SubjectTypeController(ISubjectTypeServer SubjectTypeServer)
        {
            _server = SubjectTypeServer;
        }
        [HttpPost]
        [Route(RouteName.CreateAction)]
        public Task<SubjectTypeDto> CreateAsync(SubjectTypeDto input)
        {
            return _server.CreateAsync(input);
        }
        [HttpGet]
        [Route(RouteName.GetAction)]
        public Task<SubjectTypeDto> GetAsync(Guid Id)
        {
            return _server.GetAsync(Id);
        }
        [HttpPost]
        [Route(RouteName.GetListAction)]
        public Task<PagedResultDto<SubjectTypeDto>> GetListAsync(SubjectTypeListInput input)
        {
            return _server.GetListAsync(input);
        }
        [HttpPost]
        [Route(RouteName.UpdateAction)]
        public Task<SubjectTypeDto> UpdateAsync(SubjectTypeDto input)
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
