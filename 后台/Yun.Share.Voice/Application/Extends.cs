using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Application
{
    public static partial class Extends
    {
        public static bool IsEmpty(this string input) => string.IsNullOrEmpty(input);
        public static bool IsNotEmpty(this string input) => !string.IsNullOrEmpty(input);
        public static bool IsEmpty<TItem>(this IList<TItem> input) => input == null || input.Count == 0;
        public static bool IsNotEmpty<TItem>(this IList<TItem> input) => input != null && input.Count > 0;

        public static string ToDate(this DateTime? date) => date?.ToString("yyyy-MM-dd");
        public static string ToDateTime(this DateTime? date) => date?.ToString("yyyy-MM-dd HH:mm");
        public static DateTime? ToDateTime(this string date) => string.IsNullOrEmpty(date) ? null : (DateTime?)DateTime.Parse(date);

        public static string FormartDecimal(this decimal value) => value.ToString("f2");
        public static string FormartDecimal(this decimal? value) => value?.ToString("f2");

        public static string FormartLocation(this decimal value) => value.ToString("f6");
        public static string FormartLocation(this decimal? value) => value?.ToString("f6");
    }
}
