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
    public class HomeServer : IHomeServer
    {
        private readonly CoreDbContext _db;
        public async Task<Dictionary<string,List<string>>> GetListAsync()
        {
            return null;
        }

        public Task<CarTypeDto> IndexAsync()
        {
            throw new NotImplementedException();
        }
    }
}
