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
    public class UserServer : BaseVoiceServer<User, UserDto, UserListInput, CreateUserDto>, IUserServer
    {
        private readonly CoreDbContext _db;
        private readonly IJwtTokenServer _jwtTokenServer;
        public UserServer(CoreDbContext db, IJwtTokenServer jwtTokenServer)
        {
            _db = db;
            _jwtTokenServer = jwtTokenServer;
            base.query = db.Users.AsQueryable();
        }
        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            var userId = _jwtTokenServer.GetCurrentUserId();
            if(!userId.HasValue)
            {
                throw new Exception($"当前无登录");
            }
            var user = await GetAsync(userId.Value);
            if (user== null)
            {
                throw new Exception($"当前无登录");
            }
            if (user.UserTypeEnum != Enum.UserTypeEnum.Sale && user.UserTypeEnum != Enum.UserTypeEnum.Admin)
            {
                throw new Exception("只有销售员和管理员可以添加账户");
            }

            User per = new User();
            per.UserStatusTypeEnum = Enum.UserStatusTypeEnum.Formal;
            per.UserTypeEnum = Enum.UserTypeEnum.Empty;
            per.StrTime = DateTime.Now;
            per.Name = input.Phone;
            per.Phone = input.Phone;
            per.Password = Md5Encrypt.Encrypt(input.Password);
            await _db.Users.AddAsync(per);
            await _db.SaveChangesAsync();
            return per.MapTo<UserDto, User>();
        }

        public override async Task<UserDto> GetAsync(Guid Id)
        {
            User per = await _db.Users.FindAsync(Id);
            return per.MapTo<UserDto, User>();
        }

        public async Task<UserDto> GetRandomAsync(UserListInput input)
        {
            var perItem = await NextFilter(input).OrderBy(x=> Guid.NewGuid()).FirstOrDefaultAsync();

            return perItem == null ? new UserDto() : await GetMapDto(perItem);
        }

        public override async Task<UserDto> UpdateAsync(CreateUserDto input)
        {
            User per = await _db.Users.FindAsync(input.Id);
            if(input.UserStatusTypeEnum.HasValue)
                per.UserStatusTypeEnum = input.UserStatusTypeEnum.Value;
            if (input.UserTypeEnum.HasValue)
                per.UserTypeEnum = input.UserTypeEnum.Value;
            if (input.EndTime.HasValue)
                per.EndTime = input.EndTime;
            if (!input.Password.IsNotEmpty())
                per.Password = Md5Encrypt.Encrypt(input.Password);
            
            await _db.SaveChangesAsync();
            return per.MapTo<UserDto, User>();
        }

        public override async Task<PagedResultDto<UserDto>> GetListAsync(UserListInput input)
        {
            return await base.GetListAsync(input);
        }
        /// <summary>
        /// 初始化外键
        /// </summary>
        /// <param name="iq"></param>
        /// <returns></returns>
        private IQueryable<User> IncludeDefault(IQueryable<User> iq)
        {
            return iq
                //.Include(x => x.UserImages)
                //.Include(x => x.CarType)
                //.Include(x => x.Options)
                //.Include(x => x.SubjectType);
                ;
            
        }
        /// <summary>
        /// 答题的筛选
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private IQueryable<User> NextFilter(UserListInput input)
        {

            var iq = _db.Users.AsQueryable();
            iq = IncludeDefault(iq);
            if (!input.Ids.IsNotEmpty())
            {
                input.Ids = new List<Guid>();
            }
            if (input.UserStatusTypeEnum.HasValue)
            {
                iq = iq.Where(x => x.UserStatusTypeEnum == input.UserStatusTypeEnum);
            }
            if (input.UserTypeEnum.HasValue)
            {
                iq = iq.Where(x => x.UserTypeEnum == input.UserTypeEnum);
            }
          
            return iq.Where(x => !input.Ids.Contains(x.Id));
        }
        protected override IQueryable<User> Filter(IQueryable<User> iq, UserListInput input)
        {
            iq = IncludeDefault(iq);

            if (input.Name.IsNotEmpty())
            {
                iq = iq.Where(x => x.Name.Contains(input.Name) || x.Phone.Contains(input.Name));
            }
            if (input.UserStatusTypeEnum.HasValue)
            {
                iq = iq.Where(x => x.UserStatusTypeEnum == input.UserStatusTypeEnum);
            }
            if (input.UserTypeEnum.HasValue)
            {
                iq = iq.Where(x => x.UserTypeEnum == input.UserTypeEnum);
            }
            if (input.Ids.IsNotEmpty())
            {
                iq = iq.Where(x => input.Ids.Contains(x.Id));
            }
           
            if (input.UnIds.IsNotEmpty())
            {
                iq = iq.Where(x => !input.UnIds.Contains(x.Id));
            }
            if(input.IsSelfCreate == true)
            {
                var userId = _jwtTokenServer.GetCurrentUserId();
                iq = iq.Where(x => x.CreatorId == userId.Value);
            }
            return iq;
        }

        protected override async Task<List<UserDto>> MapToGetListOutputDtosAsync(List<User> entities)
        {
            if(entities.Count == 0)
            {
                return new List<UserDto>();
            }

            var list = entities.Select(x => x.MapTo<UserDto, User>()).ToList();

            var createIds = list.Select(x => x.CreatorId).ToList();

            var users = await _db.Users.Where(x => createIds.Contains(x.Id)).Select(x => new { x.Id, x.CreatorId,x.Name }).ToListAsync();

            list.ForEach(item =>
            {
                item.CreateUserName = users.FirstOrDefault(x => x.Id == item.CreatorId)?.Name;
            });

            return list;
        }

        private async Task<UserDto> GetMapDto(User per)
        {
            var list = await MapToGetListOutputDtosAsync(new List<User> { per });
            return list[0];
        }
    }
}
