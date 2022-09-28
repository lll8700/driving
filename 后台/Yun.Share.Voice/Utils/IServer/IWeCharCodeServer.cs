using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.BaseInterface;
using Yun.Share.Voice.IApplication.Input;

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

        /// <summary>
        /// 注册手机号
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        Task<string> TellPhoneNumber(TellPhonenumberInputDto inputDto);
    }
}
