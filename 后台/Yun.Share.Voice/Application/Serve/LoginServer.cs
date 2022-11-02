using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.DataBase;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Dtos;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;
using Yun.Share.Voice.Models.Entities;
using Yun.Share.Voice.Utils;
using Yun.Share.Voice.Utils.IServer;

namespace Yun.Share.Voice.Application.Serve
{
    public class LoginServer : ILoginServer
    {
        private readonly IJwtTokenServer _jwtTokenServer;
        private readonly CoreDbContext _db;
        private readonly IWeCharCodeServer _weCharCodeServer;
        public LoginServer(IJwtTokenServer jwtTokenServer, CoreDbContext db, IWeCharCodeServer weCharCodeServer)
        {
            _jwtTokenServer = jwtTokenServer;
            _db = db;
            _weCharCodeServer = weCharCodeServer;
        }
        public async Task<string> Login(LoginInputDto input)
        {
            var pass = Md5Encrypt.Encrypt(input.Password);
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Password == pass && x.Phone == input.PhoneNumber && x.UserTypeEnum == Enum.UserTypeEnum.Admin && x.UserStatusTypeEnum == Enum.UserStatusTypeEnum.Formal);
            var userData = string.Empty;
            if(user != null)
            {
                var jwtInput = new JwtAuthorizationTokenInput
                {
                    Name = user.Name,
                    PhoneNumber = input.PhoneNumber,
                    UserId = user.Id
                };
                userData = _jwtTokenServer.GetToken(jwtInput);
            }
            return userData;
        }

        public async Task<UserDto> Info()
        {
            UserDto userDto = null;
            var userId = _jwtTokenServer.GetCurrentUserId();
            if(userId.HasValue)
            {
                User user = await _db.Users.FindAsync(userId.Value);
                userDto = user.MapTo<UserDto, User>();
            }

            return userDto;
        }


        public async Task<LoginDto> WeChatLogin(LoginInputDto input)
        {
            var userId = _jwtTokenServer.GetCurrentUserId();
            User user = null;
            LoginDto dto = new LoginDto();
            if (userId.HasValue)
            {
                user = await _db.Users.FindAsync(userId.Value);
            }else
            {
                var openId = await _weCharCodeServer.GetOpenId(input.OpenId);
                if(openId.IsNotEmpty())
                {
                    user = await _db.Users.FirstOrDefaultAsync(x => x.WeChatId == openId);
                    if (user == null)
                    {
                        user = new User
                        {
                            WeChatId = openId,
                            UserTypeEnum = Enum.UserTypeEnum.Other,
                            StrTime = DateTime.Now,
                            UserStatusTypeEnum = Enum.UserStatusTypeEnum.Formal
                        };
                        await _db.Users.AddAsync(user);
                        await _db.SaveChangesAsync();
                    }
                }else
                {
                    return null;
                }
            }
           
            var jwtInput = new JwtAuthorizationTokenInput
            {
                Name = user.Name,
                PhoneNumber = input.PhoneNumber,
                UserId = user.Id
            };
            var token = _jwtTokenServer.GetToken(jwtInput);
            var userDto = user.MapTo<UserDto, User>();
            dto.Token = token;
            dto.UserDto = userDto;
            return dto;
        }

        public async Task<LoginDto>WebLogin(LoginInputDto input)
        {
            var pass = Md5Encrypt.Encrypt(input.Password);
            var date = DateTime.Now;
            User user = await _db.Users.FirstOrDefaultAsync(x => 
            x.Password == pass 
            && x.Phone == input.PhoneNumber
            //&& x.UserTypeEnum == Enum.UserTypeEnum.Empty
            && x.UserStatusTypeEnum == Enum.UserStatusTypeEnum.Formal
             && x.StrTime < date
             && x.EndTime == null || x.EndTime > date
            );

            LoginDto dto = new LoginDto();

            if (user != null)
            {
                var jwtInput = new JwtAuthorizationTokenInput
                {
                    Name = user.Name,
                    PhoneNumber = input.PhoneNumber,
                    UserId = user.Id
                };
                var token = _jwtTokenServer.GetToken(jwtInput);
                var userDto = user.MapTo<UserDto, User>();
                dto.Token = token;
                dto.UserDto = userDto;
            }

            return dto;
        }

        public async Task<UserDto> TellPhoneNumber(TellPhonenumberInputDto inputDto)
        {
            var phone = await _weCharCodeServer.TellPhoneNumber(inputDto);

            if(phone.IsEmpty())
            {
                return null;
            }

            var user = await _db.Users.FindAsync(inputDto.UserId);

            if(user.Phone.IsEmpty())
            {
                user.Phone = phone;
                await _db.SaveChangesAsync();
            }

            return user.MapTo<UserDto, User>();
        }

        public async Task<bool> SavePhoneNumber(LoginInputDto input)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.WeChatId == input.OpenId);
            user.Phone = input.PhoneNumber;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<string> Create(LoginInputDto input)
        {
            var userId = _jwtTokenServer.GetCurrentUserId();
            if (!userId.HasValue)
            {
                throw new Exception($"当前无登录");
            }
            var user = await _db.Users.FindAsync(userId.Value);
            if (user == null)
            {
                throw new Exception($"当前无登录");
            }
            if (user.UserTypeEnum != Enum.UserTypeEnum.Sale && user.UserTypeEnum != Enum.UserTypeEnum.Admin)
            {
                throw new Exception("只有销售员和管理员可以添加账户");
            }
            var baseUser = await _db.Users.FirstOrDefaultAsync(x => x.Phone == input.PhoneNumber);

            if(baseUser!= null)
            {
                throw new Exception("当前用户已有账号");
            }

            var pass = Md5Encrypt.Encrypt(input.Password);

            User u = new User()
            {
                Password = pass,
                Phone = input.PhoneNumber,
                Name = input.PhoneNumber,
                UserTypeEnum = Enum.UserTypeEnum.Empty,
                StrTime = DateTime.Now,
                UserStatusTypeEnum = Enum.UserStatusTypeEnum.Formal
            };
            await _db.Users.AddAsync(u);
            await _db.SaveChangesAsync();
            return "success";
        }
    }
}
