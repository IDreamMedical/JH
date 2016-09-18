using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Extensions
{
    public static class BoolExtension
    {
        public static bool IsNullBool(this bool? @this)
        {
            return @this == null || !@this.HasValue;
        }

        /// <summary>
        /// 如果有布尔值则返回该布尔值, 否则返回False;
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool GetValueOrFalse(this bool? @this)
        {
            return (@this != null && @this.HasValue) ? @this.Value : false;
        }
        /// <summary>
        /// 如果有布尔值则返回该布尔值, 否则返回True;
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool GetValueOrTrue(this bool? @this)
        {
            return (@this != null && @this.HasValue) ? @this.Value : true;
        }

        /// <summary>
        /// 三态与
        /// AND     null    true    false
        /// null    null    null    false
        /// true    null    true    false
        /// false   false   false   false
        /// true AND null = null
        /// false AND null = false
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool? And(this bool? a, bool? b)
        {
            if (a == null)
                return b == false ? (bool?)false : null;
            return (a == true) ? b : a;
        }

        /// <summary>
        /// 三态或
        /// Or      null    true    false
        /// null    null    true    null
        /// true    true    true    true
        /// false   null    true   false
        /// true Or null = true
        /// false Or null = null
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool? Or(this bool? a, bool? b)
        {
            if (a == null)
                return b == true ? (bool?)true : null;
            return (a == true) ? a : b;
        }

        /// <summary>
        /// 三态或
        /// Or      null    true    false
        /// null    null    null    null
        /// true    null    true    true
        /// false   null    true   false
        /// true Xor null = null
        /// false Xor null = null
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool? Xor(this bool? a, bool? b)
        {
            if (a == null)
                return null;
            if (a == true)
                return (b == null) ? null : (bool?)true;
            return (b == null) ? null : b;
        }

        /// <summary>
        /// 三态非
        /// Not     null    null
        ///         true    false
        ///         false   true
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool? Not(this bool? a)
        {
            return a.IsNullBool() ? null : (bool?)a.Value;
        }
    }
}