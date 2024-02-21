using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.DataBase;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Dtos;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;
using Yun.Share.Voice.Models.Entities;
using Yun.Share.Voice.Utils;

namespace Yun.Share.Voice.Application.Serve
{
    public class User_JurisdictionServer : IUser_JurisdictionServer
    {
        private readonly CoreDbContext _db;
        public User_JurisdictionServer(CoreDbContext db, IJwtTokenServer jwtTokenServer)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(User_JurisdictionInput input)
        {
            if(input.UserStatusTypeEnum.HasValue)
            {
                var user = await _db.Users.FindAsync(input.UserId);

                if(user.UserStatusTypeEnum != input.UserStatusTypeEnum)
                {
                    user.UserStatusTypeEnum = input.UserStatusTypeEnum.Value;
                    if(input.UserStatusTypeEnum == Enum.UserStatusTypeEnum.Invalid)
                    {
                        await _db.SaveChangesAsync();
                        return true;
                    }
                }
            }
           
            var list = await _db.User_Jurisdictions.Where(x => x.UserId == input.UserId).ToListAsync();

            _db.User_Jurisdictions.RemoveRange(list);

            foreach (var car in input.Cars)
            {
                User_Jurisdiction _Jurisdiction = new User_Jurisdiction
                {
                    UserId = input.UserId,
                    TableId = car,
                    User_JurisdictionTypeEnum = Enum.User_JurisdictionTypeEnum.Car
                };
                await _db.User_Jurisdictions.AddAsync(_Jurisdiction);
            }
            foreach (var car in input.SubjectTypes)
            {
                User_Jurisdiction _Jurisdiction = new User_Jurisdiction
                {
                    UserId = input.UserId,
                    TableId = car,
                    User_JurisdictionTypeEnum = Enum.User_JurisdictionTypeEnum.SubjectType
                };
                await _db.User_Jurisdictions.AddAsync(_Jurisdiction);
            }
            await _db.SaveChangesAsync();
            return true;

        }



        public async Task<List<User_JurisdictionDto>> GetJurisdictionListAsync(Guid UserId)
        {
            var list = await _db.User_Jurisdictions.Where(x => x.UserId == UserId).ToListAsync();

            return list.Select(x => x.MapTo<User_JurisdictionDto, User_Jurisdiction>()).ToList();
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public async Task<List<User_JurisdictionDto>> GetJurisdictionListAsync(List<Guid> UserIds)
        {
            var list = await _db.User_Jurisdictions.Where(x => UserIds.Contains(x.UserId) ).ToListAsync();
            return list.Select(x => x.MapTo<User_JurisdictionDto, User_Jurisdiction>()).ToList();
        }
    }
}
