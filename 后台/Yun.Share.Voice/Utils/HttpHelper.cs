using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Yun.Share.Voice.Utils
{
    public class HttpHelper
    {
        /// <summary>
        /// api地址拼接
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public string BuildRequestUrl(string uri, IDictionary<string, string> paramters)
        {
            var requestUrlBuilder = new StringBuilder(128);
            requestUrlBuilder.Append(uri);
            if (!uri.Contains("?") && paramters.Count > 0)
            {
                requestUrlBuilder.Append("?");
            }
            var index = 0;
            foreach (var paramter in paramters)
            {
                if (uri.Contains("?"))
                    requestUrlBuilder.Append("&");
                if (index != 0)
                {
                    requestUrlBuilder.Append("&");
                }
                requestUrlBuilder.AppendFormat("{0}={1}", paramter.Key, paramter.Value);
                index++;
            }
            requestUrlBuilder.Remove(requestUrlBuilder.Length - 1, 1);
            return requestUrlBuilder.ToString();
        }

        /// <summary>
        /// api地址拼接
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public string BuildRequestUrl(string uri, object entity)
        {
            var requestUrlBuilder = new StringBuilder(128);
            requestUrlBuilder.Append(uri);
            if (!uri.Contains("?"))
            {
                requestUrlBuilder.Append("?");
            }
            var per = entity.GetType().GetProperties();
            foreach (var paramter in per)
            {
                if (paramter.GetValue(entity) != null)
                {
                    requestUrlBuilder.Append("&");
                    requestUrlBuilder.AppendFormat("{0}={1}", paramter.Name, paramter.GetValue(entity).ToString());
                }
            }
            //requestUrlBuilder.Remove(requestUrlBuilder.Length - 1, 1);
            return requestUrlBuilder.ToString();
        }

        /// <summary>
        /// 参数拼接
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public string BuildRequestUrl(object entity)
        {
            var requestUrlBuilder = new StringBuilder(1024);
            var per = entity.GetType().GetProperties();
            var index = 0;
            foreach (var paramter in per)
            {
                if (paramter.GetValue(entity) != null)
                {
                    if (index != 0)
                    {
                        requestUrlBuilder.Append("&");
                    }

                    requestUrlBuilder.AppendFormat("{0}={1}", paramter.Name, HttpUtility.UrlEncode(paramter.GetValue(entity).ToString()));
                    index++;
                }
            }
            return requestUrlBuilder.ToString();
        }
    }
}
