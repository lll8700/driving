using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Yun.Share.Voice.Utils;

namespace Yun.Share.Voice.Controllers
{
    public class SelectList
    {
        public object Key { get; set; }
        public string Label { get; set; }
    }
    [ApiController]
    [Authorize]
    [Route("api/Voice/Enum")]
    [ResponseCache(Duration = 60 * 60, Location = ResponseCacheLocation.Client)]
    public class EnumsController : AuthorizeController
    {
        /// <summary>
        /// 获取枚举（可以获取任何枚举）
        /// </summary>
        /// <param name="f">命名空间</param>
        /// <param name="c">类名</param>
        /// <param name="a">程序集</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getenum")]
        public virtual List<SelectList> GetClassEnum(string className)
        {
            
            // var obj = EntityMap.CreateInstance("Yun.Module.TraderManage.Enum", "PayMode"); 举例
            //var className = "UserStatusTypeEnum";
            var fullName = "Yun.Share.Voice.Enum";
            //var fullName = "Yun.Share.Voice.Domain.Shared";
            var assemblyName = "Yun.Share.Voice";
            var obj = EntityMap.CreateInstance(fullName, className, assemblyName); 
            return GetEnum(obj);
        }
        private List<SelectList> GetEnum(object obj)
        {
           
            return EnumHelper.ToList(obj)
              .Select(x => new SelectList()
              {
                  Key = x.Id,
                  Label = x.Name,
              }).ToList();
        }
    }
}
