using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.Controllers
{
    /// <summary>
    /// 具有登录验证的基础控制器
    /// </summary>
  
    public class AuthorizeController: ControllerBase
    {
        public AuthorizeController()
        {

        }
        protected RequestData GetBaseData(object data)
        {
            return new RequestData
            {
                Data = data,
                Success = true,
            };
        }
        protected RequestData GetBaseData(string message)
        {
            return new RequestData
            {
                Data = null,
                Success = false,
                Message = message
            };
        }
    }
}
