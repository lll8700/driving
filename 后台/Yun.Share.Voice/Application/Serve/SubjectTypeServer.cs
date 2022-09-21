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
    public class SubjectTypeServer : BaseVoiceServer<SubjectType, SubjectTypeDto, SubjectTypeListInput, SubjectTypeDto>, ISubjectTypeServer
    {
        private readonly CoreDbContext _db;
        public SubjectTypeServer(CoreDbContext db)
        {
            _db = db;
            base.query = db.SubjectTypes.AsQueryable();
        }
        public override async Task<SubjectTypeDto> CreateAsync(SubjectTypeDto input)
        {
            SubjectType per = input.MapTo<SubjectType, SubjectTypeDto>();
            await _db.SubjectTypes.AddAsync(per);
            await _db.SaveChangesAsync();
            return per.MapTo<SubjectTypeDto, SubjectType>();
        }

        public override async Task<SubjectTypeDto> GetAsync(Guid Id)
        {
            SubjectType per = await _db.SubjectTypes.FindAsync(Id);
            return per.MapTo<SubjectTypeDto, SubjectType>();
        }

        public override async Task<SubjectTypeDto> UpdateAsync(SubjectTypeDto input)
        {
            SubjectType per = await _db.SubjectTypes.FindAsync(input.Id);
            per.Name = input.Name;
            await _db.SaveChangesAsync();
            return per.MapTo<SubjectTypeDto, SubjectType>();
        }

        public override async Task<PagedResultDto<SubjectTypeDto>> GetListAsync(SubjectTypeListInput input)
        {
            return await base.GetListAsync(input);
        }

        protected override IQueryable<SubjectType> Filter(IQueryable<SubjectType> iq, SubjectTypeListInput input)
        {
            if(input.Name.IsNotEmpty())
            {
                iq = iq.Where(x => x.Name.Contains(input.Name));
            }
            return iq;
        }

        protected override async Task<List<SubjectTypeDto>> MapToGetListOutputDtosAsync(List<SubjectType> entities)
        {
            var list = entities.MapTo<List<SubjectTypeDto>, List<SubjectType>>();
            return list;
        }
    }
}
