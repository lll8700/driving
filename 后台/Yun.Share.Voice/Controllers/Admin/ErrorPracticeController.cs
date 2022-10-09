
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
    [Route("api/err")]
    public class ErrorPracticeController : AuthorizeController
    {
        private readonly IPracticeServer _server;

        public ErrorPracticeController(IPracticeServer practiceServer)
        {
            _server = practiceServer;
        }
        [HttpPost]
        [Route(RouteName.CreateAction)]
        public Task<PracticeDto> CreateAsync(PracticeDto input)
        {
            return _server.CreateAsync(input);
        }
        
    }
}
