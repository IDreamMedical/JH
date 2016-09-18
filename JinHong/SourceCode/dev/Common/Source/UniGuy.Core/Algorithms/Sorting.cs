using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.Algorithms
{
    /// <summary>
    /// 排序算法
    /// </summary>
    /// <remarks>
    /// 对Tektosyne.Collections.Sorting和UniGuy.Core.Extensions.Algorithms.Dsa.Sorting的扩展补充
    /// </remarks>
    public static class Sorting
    {
        #region Private methods
        private static int BinarySearchCore<T>(IList<T> list, int start, int count, T value, Comparison<T> comparison)
        {
            int left = start, right = start+count-1, middle = 0;

            while (left <= right)
            {
                middle = (left + right) / 2;
                int result = comparison(value, list[middle]);

                if (result == 0) return middle;
                if (result < 0) right = middle - 1;
                else left = middle + 1;
            }

            return ~Math.Max(left, middle);
        }
        #endregion

        #region BinarySearch
        public static int BinarySearch<T>(IList<T> list, int start, int count, T value) 
        {
            return BinarySearch<T>(list, start, count, value, null);
        }

        public static int BinarySearch<T>(IList<T> list, int start, int count, T value, Comparison<T> comparison)
        {
            if (list == null)
                throw new Exception("list");

            if (comparison == null)
                comparison = Comparer<T>.Default.Compare;

            return BinarySearchCore<T>(list, start, count, value, comparison);
        }
        #endregion

        #region BinaryInsertionSort

        /// <summary>
        /// 二分法插入排序
        /// 插入排序涉及到搜索, 这里修改传统的直接搜索为二分法搜索, 使时间复杂度由O (n^2) 下降为 O (n lg n);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparison"></param>
        public static void BinaryInsertionSort<T>(IList<T> list, Comparison<T> comparison)
        {
            if (list == null)
                throw new Exception("list");

            int count = list.Count;
            if (count < 2) return;

            if (comparison == null)
                comparison = Comparer<T>.Default.Compare;

            for (int i = 1; i < count; i++)
            {
                T value = list[i];
                int j = BinarySearch<T>(list, 0, i, value, comparison);
                if (j >= 0)
                    j++;
                else
                    j = ~j;
                
                if(j!=i)
                {
                    for (int k = i; k > j; k--)
                        list[k] = list[k - 1];
                    list[j] = value;
                }
            }
        }

        public static void BinaryInsertionSort<T>(IList<T> list)
        {
            BinaryInsertionSort(list, null);
        }
        #endregion
    }
}
