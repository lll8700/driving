
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Dtos;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.Controllers.Admin
{
    [ApiController]
    [Route("api/Config")]
    public class ConfigController : AuthorizeController
    {
        private readonly IConfigServer _server;
        private readonly IUser_JurisdictionServer _user_JurisdictionServer;

        public ConfigController(IConfigServer configServer, IUser_JurisdictionServer user_JurisdictionServer)
        {
            _server = configServer;
            _user_JurisdictionServer = user_JurisdictionServer;
        }

        [HttpGet]
        [Route(RouteName.GetFist)]
        public Task<ConfigDto> GetConfigDtoAsync()
        {
            return _server.GetConfigDtoAsync();
        }
        [HttpPost]
        [Route(RouteName.UpdateAction)]
        public Task<ConfigDto> UpdateConfigDtoAsync(ConfigDto input)
        {
            return _server.UpdateConfigDtoAsync(input);
        }
        [HttpGet]
        [Route("Home")]
        public Task<HomeUser_JurisdictionListDto> GetHomeUser_Jurisdiction()
        {
            return _server.GetHomeUser_Jurisdiction();
        }

        [HttpGet]
        [Route("jurisdiction")]
        public Task<List<User_JurisdictionDto>> GetHomeUser_Jurisdiction(Guid userId)
        {
            return _user_JurisdictionServer.GetJurisdictionListAsync(userId);
        }
        [HttpPost]
        [Route("createjurisdiction")]
        public Task<bool> CreateAsync(User_JurisdictionInput input)
        {
            return _user_JurisdictionServer.CreateAsync(input);
        }
    }
}
