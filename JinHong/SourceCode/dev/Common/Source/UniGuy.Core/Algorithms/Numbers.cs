using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Algorithms
{
    public class Numbers
    {
        /// <summary>
        /// 求最大公约数
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static int GetGCDByModulus(int value1, int value2)
        {
            while (value1 != 0 && value2 != 0)
            {
                if (value1 > value2)
                    value1 %= value2;
                else
                    value2 %= value1;
            }
            return Math.Max(value1, value2);
        }

        /// <summary>
        /// 判断是否互质
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool Coprime(int value1, int value2)
        {
            return GetGCDByModulus(value1, value2) == 1;
        }

        /// <summary>
        /// 不通过引入第三个临时变量交换数据
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void AddSubSwap(ref int a, ref int b)
        {
            a = a + b;
            b = a - b;
            a = a - b;
        }

        /// <summary>
        /// We can implement the XOR swap algorithm very easily using C#'s logical bitwise operators and compound assignment. A method to achieve this is shown below. Note that the first statement checks if the two values are equal. This resolves the problem that if the two variables are using the same reference then the algorithm will fail. This happens because the first XOR zeroes both values.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void XorSwap(ref int a, ref int b)
        {
            if (a != b)
            {
                a ^= b;
                b ^= a;
                a ^= b;
            }
        }
    }
}
