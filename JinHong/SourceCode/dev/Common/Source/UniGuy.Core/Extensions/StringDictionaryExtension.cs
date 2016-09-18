using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace UniGuy.Core.Extensions
{
    //  wj
    //  20120320
    public static class StringDictionaryExtension
    {
        /// <summary>
        /// 把字符串字典转换为泛型字段
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="this">字符串字典</param>
        /// <returns>泛型字段</returns>
        public static T ToDictionary<T>(this StringDictionary @this) where T : Dictionary<string, string>, new()
        {
            T dict = new T();

            foreach (string key in @this.Keys)
                dict[key] = @this[key];

            return dict;
        }

        /// <summary>
        /// 把字符串字典转换为Hashtable
        /// </summary>
        /// <param name="this">字符串字典</param>
        /// <param name="isCaseSensitive">是否大小写敏感</param>
        /// <returns>Hashtable</returns>
        public static Hashtable ToHashtable(this StringDictionary @this, bool ignoreCase)
        {
            Hashtable ht = ignoreCase ? new Hashtable(new CaseInsensitiveHashCodeProvider(), CaseInsensitiveComparer.Default) : new Hashtable();

            foreach (string key in @this.Keys)
                ht[key] = @this[key];

            return ht;
        }

        public static Hashtable ToHashtable(this StringDictionary @this)
        {
            return ToHashtable(@this, false);
        }
    }
}