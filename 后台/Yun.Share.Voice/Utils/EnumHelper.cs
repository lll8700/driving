using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Yun.Share.Voice.Utils
{
    public static class EnumHelper
    {

        /// <summary>
        /// 表示枚举所产生的数据项。
        /// </summary>
        public class EnumItem
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="EnumItem"/> class.
            /// </summary>
            public EnumItem() { }

            /// <summary>
            /// 实例化一个枚举数据项对象。
            /// </summary>
            /// <param name="id">枚举的值。</param>
            /// <param name="name">枚举定义的描述信息。</param>
            public EnumItem(object id, string name)
            {
                this.Id = id;
                this.Name = name;
            }

            /// <summary>
            /// 枚举的值。
            /// </summary>
            public object Id { get; internal set; }

            /// <summary>
            /// 枚举定义的描述信息。
            /// </summary>
            public string Name { get; internal set; }
        }
        #region 静态方法
        public static Dictionary<string, string> GetEnumDescription<T>()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            FieldInfo[] fields = typeof(T).GetFields();

            foreach (FieldInfo field in fields)
            {

                if (field.FieldType.IsEnum)
                {

                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;

                    dic.Add(field.Name, description);

                }
            }

            return dic;
        }

        /// <summary>        
        /// 获取对应的枚举描述        
        /// </summary>        
        public static List<KeyValuePair<string, string>> GetEnumDescriptionList<T>()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            FieldInfo[] fields = typeof(T).GetFields();

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {

                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;

                    result.Add(new KeyValuePair<string, string>(field.Name, description));

                }

            }
            return result;
        }
        /// <summary>
        /// 将一个指定的枚举定义转换成集合对象.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public  static List<EnumItem> ToList(object obj)
        {
            //Array datas = Enum.GetValues(typeof(T));
            var items = new List<EnumItem>();
            var type = obj.GetType();
            FieldInfo[] fields = type.GetFields();

            foreach (var field in fields)
            {
                if (field.FieldType != type) continue;
                var item = new EnumItem
                {
                    Id = field.GetRawConstantValue(),
                    Name = GetEnumCaption(type, field.Name)
                };
                items.Add(item);
            }
            return items;
        }

        private static string GetEnumCaption(Type type, object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            var description = GetEnumDescription(type, value, 0);
            if (string.IsNullOrEmpty(description))
            {
                return value.ToString();
            }
            return description;
        }
        private static string GetEnumCaption<T>(T value)
        {
            return GetEnumCaption(value?.GetType(), value);
        }
        private static string GetEnumDescription<T>(T value, int pIndex)
        {
            return GetEnumDescription(value?.GetType(), value, pIndex);
        }

        private static string GetEnumDescription(Type type, object value, int pIndex)
        {
            // get attributes  
            var field = type?.GetField(value.ToString());
            if (field == null) return null;
            var attributes = field.GetCustomAttributes(false);
            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(pIndex);
            }
            // return description
            return displayAttribute?.Description ?? value.ToString();
            //return null;
        }

        /// <summary>
        /// 获取枚举的 值和描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> GetEnumValueDescriptionList<T>()
        {
            List<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;
                    result.Add(new KeyValuePair<int, string>(Convert.ToInt32(field.GetValue(null)), description));
                }
            }

            return result;
        }

        public static string GetDescriptionByEnumName<T>(string name)
        {
            try
            {
                Dictionary<string, string> dic = GetEnumDescription<T>();
                string description = dic[name];
                return description;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 扩展方法，获得枚举的Description
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="nameInstead">当枚举值没有定义DescriptionAttribute，是否使用枚举名代替，默认是使用</param>
        /// <returns>枚举的Description</returns>
        public static string GetDescription(this System.Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = System.Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute?.Description;
        }

        public static List<T> GetEnnmValuseList<T>()
        {
            Array values = System.Enum.GetValues(typeof(T));
            List<T> list = new List<T>();
            foreach (var item in values)
            {
                list.Add((T)item);
            }
            return list;
        }

        #endregion
    }
    
}
