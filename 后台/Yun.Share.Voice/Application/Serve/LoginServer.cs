using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.DataBase;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;
using Yun.Share.Voice.Utils;

namespace Yun.Share.Voice.Application.Serve
{
    public class LoginServer : ILoginServer
    {
        private readonly IJwtTokenServer _jwtTokenServer;
        private readonly CoreDbContext _db;
        public LoginServer(IJwtTokenServer jwtTokenServer, CoreDbContext db)
        {
            _jwtTokenServer = jwtTokenServer;
            _db = db;
        }
        public async Task<string> Login(LoginInputDto input)
        {
            var pass = Md5Encrypt.Encrypt(input.Password);
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Password == pass && x.Phone == input.PhoneNumber);
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
    }
}
