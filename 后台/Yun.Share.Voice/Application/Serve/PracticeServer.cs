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

namespace Yun.Share.Voice.Application.Serve
{
    public class PracticeServer : BaseVoiceServer<Practice, PracticeDto, PracticeListInput, PracticeDto>, IPracticeServer
    {
        private readonly CoreDbContext _db;
        public PracticeServer(CoreDbContext db)
        {
            _db = db;
            base.query = db.Practices.AsQueryable();
        }
        public override async Task<PracticeDto> CreateAsync(PracticeDto input)
        {
            Practice per = input.MapTo<Practice, PracticeDto>();
            await _db.Practices.AddAsync(per);
            await _db.SaveChangesAsync();
            return per.MapTo<PracticeDto, Practice>();
        }

        public override async Task<PracticeDto> GetAsync(Guid Id)
        {
            Practice per = await _db.Practices.FindAsync(Id);
            return per.MapTo<PracticeDto, Practice>();
        }

        public override async Task<PracticeDto> UpdateAsync(PracticeDto input)
        {
            Practice per = await _db.Practices.FindAsync(input.Id);
            per.Title = input.Title;
            per.SubjectTypeId = input.SubjectTypeId;
            per.CarTypeId = input.CarTypeId;
            await _db.SaveChangesAsync();
            return per.MapTo<PracticeDto, Practice>();
        }

        public override async Task<PagedResultDto<PracticeDto>> GetListAsync(PracticeListInput input)
        {
            return await base.GetListAsync(input);
        }

        protected override IQueryable<Practice> Filter(IQueryable<Practice> iq, PracticeListInput input)
        {
            if(input.Name.IsNotEmpty())
            {
                iq = iq.Where(x => x.Title.Contains(input.Name));
            }
            if (input.CarTypeId.HasValue)
            {
                iq = iq.Where(x => x.CarTypeId == input.CarTypeId);
            }
            if (input.SubjectTypeId.HasValue)
            {
                iq = iq.Where(x => x.SubjectTypeId == input.SubjectTypeId);
            }
            if (input.Ids.IsNotEmpty())
            {
                iq = iq.Where(x => input.Ids.Contains(x.Id));
            }
            if (input.StatusTypeEnum.HasValue)
            {
                iq = iq.Where(x => x.StatusTypeEnum == input.StatusTypeEnum);
            }
            if (input.UnIds.IsNotEmpty())
            {
                iq = iq.Where(x => !input.UnIds.Contains(x.Id));
            }
            return iq;
        }

        protected override async Task<List<PracticeDto>> MapToGetListOutputDtosAsync(List<Practice> entities)
        {
            var list = entities.MapTo<List<PracticeDto>, List<Practice>>();
            return list;
        }
    }
}
