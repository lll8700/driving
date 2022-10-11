
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Dtos;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.Controllers.Admin
{
    [ApiController]
    [Route("api/User")]
    public class UserController : AuthorizeController
    {
        private readonly IUserServer _server;

        public UserController(IUserServer UserServer)
        {
            _server = UserServer;
        }
        [HttpPost]
        [Route(RouteName.CreateAction)]
        public Task<UserDto> CreateAsync(CreateUserDto input)
        {
            return _server.CreateAsync(input);
        }
        [HttpGet]
        [Route(RouteName.GetAction)]
        public Task<UserDto> GetAsync(Guid Id)
        {
            return _server.GetAsync(Id);
        }
        [HttpGet]
        [Route(RouteName.GetListAction)]
        public Task<PagedResultDto<UserDto>> GetListAsync(UserListInput input)
        {
            return _server.GetListAsync(input);
        }
        [HttpPost]
        [Route(RouteName.UpdateAction)]
        public Task<UserDto> UpdateAsync(CreateUserDto input)
        {
            return _server.UpdateAsync(input);
        }
       
    }
}
