using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Yun.Share.Voice.Utils
{
    /// <summary>
    /// 实体赋值实体
    /// </summary>
    public class EntityMap
    {
        /// <summary>
        /// A实体赋值到B实体
        /// </summary>
        /// <typeparam name="T">B实体 返回的实体</typeparam>
        /// <param name="t2"></param>
        /// <param name="mapType"></param>
        /// <returns></returns>
        public T GetEntityDto<T>(object t2)
        {
            var t1 =  Activator.CreateInstance<T>();//初始化t1

            if (t2 == null)
            {
                return t1;
            }
            var perList = t2.GetType().GetProperties();

            var pert1 = t1.GetType().GetProperties();

            var perName1 = pert1.Select(m => m.Name).ToList();

            foreach (var item in perList)
            {
                var name = item.Name;//字段名
                if (perName1.Contains(name)) //包含这个字段
                {
                    var value = item.GetValue(t2); //获取值
                    if (value != null)
                    {
                        var pro = t1.GetType().GetProperty(name);

                        pro.SetValue(t1, value);
                    }
                }
            }
            return t1;
        }

        /// <summary>
        /// A实体赋值到B实体
        /// </summary>
        /// <typeparam name="T">B实体 返回的实体</typeparam>
        /// <param name="t2"></param>
        /// <param name="IsMustNull">是否强制赋值空值</param>
        /// <returns></returns>
        public void GetEntityDto<T>(object t2, ref T t, bool IsMustNull = false, bool isUpdate = false , BindingFlags bindingFlags = BindingFlags.Public)
        {
            //var t1 = Activator.CreateInstance<T>();//初始化t1

            if (t2 == null)
            {
                return;
            }

            var perList = t2.GetType().GetProperties();

            var pert1 = t.GetType().GetProperties();

            var perName1 = pert1.Select(m => m.Name).ToList();

            foreach (var item in perList)
            {
                var name = item.Name;//字段名
                if (perName1.Contains(name)) //包含这个字段
                {
                    var value = item.GetValue(t2); //获取值

                    if (IsMustNull || isUpdate || value != null)
                    {
                        var pro = pert1.FirstOrDefault(m=>m.Name==name);

                        if (isUpdate)
                        {
                            pro.SetValue(t, value);
                        }
                        else
                        {
                            var va1 = pro.GetValue(t);
                            if (va1 == null)
                            {
                                try
                                {
                                    pro.SetValue(t, value);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 解析一个实体到Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="list">排除的实体字段</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDictionaryByEntity<T>(T t, IList<string> list = null)
        {
            var perList = t.GetType().GetProperties();

            var perName1 = perList.Select(m => m.Name).ToList();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (var item in perList)
            {
                var name = item.Name;//字段名

                if (list == null || !list.Contains(name))
                {
                    var value = item.GetValue(t); //获取值
                    if (value != null && !string.IsNullOrEmpty(value.ToString()))
                    {
                        dic.Add(name, value.ToString().Trim());
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 创建对象实例(obj类型 获取后可强转)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullName">命名空间</param>
        /// <param name="calssName">类名</param>
        /// <param name="assemblyName">程序集</param>
        /// <returns></returns>
        public static object CreateInstance(string fullName,string calssName, string assemblyName )
        {
            string path =$"{fullName}.{calssName},{assemblyName}";//命名空间.类型名,程序集
            Type o = Type.GetType(path);//加载类型
            object obj = Activator.CreateInstance(o, true);//根据类型创建实例
            return obj;//类型转换并返回
        }
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                //此为第一种写法
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }
    }
}
