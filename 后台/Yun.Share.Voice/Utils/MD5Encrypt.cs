using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Yun.Share.Voice.Utils
{
    /// <summary>
    /// MD5
    /// </summary>
    public class Md5Encrypt
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">加密字符串</param>
        /// <param name="lowerCase">是否小写</param>
        /// <returns></returns>
        public static string Encrypt(string source, bool lowerCase = false)
        {
            if (source.IsNull())
                return null;

            return Encrypt(Encoding.UTF8.GetBytes(source), lowerCase);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">加密字节流</param>
        /// <param name="lowerCase">是否小写</param>
        /// <returns></returns>
        public static string Encrypt(byte[] source, bool lowerCase = false)
        {
            if (source == null)
                return null;

            using (var md5Hash = MD5.Create())
            {
                return md5Hash.ComputeHash(source).ToHex(lowerCase);
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="inputStream">流</param>
        /// <param name="lowerCase">是否小写</param>
        /// <returns></returns>
        public static string Encrypt(Stream inputStream, bool lowerCase = false)
        {
            if (inputStream == null) return null;

            using (var md5Hash = MD5.Create())
            {
                return md5Hash.ComputeHash(inputStream).ToHex(lowerCase);
            }
        }

        /// <summary>
        /// 加密帮助方法(房天下独有的加密)
        /// </summary>
        /// <param name="secret">秘钥兼向量</param>
        /// <param name="content">待加密字符串</param>
        /// <returns>返回base64的加密串</returns>
        public static string DesEncrypt(string secret, string content)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            secret = secret + "00000000";
            secret = secret.Substring(0, 8);
            byte[] inputByteArray = Encoding.UTF8.GetBytes(content);
            des.Key = Encoding.UTF8.GetBytes(secret);
            des.IV = Encoding.UTF8.GetBytes(secret);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}
