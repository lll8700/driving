using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Utils
{
    [Flags]
    public enum CodeType
    {
        Number = 1,
        LowerLetters = 2,
        UpperLetters = 4,
    }

    /// <summary>
    /// 随机码生成工具
    /// <hr/>
    /// 通常用于生成验证码
    /// </summary>
    public class CodeGenerateHelper
    {
        private const string numbers = "0123456789";
        private const string upperLetter = "ABCDEFGHIGKLMNOPQRSTUVWXYZ";
        private const string lowerLetters = "abcdefghigklmnopqrstuvwxyz";


        public static string GenerateCode(CodeType codeType = CodeType.Number, int codeLength = 6)
        {
            var avaliableCodes = GetAvalidateCodeArrays(codeType);

            var result = string.Empty;
            Random random = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                result += avaliableCodes[random.Next(avaliableCodes.Length)];
            }
            return result;
        }
        private static string GetAvalidateCodeArrays(CodeType codeType)
        {
            switch (codeType)
            {
                case CodeType.Number:
                    return numbers;
                case CodeType.Number | CodeType.LowerLetters:
                    return numbers + lowerLetters;
                case CodeType.Number | CodeType.UpperLetters:
                    return numbers + upperLetter;
                case (CodeType.Number | CodeType.LowerLetters | CodeType.UpperLetters):
                    return numbers + lowerLetters + upperLetter;
                case CodeType.LowerLetters | CodeType.UpperLetters:
                    return lowerLetters + upperLetter;
                case CodeType.LowerLetters:
                    return lowerLetters;
                case CodeType.UpperLetters:
                    return upperLetter;
                default:
                    return numbers;
            }
        }
    }
}
