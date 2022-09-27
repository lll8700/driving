
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NPOI.Util;
using System.Text.Json;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Yun.Share.Voice.DataBase;
using Yun.Share.Voice.Utils.IServer;
using Newtonsoft.Json;

namespace Yun.Share.Voice.Utils.Server
{
    /// <summary>
    /// 小程序二维码生成工具
    /// </summary>
    public class WeCharCodeServer : IWeCharCodeServer
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string loginUrlTemplate = "https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&grant_type=authorization_code&js_code={2}";
        private readonly CoreDbContext _myDBContext;
        public WeCharCodeServer(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, CoreDbContext myDBContext)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _myDBContext = myDBContext;
        }
        public class AuthCode2SessionResponse
        {
            public string openid { get; set; }
            public string session_key { get; set; }
            public string unionid { get; set; }
            public int errcode { get; set; }
            public string errmsg { get; set; }
        }

        /// <summary>
        /// 微信登录凭证校验，获取服务器端session_key
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<AuthCode2SessionResponse> AuthCode2SessionAsync(string code)
        {
            var appId = _configuration["Authentication:Wechat:AppId"];
            var appSecret = _configuration["Authentication:Wechat:AppSecret"];
            var loginUrl = string.Format(loginUrlTemplate, appId, appSecret, code);

            AuthCode2SessionResponse result = null;

            var client = _httpClientFactory.CreateClient(WechatHttpConsts.HttpClientName);
            var request = new HttpRequestMessage(HttpMethod.Get, loginUrl);
            request.Headers.Add("Accept", "application/json");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                result = await System.Text.Json.JsonSerializer.DeserializeAsync<AuthCode2SessionResponse>(responseStream);
            }
            return result;
        }


        /// <summary>
        /// 小程序登录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<string> GetOpenId(string code)
        {
            var wxResult = await AuthCode2SessionAsync(code);

            return wxResult.openid;
        }

        /// <summary>
        /// 获取accessToken
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public string GetAccessToken()
        {
            var appid = _configuration["Authentication:Wechat:AppId"];
            var secret = _configuration["Authentication:Wechat:AppSecret"];

            var url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;

            var client = _httpClientFactory.CreateClient(WechatHttpConsts.HttpClientName);

            var response = client.GetAsync(url).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"UpdateUser http request service returns error! HttpStatusCode: {response.StatusCode}, ReasonPhrase: {response.ReasonPhrase}");
            }
            string resultContent = response.Content.ReadAsStringAsync().Result;

            JObject _jObject = (JObject)JsonConvert.DeserializeObject(resultContent);
            var _value = _jObject["access_token"].ToString();    //取值

            return _value;
        }

        // <summary>
        /// B接口-微信小程序带参数二维码的生成
        /// </summary>
        /// <param name="MicroId"></param>
        /// <returns></returns>
        public string CreateWxCode(string code)
        {
            string ret = string.Empty;
            DateTime date = DateTime.Now.AddMinutes(60 * 1.5 * -1); //  为了保险 一个半小时重新获取
            //var token = _myDBContext.App_L_WeChar_Token_Logs.FirstOrDefault(x => x.CreationTime > date);
            //if(token == null)
            //{
             var toekenStr = GetAccessToken();

            //    token = new Models.Share.L_WeChar_Token_Log
            //    {
            //        Token = toekenStr
            //    };
            //    _myDBContext.App_L_WeChar_Token_Logs.Add(token);
            //    _myDBContext.SaveChanges();
            //}
             
            try
            {
                string DataJson = string.Empty;
                string url = "https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token=" + toekenStr;

                WeiCharCodeDataDto dto = new WeiCharCodeDataDto();
                dto.scene = code;
                DataJson = JsonConvert.SerializeObject(dto);

                //DataJson = "{";
                //DataJson += string.Format("\"scene\":{0},", 1);//所要传的参数用,分看
                //DataJson += string.Format("\"width\":\"{0}\",", 124); // 二维码大小
                //DataJson += string.Format("\"page\":\"{0}\",", "pages/basics/myteam/myteam");//扫码所要跳转的地址，根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面
                //DataJson += "\"line_color\":{";
                //DataJson += string.Format("\"r\":\"{0}\",", "0");
                //DataJson += string.Format("\"g\":\"{0}\",", "0");
                //DataJson += string.Format("\"b\":\"{0}\"", "0");
                //DataJson += "}";
                //DataJson += "}";

                ret = GetResponseData(url, DataJson).Result;
                //DataJson的配置见小程序开发文档，B接口：https://mp.weixin.qq.com/debug/wxadoc/dev/api/qrcode.html
                //ret = PostMoths(url, DataJson);
                if (ret.Length > 0)
                {
                    //对图片进行存储操作，下次可直接调用你存储的图片，不用再调用接口
                }
            }
            catch (Exception e)
            { ret = e.Message; }
            return ret;//返回图片地址
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<string> GetResponseData(string url, string data)
        {
            var client = _httpClientFactory.CreateClient(WechatHttpConsts.HttpClientName);

            var requestContent = new StringContent(data, Encoding.UTF8);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = requestContent
            };

            requestMessage.Headers.Add("Accept", "application/json");
            var response = await client.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"UpdateUser http request service returns error! HttpStatusCode: {response.StatusCode}, ReasonPhrase: {response.ReasonPhrase}");
            }
            var resultContent = await response.Content.ReadAsStringAsync();
            string error = string.Empty;

            try
            {
                //{"errcode":41030,"errmsg":"invalid page rid: 61b6b513-6cb01310-6ffda32d"}
                JObject _jObject = (JObject)JsonConvert.DeserializeObject(resultContent);
                error = _jObject["errcode"].ToString();    
            }
            catch
            {

            }

            if(error != string.Empty)
            {
                throw new Exception($"生成二维码出错{error}");
            }

            var tt = await response.Content.ReadAsByteArrayAsync();


            return Convert.ToBase64String(tt);
        }

        //请求处理，返回二维码图片
        public string PostMoths(string url, string param)
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            string paraUrlCoded = param;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            
            System.IO.Stream s;
            s = response.GetResponseStream();//返回图片数据流
            byte[] tt = StreamToBytes(s);//将数据流转为byte[]

            //在文件名前面加上时间，以防重名
            string imgName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
            //文件存储相对于当前应用目录的虚拟目录
            string path = "/image/";
            //获取相对于应用的基目录,创建目录
            string imgPath = System.AppDomain.CurrentDomain.BaseDirectory + path;     //通过此对象获取文件名
           
            System.IO.File.WriteAllBytes("D://" + path + imgName, tt);//讲byte[]存储为图片
            


            s.Close();
            return Convert.ToBase64String(tt);
        }
        ///将数据流转为byte[]
        public static byte[] StreamToBytes(Stream stream)
        {
            List<byte> bytes = new List<byte>();
            int temp = stream.ReadByte();
            while (temp != -1)
            {
                bytes.Add((byte)temp);
                temp = stream.ReadByte();
            }
            return bytes.ToArray();
        }
      

        /// <summary>
        /// Image 转成 base64
        /// </summary>
        /// <param name="fileFullName"></param>
        public string ImageToBase64(string fileFullName)
        {
            try
            {
                Bitmap bmp = new Bitmap(fileFullName);
                MemoryStream ms = new MemoryStream();
                var suffix = fileFullName.Substring(fileFullName.LastIndexOf('.') + 1,
                    fileFullName.Length - fileFullName.LastIndexOf('.') - 1).ToLower();
                var suffixName = suffix == "png"
                    ? ImageFormat.Png
                    : suffix == "jpg" || suffix == "jpeg"
                        ? ImageFormat.Jpeg
                        : suffix == "bmp"
                            ? ImageFormat.Bmp
                            : suffix == "gif"
                                ? ImageFormat.Gif
                                : ImageFormat.Jpeg;

                bmp.Save(ms, suffixName);
                byte[] arr = new byte[ms.Length]; ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length); ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch
            {
                return null;
            }

        }

        public class WeiCharCodeDataDto
        {
            public string scene { get; set; } // 页面参数
            public int width { get; set; } // 二维码大小  默认64*64

            public string page { get; set; } = "pages/index/index"; // 扫码默认跳转页面
        }
    }
}
