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
    public class PracticeTypeServer : BaseVoiceServer<PracticeType, PracticeTypeDto, PracticeTypeListInput, PracticeTypeDto>, IPracticeTypeServer
    {
        private readonly CoreDbContext _db;
        public PracticeTypeServer(CoreDbContext db)
        {
            _db = db;
            base.query = db.PracticeTypes.AsQueryable();
        }
        public override async Task<PracticeTypeDto> CreateAsync(PracticeTypeDto input)
        {
            if(input.Id.HasValue)
            {
                return await UpdateAsync(input);
            }
            PracticeType per = input.MapTo<PracticeType, PracticeTypeDto>();
            await _db.PracticeTypes.AddAsync(per);
            await _db.SaveChangesAsync();
            return per.MapTo<PracticeTypeDto, PracticeType>();
        }

        public override async Task<PracticeTypeDto> GetAsync(Guid Id)
        {
            PracticeType per = await _db.PracticeTypes.FindAsync(Id);
            return per.MapTo<PracticeTypeDto, PracticeType>();
        }

        public override async Task<PracticeTypeDto> UpdateAsync(PracticeTypeDto input)
        {
            PracticeType per = await _db.PracticeTypes.FindAsync(input.Id);
            per.Name = input.Name;
            await _db.SaveChangesAsync();
            return per.MapTo<PracticeTypeDto, PracticeType>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _db.PracticeTypes.FindAsync(id);
            _db.PracticeTypes.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public override async Task<PagedResultDto<PracticeTypeDto>> GetListAsync(PracticeTypeListInput input)
        {
            //List<PracticeType> list = new List<PracticeType>
            //{
            //    new PracticeType
            //    {
            //        Name = "小车",
            //        Subname ="C1C2C3",
            //        Icon = "icon-xiaoche"
            //    },
            //    new PracticeType
            //    {
            //        Name = "摩托车",
            //        Subname ="D/F/E",
            //        Icon = "icon-motuoche"
            //    },
            //    new PracticeType
            //    {
            //        Name = "货车",
            //        Subname ="A2/N2",
            //        Icon = "icon-xiaochewuliu"
            //    },
            //    new PracticeType
            //    {
            //        Name = "轻型牵引挂车",
            //        Subname ="C6",
            //        Icon = "icon-qianyinche01"
            //    }
            //};
            //await _db.PracticeTypes.AddRangeAsync(list);
            //await _db.SaveChangesAsync();
            return await base.GetListAsync(input);
        }

        protected override IQueryable<PracticeType> Filter(IQueryable<PracticeType> iq, PracticeTypeListInput input)
        {
            if(input.Name.IsNotEmpty())
            {
                iq = iq.Where(x => x.Name.Contains(input.Name));
            }
            if(input.Ids.IsNotEmpty() || input.IsIds == true)
            {
                iq = iq.Where(x => input.Ids.Contains( x.Id));
            }
            return iq;
        }

        protected override async Task<List<PracticeTypeDto>> MapToGetListOutputDtosAsync(List<PracticeType> entities)
        {
            var list = entities.Select(x=>x.MapTo<PracticeTypeDto, PracticeType>()).ToList();
            return list;
        }
    }
}
