using System;

namespace UniGuy.Core.Extensions
{
    /// <summary>
    /// 	Extension methods for the string data type
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// 	Performs the specified action n times based on the underlying int value.
        /// </summary>
        /// <param name = "value">The value.</param>
        /// <param name = "action">The action.</param>
        public static void Times(this int value, Action action)
        {
            for (var i = 0; i < value; i++)
                action();
        }

        /// <summary>
        /// 	Performs the specified action n times based on the underlying int value.
        /// </summary>
        /// <param name = "value">The value.</param>
        /// <param name = "action">The action.</param>
        public static void Times(this int value, Action<int> action)
        {
            for (var i = 0; i < value; i++)
                action(i);
        }

        /// <summary>
        /// 	Determines whether the value is even
        /// </summary>
        /// <param name = "value">The Value</param>
        /// <returns>true or false</returns>
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// 	Determines whether the value is odd
        /// </summary>
        /// <param name = "value">The Value</param>
        /// <returns>true or false</returns>
        public static bool IsOdd(this int value)
        {
            return !IsEven(value);
        }

        /// <summary>Checks whether the value is in range</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        public static bool InRange(this int value, int minValue, int maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }

        /// <summary>Checks whether the value is in range or returns the default value</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <param name="defaultValue">The default value</param>
        public static int InRange(this int value, int minValue, int maxValue, int defaultValue)
        {
            return value.InRange(minValue, maxValue) ? value : defaultValue;
        }
    }
}