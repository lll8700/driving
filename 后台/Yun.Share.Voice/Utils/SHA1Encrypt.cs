using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Yun.Share.Voice.Utils
{
    public class SHA1Encrypt
    {
        public static string Encrypt(string source, bool lowerCase = false)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }
            var strRes = Encoding.Default.GetBytes(source);
            HashAlgorithm iSha = new SHA1CryptoServiceProvider();
            strRes = iSha.ComputeHash(strRes);
            var enText = new StringBuilder();
            foreach (byte iByte in strRes)
            {
                enText.AppendFormat("{0:x2}", iByte);
            }
            if (lowerCase)
            {
                return enText.ToString().ToLower();
            }
            else
            {
                return enText.ToString().ToUpper();
            }
           
        }
    }
}
