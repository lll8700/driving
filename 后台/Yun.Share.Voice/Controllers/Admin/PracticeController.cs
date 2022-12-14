
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
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
        [HttpPost]
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
        [Route(RouteName.DeleteAction)]
        public Task<bool> DeleteAsync(Guid id)
        {
            return _server.DeleteAsync(id);
        }
        [HttpPost]
        [Route("zip")]
        public int UploadImageZip(IFormFile file)
        {
            return _server.UploadImageZip(file);
        }

        [HttpPost]
        [Route("excel")]
        public Task<int> UploadExcel(PracticeFileInput input)
        {
            return _server.UploadExcel(input);
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
        /// <summary>
        /// 获取考试100题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("count")]
        public Task<int> GetSucceedCount(PracticeListInput input)
        {
            return _server.GetSucceedCount(input);
        }
        /// <summary>
        /// 获取考试100题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("testlist")]
        public Task<PagedResultDto<PracticeDto>> GetTestListAsync(PracticeTestListInput input)
        {
            return _server.GetTestListAsync(input);
        }
        [HttpPost]
        [Route("outexcel")]
        public async Task<IActionResult> GetCommissionFormListExcel(PracticeListInput input)
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

    }
}
