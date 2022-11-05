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
    public class CarTypeServer : BaseVoiceServer<CarType, CarTypeDto, CarTypeListInput, CarTypeDto>, ICarTypeServer
    {
        private readonly CoreDbContext _db;
        public CarTypeServer(CoreDbContext db)
        {
            _db = db;
            base.query = db.CarTypes.AsQueryable();
        }
        public override async Task<CarTypeDto> CreateAsync(CarTypeDto input)
        {
            if(input.Id.HasValue)
            {
                return await UpdateAsync(input);
            }
            CarType per = input.MapTo<CarType, CarTypeDto>();
            await _db.CarTypes.AddAsync(per);
            await _db.SaveChangesAsync();
            return per.MapTo<CarTypeDto, CarType>();
        }

        public override async Task<CarTypeDto> GetAsync(Guid Id)
        {
            CarType per = await _db.CarTypes.FindAsync(Id);
            return per.MapTo<CarTypeDto, CarType>();
        }

        public override async Task<CarTypeDto> UpdateAsync(CarTypeDto input)
        {
            CarType per = await _db.CarTypes.FindAsync(input.Id);
            per.Name = input.Name;
            per.Subname = input.Subname;
            per.Icon = input.Icon;
            await _db.SaveChangesAsync();
            return per.MapTo<CarTypeDto, CarType>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _db.CarTypes.FindAsync(id);
            _db.CarTypes.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public override async Task<PagedResultDto<CarTypeDto>> GetListAsync(CarTypeListInput input)
        {
            //List<CarType> list = new List<CarType>
            //{
            //    new CarType
            //    {
            //        Name = "小车",
            //        Subname ="C1C2C3",
            //        Icon = "icon-xiaoche"
            //    },
            //    new CarType
            //    {
            //        Name = "摩托车",
            //        Subname ="D/F/E",
            //        Icon = "icon-motuoche"
            //    },
            //    new CarType
            //    {
            //        Name = "货车",
            //        Subname ="A2/N2",
            //        Icon = "icon-xiaochewuliu"
            //    },
            //    new CarType
            //    {
            //        Name = "轻型牵引挂车",
            //        Subname ="C6",
            //        Icon = "icon-qianyinche01"
            //    }
            //};
            //await _db.CarTypes.AddRangeAsync(list);
            //await _db.SaveChangesAsync();
            return await base.GetListAsync(input);
        }

        protected override IQueryable<CarType> Filter(IQueryable<CarType> iq, CarTypeListInput input)
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

        protected override async Task<List<CarTypeDto>> MapToGetListOutputDtosAsync(List<CarType> entities)
        {
            var list = entities.Select(x=>x.MapTo<CarTypeDto, CarType>()).ToList();
            return list;
        }
    }
}
