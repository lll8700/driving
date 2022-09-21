
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
    [Route("api/cartype")]
    public class CarTypeController : AuthorizeController
    {
        private readonly ICarTypeServer _server;

        public CarTypeController(ICarTypeServer CarTypeServer)
        {
            _server = CarTypeServer;
        }
        [HttpPost]
        [Route(RouteName.CreateAction)]
        public Task<CarTypeDto> CreateAsync(CarTypeDto input)
        {
            return _server.CreateAsync(input);
        }
        [HttpGet]
        [Route(RouteName.GetAction)]
        public Task<CarTypeDto> GetAsync(Guid Id)
        {
            return _server.GetAsync(Id);
        }
        [HttpGet]
        [Route(RouteName.GetListAction)]
        public Task<PagedResultDto<CarTypeDto>> GetListAsync(CarTypeListInput input)
        {
            return _server.GetListAsync(input);
        }
        [HttpPost]
        [Route(RouteName.UpdateAction)]
        public Task<CarTypeDto> UpdateAsync(CarTypeDto input)
        {
            return _server.UpdateAsync(input);
        }
    }
}
