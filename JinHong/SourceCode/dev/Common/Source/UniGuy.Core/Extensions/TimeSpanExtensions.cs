using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Extensions
{
    //  Author: WJ
    //  LastModified: 20110912
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// 毫秒部分清零
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TimeSpan TrimMilliseconds(this TimeSpan @this)
        {
            return new TimeSpan(@this.Days, @this.Hours, @this.Minutes, @this.Seconds);
        }
        /// <summary>
        /// 秒以后部分清零
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TimeSpan TrimSeconds(this TimeSpan @this)
        {
            return new TimeSpan(@this.Days, @this.Hours, @this.Minutes, 0);
        }
        /// <summary>
        /// 分钟以后部分清零
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TimeSpan TrimMinutes(this TimeSpan @this)
        {
            return new TimeSpan(@this.Days, @this.Hours, 0, 0);
        }
        /// <summary>
        /// 小时以后部分清零
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TimeSpan TrimHours(this TimeSpan @this)
        {
            return new TimeSpan(@this.Days, 0, 0, 0);
        }
    }
}