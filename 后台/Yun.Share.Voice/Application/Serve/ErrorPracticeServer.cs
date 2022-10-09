using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.DataBase;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;
using Yun.Share.Voice.Models.Entities;
using Yun.Share.Voice.Utils;

namespace Yun.Share.Voice.Application.Serve
{
    public class ErrorPracticeServer : BaseVoiceServer<ErrorPracticeLog, ErrorPracticeLogDto, ErrorPracticeListInput, CreateErrorPracticeLogDto>, IErrorPracticeServer
    {
        private readonly CoreDbContext _db;

        private readonly IJwtTokenServer _jwtTokenServer;
        public ErrorPracticeServer(CoreDbContext db, IJwtTokenServer jwtTokenServer)
        {
            _db = db;
            base.query = db.ErrorPracticeLoges.AsQueryable();
            _jwtTokenServer = jwtTokenServer;
        }
        public override async Task<ErrorPracticeLogDto> CreateAsync(CreateErrorPracticeLogDto input)
        {
            ErrorPracticeLog log = new ErrorPracticeLog();
            log.UserId = _jwtTokenServer.GetCurrentUserId().Value;
            log.PracticeId = input.PracticeId;

            foreach(var item in input.OptionIds)
            {
                ErrorPracticeId errorPractice = new ErrorPracticeId()
                {
                    IsCorrect = item.IsCorrect,
                    ErrorPracticeLog = log,
                    OptionId = item.OptionId
                };
                await _db.ErrorPracticeIds.AddAsync(errorPractice);
            }

            await _db.ErrorPracticeLoges.AddAsync(log);
            await _db.SaveChangesAsync();
            return log.MapTo<ErrorPracticeLogDto, ErrorPracticeLog>();
        }

        public override async Task<ErrorPracticeLogDto> GetAsync(Guid Id)
        {
            Practice per = await _db.Practices.FindAsync(Id);
            return per.MapTo<ErrorPracticeLogDto, Practice>();
        }

        public override async Task<ErrorPracticeLogDto> UpdateAsync(CreateErrorPracticeLogDto input)
        {
            return null;
        }

        public override async Task<PagedResultDto<ErrorPracticeLogDto>> GetListAsync(ErrorPracticeListInput input)
        {
            return await base.GetListAsync(input);
        }
        /// <summary>
        /// 初始化外键
        /// </summary>
        /// <param name="iq"></param>
        /// <returns></returns>
        private IQueryable<ErrorPracticeLog> IncludeDefault(IQueryable<ErrorPracticeLog> iq)
        {
            return iq
                .Include(x => x.Practice)
                //.Include(x => x.CarType)
                //.Include(x => x.Options)
                //.Include(x => x.SubjectType);
                ;
            
        }
        
        protected override IQueryable<ErrorPracticeLog> Filter(IQueryable<ErrorPracticeLog> iq, ErrorPracticeListInput input)
        {
            iq = IncludeDefault(iq);

            if (input.UserId.HasValue)
            {
                iq = iq.Where(x => x.UserId == input.UserId);
            }
            if (input.PracticeId.HasValue)
            {
                iq = iq.Where(x => x.PracticeId == input.PracticeId);
            }
            if (input.Ids.IsNotEmpty())
            {
                iq = iq.Where(x => input.Ids.Contains(x.Id));
            }

            if (input.UnIds.IsNotEmpty())
            {
                iq = iq.Where(x => !input.UnIds.Contains(x.Id));
            }
            return iq;
        }

        protected override async Task<List<ErrorPracticeLogDto>> MapToGetListOutputDtosAsync(List<ErrorPracticeLog> entities)
        {
            if(entities.Count == 0)
            {
                return new List<ErrorPracticeLogDto>();
            }

            var list = entities.Select(x => x.MapTo<ErrorPracticeLogDto, ErrorPracticeLog>()).ToList();

            var ids = list.Select(x => x.Id).ToList();

            var practiceIds = await _db.ErrorPracticeIds.Include(x=>x.Option).Where(x => ids.Contains(x.ErrorPracticeLogId)).ToListAsync();


            for(var i=0;i<list.Count;i++)
            {
                var x = list[i];
                var cars = practiceIds.Where(s => s.ErrorPracticeLogId == x.Id).ToList();
                if (cars.IsNotEmpty())
                {
                    x.ErrorPracticeIdDtos = cars.Select(f => f.MapTo<ErrorPracticeIdDto, ErrorPracticeId>()).ToList();
                }
            }
            return list;
        }
    }
}
