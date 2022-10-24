using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.BaseInterface;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.IApplication
{
    public interface IPracticeServer : IDependency
    {
        Task<PracticeDto> CreateAsync(PracticeDto input);

        Task<PracticeDto> GetAsync(Guid Id);
        Task<PracticeDto> UpdateAsync(PracticeDto input);
        Task<PagedResultDto<PracticeDto>> GetListAsync(PracticeListInput input);
        //Task<int> UploadFilePath(PracticeFileInput file);

        /// <summary>
        ///  按顺序获取下一个
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<PracticeDto> GetNextAsync(PracticeListInput input);

        /// <summary>
        /// 获取排除ids的随机一个题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PracticeDto> GetRandomAsync(PracticeListInput input);

        Task<int> UploadExcel(PracticeFileInput input);
        int UploadImageZip(IFormFile file);

        /// <summary>
        /// 获取考试100题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PracticeDto>> GetTestListAsync(PracticeTestListInput input);

        Task<bool> DeleteAsync(Guid id);
    }
}
