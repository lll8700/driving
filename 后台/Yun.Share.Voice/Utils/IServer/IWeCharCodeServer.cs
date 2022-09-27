using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.BaseInterface;

namespace Yun.Share.Voice.Utils.IServer
{
    public interface IWeCharCodeServer: IDependency
    {
        /// <summary>
        /// 获取小程序OpenId
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<string> GetOpenId(string code);
        // <summary>
        /// B接口-微信小程序带参数二维码的生成
        /// </summary>
        /// <param name="MicroId"></param>
        /// <returns></returns>
        string CreateWxCode(string code);
    }
}
