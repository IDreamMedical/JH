using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace UniGuy.Core.Extensions
{
    /// <summary>
    /// Extension methods for Categorizable
    /// </summary>
    public static class IdObjectExtensions
    {
        #region IEnumerable<T> where T:IIdObject
        /// <summary>
        /// 把IIdObject迭代类型转换为其Id可迭代类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<string> ToIds<T>(this IEnumerable<T> @this) where T : IIdObject
        {
            return @this.Select<T, string>((x) => x.Id);
        }
        /// <summary>
        /// 把IIdObject迭代类型转换为其Id数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string[] ToIdArray<T>(this IEnumerable<T> @this) where T : IIdObject
        {
            return ToIds<T>(@this).ToArray();
        }
        
        #endregion //   IEnumerable<T>
    }

}
