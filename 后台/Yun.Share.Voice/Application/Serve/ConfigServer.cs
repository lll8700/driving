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

namespace Yun.Share.Voice.Application.Serve
{
    public class ConfigServer :  IConfigServer
    {
        private readonly CoreDbContext _db;
        private readonly IJwtTokenServer _jwtTokenServer;
        private readonly IUser_JurisdictionServer _user_JurisdictionServer;

        private readonly ICarTypeServer _carTypeServer;
        private readonly ISubjectTypeServer _subjectTypeServer;

        public ConfigServer(CoreDbContext db, IJwtTokenServer jwtTokenServer, IUser_JurisdictionServer user_JurisdictionServer
            , ICarTypeServer carTypeServer
            , ISubjectTypeServer subjectTypeServer)
        {

            _db = db;
            _jwtTokenServer = jwtTokenServer;
            _user_JurisdictionServer = user_JurisdictionServer;
            _carTypeServer = carTypeServer;
            _subjectTypeServer = subjectTypeServer;
        }
       
        public async Task<ConfigDto> GetConfigDtoAsync()
        {
            var config = await _db.Configs.FirstOrDefaultAsync();

            if(config == null)
            {
                config = await SaveConfigAsync();
            }

            return config.MapTo<ConfigDto, Config>();
        }

        public async Task<ConfigDto> UpdateConfigDtoAsync(ConfigDto input)
        {
            var config = await _db.Configs.FirstOrDefaultAsync();

            if (config == null)
            {
                config = await SaveConfigAsync();
            }

            config.CreateUserBasePrice = input.CreateUserBasePrice;
            await _db.SaveChangesAsync();
            return config.MapTo<ConfigDto, Config>();
        }

        private async Task<Config> SaveConfigAsync()
        {
            var  config = new Config
            {
                CreateUserBasePrice = 40,
                StrTime = DateTime.Now
            };
            await _db.Configs.AddAsync(config);
            await _db.SaveChangesAsync();
            return config;
        }

        public async Task<HomeUser_JurisdictionListDto> GetHomeUser_Jurisdiction()
        {

            var userId = _jwtTokenServer.GetCurrentUserId();

            HomeUser_JurisdictionListDto dto = new HomeUser_JurisdictionListDto();

            if (!userId.HasValue)
            {
                return dto;
            }

            var juList = await _user_JurisdictionServer.GetJurisdictionListAsync(userId.Value);

            var carList = juList.Where(x => x.User_JurisdictionTypeEnum == Enum.User_JurisdictionTypeEnum.Car).Select(x=>x.TableId).ToList();

            var subList = juList.Where(x => x.User_JurisdictionTypeEnum == Enum.User_JurisdictionTypeEnum.SubjectType).Select(x => x.TableId).ToList();

            dto.CarTypeDtos = (await _carTypeServer.GetListAsync(new CarTypeListInput { Ids = carList,IsIds = true })).Items;

            dto.SubjectTypeDtos = (await _subjectTypeServer.GetListAsync(new SubjectTypeListInput { Ids = subList, IsIds = true })).Items;

            return dto;
        }

    }
}
