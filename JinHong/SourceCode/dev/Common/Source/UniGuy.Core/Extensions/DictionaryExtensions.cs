using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace UniGuy.Core.Extensions
{
    /// <summary>
    /// 	Extension methods for Dictionary.
    /// </summary>
    public static class DictionaryExtensions
    {
        // todo: Needs xml documentation for these methods
        // todo: create unit testing for these methods
        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            return new SortedDictionary<TKey, TValue>(dictionary);
        }

        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");
            if (comparer == null)
                throw new ArgumentNullException("comparer");

            return new SortedDictionary<TKey, TValue>(dictionary, comparer);
        }

        public static IDictionary<TKey, TValue> SortByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return (new SortedDictionary<TKey, TValue>(dictionary)).OrderBy(kvp => kvp.Value).ToDictionary(item => item.Key, item => item.Value);
        }

        public static IDictionary<TValue, TKey> Invert<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");
            return dictionary.ToDictionary(pair => pair.Value, pair => pair.Key);
        }

        public static Hashtable ToHashTable<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var table = new Hashtable();

            foreach (var item in dictionary)
                table.Add(item.Key, item.Value);

            return table;
        }

    #region WJ
        /// <summary>
        /// 获得某个键对应的值(如果键不存在则返回值类型的默认值,而不是直接使用Dictionary索引器时可能导致的异常)
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetKeyedValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : default(TValue);
        }

        /// <summary>
        /// 联合两个字典
        /// </summary>
        /// <typeparam name="TDictionary"></typeparam>
        /// <param name="this"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static TDictionary Union<TDictionary, TKey, TValue>(this TDictionary @this, IDictionary<TKey, TValue> other) where TDictionary : IDictionary<TKey, TValue>, new()
        {
            foreach (TKey key in other.Keys)
                @this.Add(key, other[key]);
            return @this;
        }
        /// <summary>
        /// 交两个字典
        /// </summary>
        /// <typeparam name="TDictionary"></typeparam>
        /// <param name="this"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static TDictionary Intersect<TDictionary, TKey, TValue>(this TDictionary @this, IDictionary<TKey, TValue> other) where TDictionary : IDictionary<TKey, TValue>, new()
        {
            foreach (TKey key in @this.Keys)
                if (!other.ContainsKey(key))
                    @this.Remove(key);
            return @this;
        }
        /// <summary>
        /// 减两个字典
        /// </summary>
        /// <typeparam name="TDictionary"></typeparam>
        /// <param name="this"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static TDictionary Exclude<TDictionary, TKey, TValue>(this TDictionary @this, IDictionary<TKey, TValue> other) where TDictionary : IDictionary<TKey, TValue>, new()
        {
            foreach (TKey key in @this.Keys)
                if (other.ContainsKey(key))
                    @this.Remove(key);
            return @this;
        }

        /// <summary>
        /// 把字典结构转换为方便阅读的方式;
        /// => (key0, value0), (key1, value1), ...
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="entrySep"></param>
        /// <param name="keyValueSep"></param>
        /// <param name="beginTag"></param>
        /// <param name="endTag"></param>
        /// <returns></returns>
        public static string ToString<T, U>(this IDictionary<T, U> dict, string entrySep, string keyValueSep, string beginTag, string endTag)
        {
            StringBuilder sb = new StringBuilder();
            bool b = false;
            foreach (T key in dict.Keys)
            {
                if (b)
                    sb.Append(entrySep);
                else
                    b = true;
                sb.Append(beginTag).Append(key).Append(keyValueSep).Append(dict[key]).Append(endTag);
            }
            return sb.ToString();
        }

        public static string ToString<T, U>(this IDictionary<T, U> dict, string beginTag, string endTag)
        {
            return ToString(dict, "[", "]", beginTag, endTag);
        }
        #endregion
    }

}