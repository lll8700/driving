using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice
{
    public class HttpGlobalExceptionFilter : ExceptionFilterAttribute
    {
     
          public override void OnException(ExceptionContext context)
         {
             var actionName = context.HttpContext.Request.RouteValues["controller"] + "/" + context.HttpContext.Request.RouteValues["action"];
            Log.Logger.Error($"--------{actionName} Error Begin--------");
            Log.Logger.Error($"  Error Detail:" + context.Exception.ToString());
             //拦截处理
             //if (!context.ExceptionHandled) // 把异常捕获返回到前端 让程序继续运行
             //{
             //    context.Result = new JsonResult(new 
             //    {
             //        status = false,
             //        msg = context.Exception.Message
             //    });
             //    context.ExceptionHandled = true;
             //}
            Log.Logger.Error($"--------{actionName} Error End--------");
         }
    }
}
