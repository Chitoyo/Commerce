using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Commerce.Models
{
    /// <summary>
    /// [Lambda]通用函式
    /// </summary>
    public static class LambdaUtil
    {
        /// <summary>
        /// Dictionary 轉為物件(欄位不分大小寫)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dict">IDictionary 物件</param>
        /// <returns></returns>
        public static T DictionaryToObject<T>(IDictionary<string, string> dict) where T : new()
        {
            var t = new T();
            PropertyInfo[] properties = t.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                try
                {
                    if (dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        KeyValuePair<string, string> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

                        Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

                        Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

                        object newA = Convert.ChangeType(item.Value, newT);
                        t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
                    }
                }
                catch { }

            }
            return t;
        }

        /// <summary>
        /// 將model 轉換為KeyValuePair List, null值不轉
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="model">欲轉換之Model</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> ModelToKeyValuePairList<T>(T model)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            try
            {
                Type t = model.GetType();
                foreach (var p in t.GetProperties())
                {
                    string name = p.Name;
                    object value = p.GetValue(model, null);
                    if (value != null)
                    {
                        result.Add(new KeyValuePair<string, string>(name, value.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

    }
}
