using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Dtos;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.Application
{
    public class JwtTokenServer : IJwtTokenServer
    {
        #region 依赖注入
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtTokenServer(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetToken(JwtAuthorizationTokenInput input)
        {
            //相关Token的常量
            //var claims = new[]
            //{
            //     new Claim(ClaimTypes.SerialNumber, input.UserId.ToString()),
            //     new Claim(ClaimTypes.Name, input.Name),
            //     new Claim(ClaimTypes.MobilePhone, input.PhoneNumber)
            //};
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.SerialNumber, input.UserId.ToString()));
            if (!input.Name.IsNullOrEmpty())
            {
                claims.Add(new Claim(ClaimTypes.Name, input.Name));
            }
            if (!input.PhoneNumber.IsNullOrEmpty())
            {
                claims.Add(new Claim(ClaimTypes.MobilePhone, input.PhoneNumber));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Jwt:SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //JWT规定的部分字段
            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:Jwt:Issuer"],//提供者
                audience: _configuration["Authentication:Jwt:Audience"],//被授权者
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Authentication:Jwt:ExpireMinutes"])),//过期时间
                signingCredentials: creds
                );

            string Token = new JwtSecurityTokenHandler().WriteToken(token);

            return $"Bearer {Token}";
        }

        /// <summary>
        /// 获取当前登录用户ID
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public Guid? GetCurrentUserId()
        {
            Guid? userId = null;

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userIdStr = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(t => t.Type == ClaimTypes.SerialNumber).Value;
                if (!userIdStr.IsNullOrEmpty())
                {
                    userId = Guid.Parse(userIdStr);
                }
            }
            return userId;
        }

        /// <summary>
        /// 获取当前登录用户ID
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public CurrentUserDto GetCurrentUser()
        {
            CurrentUserDto dto = null;

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userData = _httpContextAccessor.HttpContext.User.Claims;
                dto = new CurrentUserDto
                {
                    UserId = Guid.Parse(userData.SingleOrDefault(t => t.Type == ClaimTypes.SerialNumber).Value),
                    Name = userData.SingleOrDefault(t => t.Type == ClaimTypes.Name).Value.ToString(),
                    PhoneNumber = userData.SingleOrDefault(t => t.Type == ClaimTypes.MobilePhone).Value.ToString()
                };
            }
            return dto;

        }

        public void RemoveToken()
        {


        }
        #endregion

    }
}
