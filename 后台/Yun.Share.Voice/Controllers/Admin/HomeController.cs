
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
    [Route("api/home")]
    public class HomeController : AuthorizeController
    {
        private readonly ICarTypeServer _server;

        public HomeController(ICarTypeServer CarTypeServer)
        {
            _server = CarTypeServer;
        }
       
    }
}
