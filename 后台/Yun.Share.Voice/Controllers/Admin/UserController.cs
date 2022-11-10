
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
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
        [HttpPost]
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
        [HttpPost]
        [Route(RouteName.DeleteAction)]
        public Task<bool> Delete(CreateUserDto input)
        {
            return _server.Delete(input);
        }

        #region µ¼³ö
        [HttpPost]
        [Route("outexcel")]
        public async Task<IActionResult> GetCommissionFormListExcel(UserListInput input)
        {
            var datas = await _server.GetCommissionFormListExcel(input);

            using (var sw = new FileStream(datas.Url, FileMode.Open))
            {
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                return new FileContentResult(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

        }

        #endregion

    }
}
