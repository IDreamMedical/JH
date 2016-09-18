using System;
using System.Collections.Generic;

namespace UniGuy.Core.Extensions
{
    /// <summary>
    /// 	Extension methods for the array data type
    /// </summary>
    public static class ArrayExtension
    {
        ///<summary>
        ///	Check if the array is null or empty
        ///</summary>
        ///<param name = "source"></param>
        ///<returns></returns>
        public static bool IsNullOrEmpty(this Array source)
        {
            return source != null ? source.Length <= 0 : false;
        }

        ///<summary>
        ///	Check if the index is within the array
        ///</summary>
        ///<param name = "source"></param>
        ///<param name = "index"></param>
        ///<returns></returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static bool WithinIndex(this Array source, int index)
        {
            return source != null && index >= 0 && index < source.Length;
        }

        ///<summary>
        ///	Check if the index is within the array
        ///</summary>
        ///<param name = "source"></param>
        ///<param name = "index"></param>
        ///<param name="dimension"></param>
        ///<returns></returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static bool WithinIndex(this Array source, int index, int dimension)
        {
            return source != null && index >= source.GetLowerBound(dimension) && index <= source.GetUpperBound(dimension);
        }

    #region WJ

    #region 一维泛型数组
        #endregion //   一维泛型数组
        /// <summary>
        /// 把数组转换成列表
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this T[] @this)
        {
            return new List<T>(@this);
        }
        /// <summary>
        /// 给数组新增一个值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="str"></param>
        public static T[] Add<T>(this T[] @this, T t)
        {
            Array.Resize(ref @this, @this.Length + 1);
            @this[@this.Length - 1] = t;
            return @this;
        }
        /// <summary>
        /// 给数组在指定位置插入一个值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <param name="str"></param>
        public static T[] Insert<T>(this T[] @this, int index, T t)
        {
            Array.Resize(ref @this, @this.Length + 1);
            Array.Copy(@this, index, @this, index + 1, @this.Length - index - 1);
            @this[index] = t;
            return @this;
        }
        /// <summary>
        /// 删除指定索引处的值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        public static T[] Remove<T>(this T[] @this, int index)
        {
            Array.Copy(@this, index + 1, @this, index, @this.Length - index - 1);
            Array.Resize(ref @this, @this.Length - 1);
            return @this;
        }
        /// <summary>
        /// 删除数组中指定的第一个值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="str"></param>
        /// <param name="backWard"></param>
        /// <returns>索引</returns>
        public static T[] RemoveFirst<T>(this T[] @this, T t)
        {
            for (int i = 0; i < @this.Length; i++)
            {
                if (object.Equals(@this[i], t))
                {
                    @this = Remove(@this, i);
                    break;
                }
            }

            return @this;
        }
        /// <summary>
        /// 删除数组中指定的最后一个值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="str"></param>
        public static T[] RemoveLast<T>(this T[] @this, T t)
        {
            for (int i = @this.Length - 1; i >= 0; i++)
            {
                if (object.Equals(@this[i], t))
                {
                    @this = Remove(@this, i);
                    break;
                }
            }

            return @this;
        }
        /// <summary>
        /// 删除数组中指定的值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="str"></param>
        public static T[] RemoveAll<T>(this T[] @this, T t)
        {
            for (int i = 0; i < @this.Length; i++)
                if (object.Equals(@this[i], t))
                    @this = Remove(@this, i);

            return @this;
        }
        /// <summary>
        /// 删除数组中指定的所有值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="strs"></param>
        public static T[] RemoveAll<T>(this T[] @this, params T[] ts)
        {
            foreach (T t in ts)
                @this = RemoveAll(@this, t);

            return @this;
        }
        /// <summary>
        /// 删除开头部分的默认值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="alsoEmptyString">是否同时删除吃空字符串</param>
        public static T[] TrimStart<T>(this T[] @this)
        {
            int i = 0;
            while (i < @this.Length && object.Equals(@this[i], default(T)))
                i++;
            if (i != 0)
            {
                Array.Copy(@this, i, @this, 0, @this.Length - i);
                Array.Resize(ref @this, @this.Length - i);
            }

            return @this;
        }
        /// <summary>
        /// 删除结尾部分的默认值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="alsoEmptyString">是否同时删除吃空字符串</param>
        public static T[] TrimEnd<T>(this T[] @this)
        {
            int i = @this.Length - 1;
            while (i >= 0 && object.Equals(@this[i], default(T)))
                i--;
            if (i != @this.Length - 1)
            {
                Array.Resize(ref @this, @this.Length - i);
            }
            return @this;
        }
        /// <summary>
        /// 删除开头和结尾部分的默认值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="alsoEmptyString"></param>
        public static string[] Trim<T>(this string[] @this)
        {
            return TrimEnd(TrimStart(@this));
        }

        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }
        #endregion //   WJ

        /// <summary>
        /// Combine two arrays into one.
        /// </summary>
        /// <typeparam name="T">Type of Array</typeparam>
        /// <param name="combineWith">Base array in which arrayToCombine will add.</param>
        /// <param name="arrayToCombine">Array to combine with Base array.</param>
        /// <returns></returns>
        /// <example>
        /// 	<code>
        /// 		int[] arrayOne = new[] { 1, 2, 3, 4 };
        /// 		int[] arrayTwo = new[] { 5, 6, 7, 8 };
        /// 		Array combinedArray = arrayOne.CombineArray<int>(arrayTwo);
        /// 	</code>
        /// </example>
        /// <remarks>
        /// 	Contributed by Mohammad Rahman, http://mohammad-rahman.blogspot.com/
        /// </remarks>
        public static T[] CombineArray<T>(this T[] combineWith, T[] arrayToCombine)
        {
            if (combineWith != default(T[]) && arrayToCombine != default(T[]))
            {
                int initialSize = combineWith.Length;
                Array.Resize<T>(ref combineWith, initialSize + arrayToCombine.Length);
                Array.Copy(arrayToCombine, arrayToCombine.GetLowerBound(0), combineWith, initialSize, arrayToCombine.Length);
            }
            return combineWith;
        }

        /// <summary>
        /// To clear the contents of the array.
        /// </summary>
        /// <param name="clear"> The array to clear</param>
        /// <returns>Cleared array</returns>
        /// <example>
        ///     <code>
        ///         Array array = Array.CreateInstance(typeof(string), 2);
        ///         array.SetValue("One", 0); array.SetValue("Two", 1);
        ///         Array arrayToClear = array.ClearAll();
        ///     </code>
        /// </example>
        /// <remarks>
        /// 	Contributed by Mohammad Rahman, http://mohammad-rahman.blogspot.com/
        /// </remarks>
        public static Array ClearAll(this Array clear)
        {
            if (clear != null)
                Array.Clear(clear, 0, clear.Length);
            return clear;
        }

        /// <summary>
        /// To clear the contents of the array.
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="clear"> The array to clear</param>
        /// <returns>Cleared array</returns>
        /// <example>
        ///     <code>
        ///         int[] result = new[] { 1, 2, 3, 4 }.ClearAll<int>();
        ///     </code>
        /// </example>
        /// <remarks>
        /// 	Contributed by Mohammad Rahman, http://mohammad-rahman.blogspot.com/
        /// </remarks>
        public static T[] ClearAll<T>(this T[] arrayToClear)
        {
            if (arrayToClear != null)
                for (int i = arrayToClear.GetLowerBound(0); i <= arrayToClear.GetUpperBound(0); ++i)
                    arrayToClear[i] = default(T);
            return arrayToClear;
        }

        /// <summary>
        /// Tests if the array is empty.
        /// </summary>
        /// <param name="array">The array to test.</param>
        /// <returns>True if the array is empty.</returns>
        public static bool IsEmpty(this Array array)
        {
            array.ExceptionIfNullOrEmpty(
                "The array cannot be null.",
                "array");

            return array.Length == 0;
        }
    }


}