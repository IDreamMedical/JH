﻿using System;
using System.Globalization;

namespace UniGuy.Core.Extensions
{
    /// <summary>
    /// 	Extension methods for the DateTimeOffset data type.
    /// </summary>
    public static class DateTimeExtensions
    {
        const int EveningEnds = 2;
        const int MorningEnds = 12;
        const int AfternoonEnds = 6;
        static readonly DateTime Date1970 = new DateTime(1970, 1, 1);

        ///<summary>
        ///	Return System UTC Offset
        ///</summary>
        public static double UtcOffset
        {
            get { return DateTime.Now.Subtract(DateTime.UtcNow).TotalHours; }
        }

        /// <summary>
        /// 	Calculates the age based on today.
        /// </summary>
        /// <param name = "dateOfBirth">The date of birth.</param>
        /// <returns>The calculated age.</returns>
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            return CalculateAge(dateOfBirth, DateTime.Today);
        }

        /// <summary>
        /// 	Calculates the age based on a passed reference date.
        /// </summary>
        /// <param name = "dateOfBirth">The date of birth.</param>
        /// <param name = "referenceDate">The reference date to calculate on.</param>
        /// <returns>The calculated age.</returns>
        public static int CalculateAge(this DateTime dateOfBirth, DateTime referenceDate)
        {
            var years = referenceDate.Year - dateOfBirth.Year;
            if (referenceDate.Month < dateOfBirth.Month || (referenceDate.Month == dateOfBirth.Month && referenceDate.Day < dateOfBirth.Day))
                --years;
            return years;
        }

        /// <summary>
        /// 	Returns the number of days in the month of the provided date.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <returns>The number of days.</returns>
        public static int GetCountDaysOfMonth(this DateTime date)
        {
            var nextMonth = date.AddMonths(1);
            return new DateTime(nextMonth.Year, nextMonth.Month, 1).AddDays(-1).Day;
        }

        /// <summary>
        /// 	Returns the first day of the month of the provided date.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <returns>The first day of the month</returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 	Returns the first day of the month of the provided date.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <param name = "dayOfWeek">The desired day of week.</param>
        /// <returns>The first day of the month</returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.GetFirstDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
                dt = dt.AddDays(1);
            return dt;
        }

        /// <summary>
        /// 	Returns the last day of the month of the provided date.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <returns>The last day of the month.</returns>
        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, GetCountDaysOfMonth(date));
        }

        /// <summary>
        /// 	Returns the last day of the month of the provided date.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <param name = "dayOfWeek">The desired day of week.</param>
        /// <returns>The date time</returns>
        public static DateTime GetLastDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.GetLastDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
                dt = dt.AddDays(-1);
            return dt;
        }

        /// <summary>
        /// 	Indicates whether the date is today.
        /// </summary>
        /// <param name = "dt">The date.</param>
        /// <returns>
        /// 	<c>true</c> if the specified date is today; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsToday(this DateTime dt)
        {
            return (dt.Date == DateTime.Today);
        }

        /// <summary>
        /// 	Sets the time on the specified DateTime value.
        /// </summary>
        /// <param name = "date">The base date.</param>
        /// <param name = "hours">The hours to be set.</param>
        /// <param name = "minutes">The minutes to be set.</param>
        /// <param name = "seconds">The seconds to be set.</param>
        /// <returns>The DateTime including the new time value</returns>
        public static DateTime SetTime(this DateTime date, int hours, int minutes, int seconds)
        {
            return date.SetTime(new TimeSpan(hours, minutes, seconds));
        }

        /// <summary>
        /// 	Sets the time on the specified DateTime value.
        /// </summary>
        /// <param name = "date">The base date.</param>
        /// <param name = "time">The TimeSpan to be applied.</param>
        /// <returns>
        /// 	The DateTime including the new time value
        /// </returns>
        public static DateTime SetTime(this DateTime date, TimeSpan time)
        {
            return date.Date.Add(time);
        }

        /// <summary>
        /// 	Converts a DateTime into a DateTimeOffset using the local system time zone.
        /// </summary>
        /// <param name = "localDateTime">The local DateTime.</param>
        /// <returns>The converted DateTimeOffset</returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime localDateTime)
        {
            return localDateTime.ToDateTimeOffset(null);
        }

        /// <summary>
        /// 	Converts a DateTime into a DateTimeOffset using the specified time zone.
        /// </summary>
        /// <param name = "localDateTime">The local DateTime.</param>
        /// <param name = "localTimeZone">The local time zone.</param>
        /// <returns>The converted DateTimeOffset</returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime localDateTime, TimeZoneInfo localTimeZone)
        {
            localTimeZone = (localTimeZone ?? TimeZoneInfo.Local);

            if (localDateTime.Kind != DateTimeKind.Unspecified)
                localDateTime = new DateTime(localDateTime.Ticks, DateTimeKind.Unspecified);

            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, localTimeZone);
        }

        /// <summary>
        /// 	Gets the first day of the week using the current culture.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <returns>The first day of the week</returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            return date.GetFirstDayOfWeek(null);
        }

        /// <summary>
        /// 	Gets the first day of the week using the specified culture.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <param name = "cultureInfo">The culture to determine the first weekday of a week.</param>
        /// <returns>The first day of the week</returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            cultureInfo = (cultureInfo ?? CultureInfo.CurrentCulture);

            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            while (date.DayOfWeek != firstDayOfWeek)
                date = date.AddDays(-1);

            return date;
        }

        /// <summary>
        /// 	Gets the last day of the week using the current culture.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <returns>The first day of the week</returns>
        public static DateTime GetLastDayOfWeek(this DateTime date)
        {
            return date.GetLastDayOfWeek(null);
        }

        /// <summary>
        /// 	Gets the last day of the week using the specified culture.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <param name = "cultureInfo">The culture to determine the first weekday of a week.</param>
        /// <returns>The first day of the week</returns>
        public static DateTime GetLastDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            return date.GetFirstDayOfWeek(cultureInfo).AddDays(6);
        }

        /// <summary>
        /// 	Gets the next occurence of the specified weekday within the current week using the current culture.
        /// </summary>
        /// <param name = "date">The base date.</param>
        /// <param name = "weekday">The desired weekday.</param>
        /// <returns>The calculated date.</returns>
        /// <example>
        /// 	<code>
        /// 		var thisWeeksMonday = DateTime.Now.GetWeekday(DayOfWeek.Monday);
        /// 	</code>
        /// </example>
        public static DateTime GetWeeksWeekday(this DateTime date, DayOfWeek weekday)
        {
            return date.GetWeeksWeekday(weekday, null);
        }

        /// <summary>
        /// 	Gets the next occurence of the specified weekday within the current week using the specified culture.
        /// </summary>
        /// <param name = "date">The base date.</param>
        /// <param name = "weekday">The desired weekday.</param>
        /// <param name = "cultureInfo">The culture to determine the first weekday of a week.</param>
        /// <returns>The calculated date.</returns>
        /// <example>
        /// 	<code>
        /// 		var thisWeeksMonday = DateTime.Now.GetWeekday(DayOfWeek.Monday);
        /// 	</code>
        /// </example>
        public static DateTime GetWeeksWeekday(this DateTime date, DayOfWeek weekday, CultureInfo cultureInfo)
        {
            var firstDayOfWeek = date.GetFirstDayOfWeek(cultureInfo);
            return firstDayOfWeek.GetNextWeekday(weekday);
        }

        /// <summary>
        /// 	Gets the next occurence of the specified weekday.
        /// </summary>
        /// <param name = "date">The base date.</param>
        /// <param name = "weekday">The desired weekday.</param>
        /// <returns>The calculated date.</returns>
        /// <example>
        /// 	<code>
        /// 		var lastMonday = DateTime.Now.GetNextWeekday(DayOfWeek.Monday);
        /// 	</code>
        /// </example>
        public static DateTime GetNextWeekday(this DateTime date, DayOfWeek weekday)
        {
            while (date.DayOfWeek != weekday)
                date = date.AddDays(1);
            return date;
        }

        /// <summary>
        /// 	Gets the previous occurence of the specified weekday.
        /// </summary>
        /// <param name = "date">The base date.</param>
        /// <param name = "weekday">The desired weekday.</param>
        /// <returns>The calculated date.</returns>
        /// <example>
        /// 	<code>
        /// 		var lastMonday = DateTime.Now.GetPreviousWeekday(DayOfWeek.Monday);
        /// 	</code>
        /// </example>
        public static DateTime GetPreviousWeekday(this DateTime date, DayOfWeek weekday)
        {
            while (date.DayOfWeek != weekday)
                date = date.AddDays(-1);
            return date;
        }

        /// <summary>
        /// 	Determines whether the date only part of twi DateTime values are equal.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <param name = "dateToCompare">The date to compare with.</param>
        /// <returns>
        /// 	<c>true</c> if both date values are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDateEqual(this DateTime date, DateTime dateToCompare)
        {
            return (date.Date == dateToCompare.Date);
        }

        /// <summary>
        /// 	Determines whether the time only part of two DateTime values are equal.
        /// </summary>
        /// <param name = "time">The time.</param>
        /// <param name = "timeToCompare">The time to compare.</param>
        /// <returns>
        /// 	<c>true</c> if both time values are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsTimeEqual(this DateTime time, DateTime timeToCompare)
        {
            return (time.TimeOfDay == timeToCompare.TimeOfDay);
        }

        /// <summary>
        /// 	Get milliseconds of UNIX area. This is the milliseconds since 1/1/1970
        /// </summary>
        /// <param name = "datetime">Up to which time.</param>
        /// <returns>number of milliseconds.</returns>
        /// <remarks>
        /// 	Contributed by blaumeister, http://www.codeplex.com/site/users/view/blaumeiser
        /// </remarks>
        public static long GetMillisecondsSince1970(this DateTime datetime)
        {
            var ts = datetime.Subtract(Date1970);
            return (long)ts.TotalMilliseconds;
        }

        /// <summary>
        /// 	Indicates whether the specified date is a weekend (Saturday or Sunday).
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <returns>
        /// 	<c>true</c> if the specified date is a weekend; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek.EqualsAny(DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        /// <summary>
        /// 	Adds the specified amount of weeks (=7 days gregorian calendar) to the passed date value.
        /// </summary>
        /// <param name = "date">The origin date.</param>
        /// <param name = "value">The amount of weeks to be added.</param>
        /// <returns>The enw date value</returns>
        public static DateTime AddWeeks(this DateTime date, int value)
        {
            return date.AddDays(value * 7);
        }

        ///<summary>
        ///	Get the number of days within that year.
        ///</summary>
        ///<param name = "year">The year.</param>
        ///<returns>the number of days within that year</returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static int GetDays(int year)
        {
            var first = new DateTime(year, 1, 1);
            var last = new DateTime(year + 1, 1, 1);
            return GetDays(first, last);
        }

        ///<summary>
        ///	Get the number of days within that date year.
        ///</summary>
        ///<param name = "date">The date.</param>
        ///<returns>the number of days within that year</returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static int GetDays(this DateTime date)
        {
            return GetDays(date.Year);
        }

        ///<summary>
        ///	Get the number of days between two dates.
        ///</summary>
        ///<param name = "fromDate">The origin year.</param>
        ///<param name = "toDate">To year</param>
        ///<returns>The number of days between the two years</returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static int GetDays(this DateTime fromDate, DateTime toDate)
        {
            return Convert.ToInt32(toDate.Subtract(fromDate).TotalDays);
        }

        ///<summary>
        ///	Return a period "Morning", "Afternoon", or "Evening"
        ///</summary>
        ///<param name = "date">The date.</param>
        ///<returns>The period "morning", "afternoon", or "evening"</returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static string GetPeriodOfDay(this DateTime date)
        {
            var hour = date.Hour;
            if (hour < EveningEnds)
                return "evening";
            if (hour < MorningEnds)
                return "morning";
            return hour < AfternoonEnds ? "afternoon" : "evening";
        }

        /// <summary>
        /// Gets the week number for a provided date time value based on the current culture settings.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The week number</returns>
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            var culture = CultureInfo.CurrentUICulture;
            var calendar = culture.Calendar;
            var dateTimeFormat = culture.DateTimeFormat;

            return calendar.GetWeekOfYear(dateTime, dateTimeFormat.CalendarWeekRule, dateTimeFormat.FirstDayOfWeek);
        }

        #region WJ added
        /// <summary>
        /// 毫秒部分清零
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime TrimMilliseconds(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute, @this.Second);
        }
        /// <summary>
        /// 秒以后部分清零
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime TrimSeconds(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute, 0);
        }
        /// <summary>
        /// 分钟以后部分清零
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime TrimMinutes(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day, @this.Hour, 0, 0);
        }
        /// <summary>
        /// 小时以后部分清零
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime TrimHours(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day);
        }
        /// <summary>
        /// 获得时间日所在月的索引
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int GetDayOfMonth(this DateTime @this)
        {
            return (@this - @this.GetFirstDayOfMonth()).Days;
        }
        /// <summary>
        /// 获得时间月所在年的索引
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int GetMonthOfYear(this DateTime @this)
        {
            return @this.Month - 1;
        }
        #endregion //   WJ added

        #region Edge.Extensions
        //  http://edgeextensions.codeplex.com/SourceControl/list/changesets
        /// <summary>
        /// Adds one month to the specified date.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing one month after of the specified date.</returns>
        public static DateTime AddMonth(this DateTime value)
        {
            switch (value.Month)
            {
                case 1:
                    if (value.IsLeapYear() == true & value.Day > 29)
                    {
                        return new DateTime(value.Year, 2, 29);
                    }
                    else if (value.Day > 28)
                    {
                        return new DateTime(value.Year, 2, 28);
                    }
                    else
                    {
                        return new DateTime(value.Year, 2, value.Day);
                    }

                case 2:
                    return new DateTime(value.Year, 3, value.Day);
                case 3:
                case 5:
                case 8:
                case 10:
                    if (value.Day > 30)
                    {
                        return new DateTime(value.Year, value.Month + 1, 30);
                    }
                    else
                    {
                        return new DateTime(value.Year, value.Month + 1, value.Day);
                    }

                case 4:
                case 6:
                case 7:
                case 9:
                case 11:
                    return new DateTime(value.Year, value.Month + 1, value.Day);
                case 12:
                    return new DateTime(value.Year + 1, 1, value.Day);
            }

            return value;
        }

        /// <summary>
        /// Adds one week to the specified <see cref="DateTime">DateTime</see> value.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing one weeek after the specified <see cref="DateTime">DateTime</see>.</returns>
        public static DateTime AddWeek(this DateTime value)
        {
            return value.AddWeeks(1);
        }

        /// <summary>
        /// Adds the specified number of weeks to the specified <see cref="DateTime">DateTime</see> value.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <param name="numberOfWeeks">The number of weeks to add to the specified value.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing the specified number of weeks after the specified <see cref="DateTime">DateTime</see>.</returns>
        public static DateTime AddWeeks_2(this DateTime value, int numberOfWeeks)
        {
            return value.Add(new TimeSpan(7 * numberOfWeeks, 0, 0, 0));
        }

        /// <summary>
        /// Gets the age of the <see cref="DateTime">DateTime</see> value.
        /// If the specified value is after the current date, the method will return 0.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <returns>The age of the value.</returns>
        public static short Age(this DateTime value)
        {
            if (value <= DateTime.Now)
            {
                if (value.Month < DateTime.Now.Month)
                {
                    return Convert.ToInt16(value.DifferenceFrom(DateTime.Now, DateInterval.Year));
                }
                else if (value.Month == DateTime.Now.Month)
                {
                    if (value.Day <= DateTime.Now.Day)
                    {
                        return Convert.ToInt16(value.DifferenceFrom(DateTime.Now, DateInterval.Year));
                    }
                    else
                    {
                        return Convert.ToInt16(value.DifferenceFrom(DateTime.Now, DateInterval.Year) - 1);
                    }
                }
                else
                {
                    return Convert.ToInt16(value.DifferenceFrom(DateTime.Now, DateInterval.Year) - 1);
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the age of the <see cref="DateTime">DateTime</see> value.
        /// If the specified value is after the current date, the method will return a negative number.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <returns>The age of the value.</returns>
        public static short Age2(this DateTime value)
        {
            if (value.Month < DateTime.Now.Month)
            {
                return Convert.ToInt16(value.DifferenceFrom(DateTime.Now, DateInterval.Year));
            }
            else if (value.Month == DateTime.Now.Month)
            {
                if (value.Day <= DateTime.Now.Day)
                {
                    return Convert.ToInt16(value.DifferenceFrom(DateTime.Now, DateInterval.Year));
                }
                else
                {
                    return Convert.ToInt16(value.DifferenceFrom(DateTime.Now, DateInterval.Year) - 1);
                }
            }
            else
            {
                return Convert.ToInt16(value.DifferenceFrom(DateTime.Now, DateInterval.Year) - 1);
            }
        }

        /// <summary>
        /// Gets the number of days since the specified date.
        /// </summary>
        /// <param name="value">The date to compute.</param>
        /// <returns>The number of days since the specified date.</returns>
        public static int DaysSince(this DateTime value)
        {
            return DateTime.Now.Subtract(value).Days;
        }

        /// <summary>
        /// Gets the difference between two dates by using the specified interval unit.
        /// </summary>
        /// <param name="value">First date to compare.</param>
        /// <param name="date">Second date to compare.</param>
        /// <param name="interval">Date interval unit.</param>
        /// <returns>Difference between the dates.</returns>
        public static long DifferenceFrom(this DateTime value, DateTime date, DateInterval interval)
        {
            if (interval == DateInterval.Year)
            {
                return date.Year - value.Year;
            }

            if (interval == DateInterval.Month)
            {
                return (date.Month - value.Month) + (12 * (date.Year - value.Year));
            }

            TimeSpan ts = date - value;

            if (interval == DateInterval.Day || interval == DateInterval.DayOfYear)
            {
                return ts.TotalDays.Round();
            }

            if (interval == DateInterval.Hour)
            {
                return ts.TotalHours.Round();
            }

            if (interval == DateInterval.Minute)
            {
                return ts.TotalMinutes.Round();
            }

            if (interval == DateInterval.Second)
            {
                return ts.TotalSeconds.Round();
            }

            if (interval == DateInterval.Weekday)
            {
                return (ts.TotalDays / 7.0).Round();
            }

            if (interval == DateInterval.WeekOfYear)
            {
                while (date.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                {
                    date = date.AddDays(-1);
                }

                while (value.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                {
                    value = value.AddDays(-1);
                }

                ts = date - value;

                return (ts.TotalDays / 7.0).Round();
            }

            if (interval == DateInterval.Quarter)
            {
                double valueQuarter = DateTimeUtilities.GetQuarter(value.Month);
                double dateQuarter = DateTimeUtilities.GetQuarter(date.Month);
                double valueDiff = dateQuarter - valueQuarter;
                double dateDiff = 4 * (date.Year - value.Year);

                return (valueDiff + dateDiff).Round();
            }

            return 0;
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> representing 23:59:59 on the specified date.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing 23:59:59 on the specified date.</returns>
        public static DateTime EndOfDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> object representing the first calendar day in the fiscal year for the specified date.
        /// </summary>
        /// <param name="value">The date to be evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> object representing the first calendar day in the fiscal year for the specified date.</returns>
        public static DateTime FirstCalendarDayOfCurrentFiscalYear(this DateTime value)
        {
            return new DateTime(value.FiscalYear() - 1, 10, 1);
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> object representing the first calendar day in the previous fiscal year for the specified date.
        /// </summary>
        /// <param name="value">The date to be evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> object representing the first calendar day in the previous fiscal year for the specified date.</returns>
        public static DateTime FirstCalendarDayOfPreviousFiscalYear(this DateTime value)
        {
            if (value.FiscalYear() == DateTime.Now.Year)
            {
                return new DateTime(value.FiscalYear() - 2, 10, 1);
            }
            else
            {
                return new DateTime(value.FiscalYear() - 3, 10, 1);
            }
        }

        /// <summary>
        /// Gets the first day of the current quarter.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>The first day of the current quarter.</returns>
        public static DateTime FirstDayOfCurrentQuarter(this DateTime value)
        {
            switch (value.Month)
            {
                case 1:
                case 2:
                case 3:
                    return new DateTime(value.Year, 1, 1);
                case 4:
                case 5:
                case 6:
                    return new DateTime(value.Year, 4, 1);
                case 7:
                case 8:
                case 9:
                    return new DateTime(value.Year, 7, 1);
                case 10:
                case 11:
                case 12:
                    return new DateTime(value.Year, 10, 1);
            }

            return value;
        }

        /// <summary>
        /// Gets the first day in the month for the given date.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing the first day in the month for the specified date.</returns>
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return value.AddDays(1 - value.Day);
        }

        /// <summary>
        /// Gets the first instance of the specified day in the specified month.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <param name="dayOfWeek">The day of week to return.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing the specified first day of the week in the month for the specified date.</returns>
        public static DateTime FirstDayOfMonth(this DateTime value, DayOfWeek dayOfWeek)
        {
            switch (value.FirstDayOfMonth().DayOfWeek)
            {
                case System.DayOfWeek.Sunday:
                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.FirstDayOfMonth();
                        case System.DayOfWeek.Monday:
                            return value.FirstDayOfMonth().AddDays(1);
                        case System.DayOfWeek.Tuesday:
                            return value.FirstDayOfMonth().AddDays(2);
                        case System.DayOfWeek.Wednesday:
                            return value.FirstDayOfMonth().AddDays(3);
                        case System.DayOfWeek.Thursday:
                            return value.FirstDayOfMonth().AddDays(4);
                        case System.DayOfWeek.Friday:
                            return value.FirstDayOfMonth().AddDays(5);
                        case System.DayOfWeek.Saturday:
                            return value.FirstDayOfMonth().AddDays(6);
                    }

                    break;
                case System.DayOfWeek.Monday:
                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.FirstDayOfMonth().AddDays(6);
                        case System.DayOfWeek.Monday:
                            return value.FirstDayOfMonth();
                        case System.DayOfWeek.Tuesday:
                            return value.FirstDayOfMonth().AddDays(1);
                        case System.DayOfWeek.Wednesday:
                            return value.FirstDayOfMonth().AddDays(2);
                        case System.DayOfWeek.Thursday:
                            return value.FirstDayOfMonth().AddDays(3);
                        case System.DayOfWeek.Friday:
                            return value.FirstDayOfMonth().AddDays(4);
                        case System.DayOfWeek.Saturday:
                            return value.FirstDayOfMonth().AddDays(5);
                    }

                    break;
                case System.DayOfWeek.Tuesday:
                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.FirstDayOfMonth().AddDays(5);
                        case System.DayOfWeek.Monday:
                            return value.FirstDayOfMonth().AddDays(6);
                        case System.DayOfWeek.Tuesday:
                            return value.FirstDayOfMonth();
                        case System.DayOfWeek.Wednesday:
                            return value.FirstDayOfMonth().AddDays(1);
                        case System.DayOfWeek.Thursday:
                            return value.FirstDayOfMonth().AddDays(2);
                        case System.DayOfWeek.Friday:
                            return value.FirstDayOfMonth().AddDays(3);
                        case System.DayOfWeek.Saturday:
                            return value.FirstDayOfMonth().AddDays(4);
                    }

                    break;

                case System.DayOfWeek.Wednesday:

                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.FirstDayOfMonth().AddDays(4);
                        case System.DayOfWeek.Monday:
                            return value.FirstDayOfMonth().AddDays(5);
                        case System.DayOfWeek.Tuesday:
                            return value.FirstDayOfMonth().AddDays(6);
                        case System.DayOfWeek.Wednesday:
                            return value.FirstDayOfMonth();
                        case System.DayOfWeek.Thursday:
                            return value.FirstDayOfMonth().AddDays(1);
                        case System.DayOfWeek.Friday:
                            return value.FirstDayOfMonth().AddDays(2);
                        case System.DayOfWeek.Saturday:
                            return value.FirstDayOfMonth().AddDays(3);
                    }

                    break;

                case System.DayOfWeek.Thursday:

                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.FirstDayOfMonth().AddDays(3);
                        case System.DayOfWeek.Monday:
                            return value.FirstDayOfMonth().AddDays(4);
                        case System.DayOfWeek.Tuesday:
                            return value.FirstDayOfMonth().AddDays(5);
                        case System.DayOfWeek.Wednesday:
                            return value.FirstDayOfMonth().AddDays(6);
                        case System.DayOfWeek.Thursday:
                            return value.FirstDayOfMonth();
                        case System.DayOfWeek.Friday:
                            return value.FirstDayOfMonth().AddDays(1);
                        case System.DayOfWeek.Saturday:
                            return value.FirstDayOfMonth().AddDays(2);
                    }

                    break;

                case System.DayOfWeek.Friday:
                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.FirstDayOfMonth().AddDays(2);
                        case System.DayOfWeek.Monday:
                            return value.FirstDayOfMonth().AddDays(3);
                        case System.DayOfWeek.Tuesday:
                            return value.FirstDayOfMonth().AddDays(4);
                        case System.DayOfWeek.Wednesday:
                            return value.FirstDayOfMonth().AddDays(5);
                        case System.DayOfWeek.Thursday:
                            return value.FirstDayOfMonth().AddDays(6);
                        case System.DayOfWeek.Friday:
                            return value.FirstDayOfMonth();
                        case System.DayOfWeek.Saturday:
                            return value.FirstDayOfMonth().AddDays(1);
                    }

                    break;

                case System.DayOfWeek.Saturday:

                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.FirstDayOfMonth().AddDays(1);
                        case System.DayOfWeek.Monday:
                            return value.FirstDayOfMonth().AddDays(2);
                        case System.DayOfWeek.Tuesday:
                            return value.FirstDayOfMonth().AddDays(3);
                        case System.DayOfWeek.Wednesday:
                            return value.FirstDayOfMonth().AddDays(4);
                        case System.DayOfWeek.Thursday:
                            return value.FirstDayOfMonth().AddDays(5);
                        case System.DayOfWeek.Friday:
                            return value.FirstDayOfMonth().AddDays(6);
                        case System.DayOfWeek.Saturday:
                            return value.FirstDayOfMonth();
                    }

                    break;
            }

            return value;
        }

        /// <summary>
        /// Gets the first day of the previous quarter.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>The first day of the previous government's fiscal quarter.</returns>
        public static DateTime FirstDayOfPreviousQuarter(this DateTime value)
        {
            switch (value.Month)
            {
                case 1:
                case 2:
                case 3:
                    return new DateTime(value.Year - 1, 10, 1);
                case 4:
                case 5:
                case 6:
                    return new DateTime(value.Year, 1, 1);
                case 7:
                case 8:
                case 9:
                    return new DateTime(value.Year, 4, 1);
                case 10:
                case 11:
                case 12:
                    return new DateTime(value.Year, 7, 1);
            }

            return value;
        }

        /// <summary>
        /// Gets the first day of the week (Sunday) for the given date.
        /// </summary>
        /// <param name="value">The date to use for determining the first day of the week.</param>
        /// <returns>The first day of the week for the given date.</returns>
        /// <remarks>Assumes that Sunday is the first day of the week.</remarks>
        public static DateTime FirstDayOfWeek(this DateTime value)
        {
            // Variable Declarations
            System.DateTime dteReturnvalue = default(System.DateTime);

            // Determine the First day of the specified week
            try
            {
                switch (value.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dteReturnvalue = value;
                        break;
                    case DayOfWeek.Monday:
                        dteReturnvalue = value.Subtract(new TimeSpan(1, 0, 0, 0));
                        break;
                    case DayOfWeek.Tuesday:
                        dteReturnvalue = value.Subtract(new TimeSpan(2, 0, 0, 0));
                        break;
                    case DayOfWeek.Wednesday:
                        dteReturnvalue = value.Subtract(new TimeSpan(3, 0, 0, 0));
                        break;
                    case DayOfWeek.Thursday:
                        dteReturnvalue = value.Subtract(new TimeSpan(4, 0, 0, 0));
                        break;
                    case DayOfWeek.Friday:
                        dteReturnvalue = value.Subtract(new TimeSpan(5, 0, 0, 0));
                        break;
                    case DayOfWeek.Saturday:
                        dteReturnvalue = value.Subtract(new TimeSpan(6, 0, 0, 0));
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dteReturnvalue;
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> object representing the first work day in the fiscal year for the specified date.
        /// </summary>
        /// <param name="value">The date to be evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> object representing the first work day in the fiscal year for the specified date.</returns>
        public static DateTime FirstWorkDayOfCurrentFiscalYear(this DateTime value)
        {
            switch (value.FirstCalendarDayOfCurrentFiscalYear().DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return value.FirstCalendarDayOfCurrentFiscalYear().AddDays(1);
                case DayOfWeek.Saturday:
                    return value.FirstCalendarDayOfCurrentFiscalYear().AddDays(2);
                default:
                    return value.FirstCalendarDayOfCurrentFiscalYear();
            }
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> object representing the first work day in the previous fiscal year for the specified date.
        /// </summary>
        /// <param name="value">The date to be evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> object representing the first work day in the previous fiscal year for the specified date.</returns>
        public static DateTime FirstWorkDayOfPreviousFiscalYear(this DateTime value)
        {
            switch (value.FirstCalendarDayOfPreviousFiscalYear().DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return value.FirstCalendarDayOfPreviousFiscalYear().AddDays(1);
                case DayOfWeek.Saturday:
                    return value.FirstCalendarDayOfPreviousFiscalYear().AddDays(2);
                default:
                    return value.FirstCalendarDayOfPreviousFiscalYear();
            }
        }

        /// <summary>
        /// Gets the government's fiscal year for the specified date.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>The government's fiscal year.</returns>
        public static short FiscalYear(this DateTime value)
        {
            // Variable Declarations
            short shtReturnvalue = 0;

            // Determine the fiscal year
            try
            {
                if (value.Month > 9)
                {
                    shtReturnvalue = Convert.ToInt16(value.Year + 1);
                }
                else
                {
                    shtReturnvalue = Convert.ToInt16(value.Year);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return shtReturnvalue;
        }

        /// <summary>
        /// Attempts the supplied date/time <see cref="string">string</see> value.
        /// </summary>
        /// <param name="value">The date to format.</param>
        /// <param name="includeTime">If set to <c>True</c> the time will be included in the formatted <see cref="string">string</see>.</param>
        /// <returns>A date formatted as dd-MMM-yyyy HH:mm:ss.</returns>
        /// <remarks>If the passed in <paramref name="value">value</paramref> is not a date, the method returns "N/A".</remarks>
        public static string FormatDateTime(this string value, bool includeTime)
        {
            if (value.IsDate())
            {
                return DateTime.Parse(value).ToDODString(includeTime);
            }
            else
            {
                return "N/A";
            }
        }

        /// <summary>
        /// Converts a fractional hour value like 1.25 to 1:15  hours:minutes format
        /// </summary>
        /// <param name="hours">Decimal hour value</param>
        /// <returns>The resulting string.</returns>
        public static string FractionalHoursToString(this decimal hours)
        {
            return hours.FractionalHoursToString(null);
        }

        /// <summary>
        /// Converts a fractional hour value to the specified format
        /// </summary>
        /// <param name="hours">Decimal hour value</param>
        /// <param name="format">An optional format string where {0} is hours and {1} is minutes.</param>
        /// <returns>The resulting string.</returns>
        public static string FractionalHoursToString(this decimal hours, string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "{0}:{1}";
            }

            TimeSpan tspan = TimeSpan.FromHours((double)hours);

            return string.Format(format, tspan.Hours, tspan.Minutes);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DateTime">DateTime</see> is in a leap year.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is in a leap year; otherwise, <c>false</c>.</returns>
        public static bool IsLeapYear(this DateTime value)
        {
            return System.DateTime.IsLeapYear(value.Year);
        }

        /// <summary>
        /// Returns whether the DateTime falls on a weekday.
        /// </summary>
        /// <param name="date">The date to be processed.</param>
        /// <returns>Whether the specified date occurs on a weekday.</returns>
        public static bool IsWeekDay(this DateTime date)
        {
            return !date.IsWeekend();
        }

        /// <summary>
        /// Lets you easily figure out if value of this instance a date value that is a weekend.
        /// </summary>
        /// <param name="value">DateTime to inspect.</param>
        /// <returns>Result of inspection.</returns>
        public static bool IsWeekend_2(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday;
        }

        /// <summary>
        /// Gets the julian date (as defined by DOD) for the specified date value.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>The julian date (as specified by DOD) for the specified date value.</returns>
        public static string JulianDate(this DateTime value)
        {
            long lngDateDiff = new DateTime(value.Year - 1, 12, 31).DifferenceFrom(value, DateInterval.Day);
            if (lngDateDiff < 10)
            {
                return value.Year.ToString(System.Globalization.CultureInfo.InvariantCulture) + "00" + lngDateDiff.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            else if (lngDateDiff < 100)
            {
                return value.Year.ToString(System.Globalization.CultureInfo.InvariantCulture) + "0" + lngDateDiff.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                return value.Year.ToString(System.Globalization.CultureInfo.InvariantCulture) + lngDateDiff.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> object representing the first calendar day in the fiscal year for the specified date.
        /// </summary>
        /// <param name="value">The date to be evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> object representing the first calendar day in the fiscal year for the specified date.</returns>
        public static DateTime LastCalendarDayOfCurrentFiscalYear(this DateTime value)
        {
            return new DateTime(value.FiscalYear(), 9, System.DateTime.DaysInMonth(value.FiscalYear(), 9));
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> object representing the first calendar day in the previous fiscal year for the specified date.
        /// </summary>
        /// <param name="value">The date to be evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> object representing the last calendar day in the previous fiscal year for the specified date.</returns>
        public static DateTime LastCalendarDayOfPreviousFiscalYear(this DateTime value)
        {
            return new DateTime(value.FiscalYear() - 1, 9, System.DateTime.DaysInMonth(value.FiscalYear() - 1, 9));
        }

        /// <summary>
        /// Gets the last day in the month for the given date.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing the last day in the month for the specified date.</returns>
        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return value.FirstDayOfMonth().AddDays(DateTime.DaysInMonth(value.Year, value.Month) - 1);
        }

        /// <summary>
        /// Gets the last specified day of the week in the month for the given date.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <param name="dayOfWeek">The day of week to return.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing the specified last day of the week in the month for the specified date.</returns>
        public static DateTime LastDayOfMonth(this DateTime value, DayOfWeek dayOfWeek)
        {
            switch (value.LastDayOfMonth().DayOfWeek)
            {
                case System.DayOfWeek.Sunday:
                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.LastDayOfMonth();
                        case System.DayOfWeek.Monday:
                            return value.LastDayOfMonth().AddDays(-6);
                        case System.DayOfWeek.Tuesday:
                            return value.LastDayOfMonth().AddDays(-5);
                        case System.DayOfWeek.Wednesday:
                            return value.LastDayOfMonth().AddDays(-4);
                        case System.DayOfWeek.Thursday:
                            return value.LastDayOfMonth().AddDays(-3);
                        case System.DayOfWeek.Friday:
                            return value.LastDayOfMonth().AddDays(-2);
                        case System.DayOfWeek.Saturday:
                            return value.LastDayOfMonth().AddDays(-1);
                    }

                    break;

                case System.DayOfWeek.Monday:

                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.LastDayOfMonth().AddDays(-1);
                        case System.DayOfWeek.Monday:
                            return value.LastDayOfMonth();
                        case System.DayOfWeek.Tuesday:
                            return value.LastDayOfMonth().AddDays(-6);
                        case System.DayOfWeek.Wednesday:
                            return value.LastDayOfMonth().AddDays(-5);
                        case System.DayOfWeek.Thursday:
                            return value.LastDayOfMonth().AddDays(-4);
                        case System.DayOfWeek.Friday:
                            return value.LastDayOfMonth().AddDays(-3);
                        case System.DayOfWeek.Saturday:
                            return value.LastDayOfMonth().AddDays(-2);
                    }

                    break;

                case System.DayOfWeek.Tuesday:

                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.LastDayOfMonth().AddDays(-2);
                        case System.DayOfWeek.Monday:
                            return value.LastDayOfMonth().AddDays(-1);
                        case System.DayOfWeek.Tuesday:
                            return value.LastDayOfMonth();
                        case System.DayOfWeek.Wednesday:
                            return value.LastDayOfMonth().AddDays(-6);
                        case System.DayOfWeek.Thursday:
                            return value.LastDayOfMonth().AddDays(-5);
                        case System.DayOfWeek.Friday:
                            return value.LastDayOfMonth().AddDays(-4);
                        case System.DayOfWeek.Saturday:
                            return value.LastDayOfMonth().AddDays(-3);
                    }

                    break;

                case System.DayOfWeek.Wednesday:

                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.LastDayOfMonth().AddDays(-3);
                        case System.DayOfWeek.Monday:
                            return value.LastDayOfMonth().AddDays(-2);
                        case System.DayOfWeek.Tuesday:
                            return value.LastDayOfMonth().AddDays(-1);
                        case System.DayOfWeek.Wednesday:
                            return value.LastDayOfMonth();
                        case System.DayOfWeek.Thursday:
                            return value.LastDayOfMonth().AddDays(-6);
                        case System.DayOfWeek.Friday:
                            return value.LastDayOfMonth().AddDays(-5);
                        case System.DayOfWeek.Saturday:
                            return value.LastDayOfMonth().AddDays(-4);
                    }

                    break;

                case System.DayOfWeek.Thursday:

                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.LastDayOfMonth().AddDays(-4);
                        case System.DayOfWeek.Monday:
                            return value.LastDayOfMonth().AddDays(-3);
                        case System.DayOfWeek.Tuesday:
                            return value.LastDayOfMonth().AddDays(-2);
                        case System.DayOfWeek.Wednesday:
                            return value.LastDayOfMonth().AddDays(-1);
                        case System.DayOfWeek.Thursday:
                            return value.LastDayOfMonth();
                        case System.DayOfWeek.Friday:
                            return value.LastDayOfMonth().AddDays(-6);
                        case System.DayOfWeek.Saturday:
                            return value.LastDayOfMonth().AddDays(-5);
                    }

                    break;

                case System.DayOfWeek.Friday:

                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.LastDayOfMonth().AddDays(-5);
                        case System.DayOfWeek.Monday:
                            return value.LastDayOfMonth().AddDays(-4);
                        case System.DayOfWeek.Tuesday:
                            return value.LastDayOfMonth().AddDays(-3);
                        case System.DayOfWeek.Wednesday:
                            return value.LastDayOfMonth().AddDays(-2);
                        case System.DayOfWeek.Thursday:
                            return value.LastDayOfMonth().AddDays(-1);
                        case System.DayOfWeek.Friday:
                            return value.LastDayOfMonth();
                        case System.DayOfWeek.Saturday:
                            return value.LastDayOfMonth().AddDays(-6);
                    }

                    break;

                case System.DayOfWeek.Saturday:

                    switch (dayOfWeek)
                    {
                        case System.DayOfWeek.Sunday:
                            return value.LastDayOfMonth().AddDays(-6);
                        case System.DayOfWeek.Monday:
                            return value.LastDayOfMonth().AddDays(-5);
                        case System.DayOfWeek.Tuesday:
                            return value.LastDayOfMonth().AddDays(-4);
                        case System.DayOfWeek.Wednesday:
                            return value.LastDayOfMonth().AddDays(-3);
                        case System.DayOfWeek.Thursday:
                            return value.LastDayOfMonth().AddDays(-2);
                        case System.DayOfWeek.Friday:
                            return value.LastDayOfMonth().AddDays(-1);
                        case System.DayOfWeek.Saturday:
                            return value.LastDayOfMonth();
                    }

                    break;
            }

            return value;
        }

        /// <summary>
        /// Gets the last day of the previous government's fiscal quarter.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>The last day of the previous government's fiscal quarter.</returns>
        public static DateTime LastDayOfPreviousQuarter(this DateTime value)
        {
            switch (value.Month)
            {
                case 1:
                case 2:
                case 3:
                    return new DateTime(value.Year - 1, 12, 31);
                case 4:
                case 5:
                case 6:
                    return new DateTime(value.Year, 3, 31);
                case 7:
                case 8:
                case 9:
                    return new DateTime(value.Year, 6, 30);
                case 10:
                case 11:
                case 12:
                    return new DateTime(value.Year, 9, 30);
            }

            return value;
        }

        /// <summary>
        /// Gets the <see cref="DateTime">DateTime</see> that represents the last day of the week (Saturday) for the given date.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <returns>The <see cref="DateTime">DateTime</see> that represents the last day of the week (Saturday) for the given date.</returns>
        public static DateTime LastDayOfWeek(this DateTime value)
        {
            // Variable Declarations
            System.DateTime dteReturnvalue = default(System.DateTime);

            // Determine the last day of the week
            try
            {
                switch (value.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dteReturnvalue = value.Add(new TimeSpan(6, 0, 0, 0));
                        break;
                    case DayOfWeek.Monday:
                        dteReturnvalue = value.Add(new TimeSpan(5, 0, 0, 0));
                        break;
                    case DayOfWeek.Tuesday:
                        dteReturnvalue = value.Add(new TimeSpan(4, 0, 0, 0));
                        break;
                    case DayOfWeek.Wednesday:
                        dteReturnvalue = value.Add(new TimeSpan(3, 0, 0, 0));
                        break;
                    case DayOfWeek.Thursday:
                        dteReturnvalue = value.Add(new TimeSpan(2, 0, 0, 0));
                        break;
                    case DayOfWeek.Friday:
                        dteReturnvalue = value.Add(new TimeSpan(1, 0, 0, 0));
                        break;
                    case DayOfWeek.Saturday:
                        dteReturnvalue = value;
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dteReturnvalue;
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> object representing the first work day in the fiscal year for the specified date.
        /// </summary>
        /// <param name="value">The date to be evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> object representing the first work day in the fiscal year for the specified date.</returns>
        public static DateTime LastWorkDayOfCurrentFiscalYear(this DateTime value)
        {
            switch (value.LastCalendarDayOfCurrentFiscalYear().DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return value.LastCalendarDayOfCurrentFiscalYear().Subtract(new TimeSpan(2, 0, 0, 0));
                case DayOfWeek.Saturday:
                    return value.LastCalendarDayOfCurrentFiscalYear().Subtract(new TimeSpan(1, 0, 0, 0));
                default:
                    return value.LastCalendarDayOfCurrentFiscalYear();
            }
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> object representing the first work day in the previous fiscal year for the specified date.
        /// </summary>
        /// <param name="value">The date to be evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> object representing the last work day in the previous fiscal year for the specified date.</returns>
        public static DateTime LastWorkDayOfPreviousFiscalYear(this DateTime value)
        {
            switch (value.LastCalendarDayOfPreviousFiscalYear().DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return value.LastCalendarDayOfPreviousFiscalYear().Subtract(new TimeSpan(2, 0, 0, 0));
                case DayOfWeek.Saturday:
                    return value.LastCalendarDayOfPreviousFiscalYear().Subtract(new TimeSpan(1, 0, 0, 0));
                default:
                    return value.LastCalendarDayOfPreviousFiscalYear();
            }
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> representing midnight on the specified date.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing midnight on the specified date.</returns>
        public static DateTime Midnight(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day);
        }

        /// <summary>
        /// Gets the first date following the specified date which falls on the specified day of the week.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <param name="dayOfWeek">The day of week to return.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing the next specified day of week that falls after after the specified date.</returns>
        public static DateTime NextDayOfMonth(this DateTime value, DayOfWeek dayOfWeek)
        {
            int intOffsetDays = dayOfWeek - value.DayOfWeek;

            if (intOffsetDays <= 0)
            {
                intOffsetDays += 7;
            }

            return value.AddDays(intOffsetDays);
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> representing noon on the specified date.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing noon on the specified date.</returns>
        public static DateTime Noon(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 12, 0, 0);
        }

        /// <summary>
        /// Sets the time of the specified date with hour precision.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <param name="hour">The hour to set the return value to.</param>
        /// <returns>The specified date set to hour precision.</returns>
        public static DateTime SetTime(this DateTime value, int hour)
        {
            return SetTime(value, hour, 0);
        }

        /// <summary>
        /// Sets the time of the specified date with minute precision.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <param name="hour">The hour to set the return value to.</param>
        /// <param name="minute">The minute to set the return value to.</param>
        /// <returns>The specified date set to minute precision.</returns>
        public static DateTime SetTime(this DateTime value, int hour, int minute)
        {
            return SetTime(value, hour, minute, 0, 0);
        }

        /// <summary>
        /// Sets the time of the specified date with second precision.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <param name="hour">The hour to set the return value to.</param>
        /// <param name="minute">The minute to set the return value to.</param>
        /// <param name="second">The second to set the return value to.</param>
        /// <returns>The specified date set to second precision.</returns>
        public static DateTime SetTime_2(this DateTime value, int hour, int minute, int second)
        {
            return SetTime(value, hour, minute, second, 0);
        }

        /// <summary>
        /// Sets the time of the specified date with millisecond precision.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <param name="hour">The hour to set the return value to.</param>
        /// <param name="minute">The minute to set the return value to.</param>
        /// <param name="second">The second to set the return value to.</param>
        /// <param name="millisecond">The millisecond to set the return value to.</param>
        /// <returns>The specified date set to millisecond precision.</returns>
        public static DateTime SetTime(this DateTime value, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(value.Year, value.Month, value.Day, hour, minute, second, millisecond);
        }

        /// <summary>
        /// Returns a new DateTime representing the exact start of the day for the specified instance.
        /// </summary>
        /// <param name="value">DateTime for which to retrieve the start of the day.</param>
        /// <returns>A new DateTime representing the start of the day.</returns>
        public static DateTime StartOfDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// Subtracts the specified number of days from the value of this instance.
        /// </summary>
        /// <param name="dt">DateTime to subtract days from.</param>
        /// <param name="value">A number of whole and fractional days.  The parameter value can be positive or negative.</param>
        /// <returns>The resulting date.</returns>
        public static DateTime SubtractDays(this DateTime dt, double value)
        {
            return dt.AddDays((-1 * value));
        }

        /// <summary>
        /// Subtracts one month from the specified date.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing one month previous of the specified date.</returns>
        public static DateTime SubtractMonth(this DateTime value)
        {
            switch (value.Month)
            {
                case 1:

                    return new DateTime(value.Year - 1, 12, value.Day);

                case 2:
                case 6:
                case 8:
                case 9:
                case 11:

                    return new DateTime(value.Year, value.Month - 1, value.Day);

                case 3:

                    if (value.IsLeapYear() == true & value.Day > 29)
                    {
                        return new DateTime(value.Year, 2, 29);
                    }
                    else if (value.Day > 28)
                    {
                        return new DateTime(value.Year, 2, 28);
                    }
                    else
                    {
                        return new DateTime(value.Year, 2, value.Day);
                    }

                case 4:

                    return new DateTime(value.Year, value.Month - 1, value.Day);

                case 5:
                case 7:
                case 10:
                case 12:

                    if (value.Day > 30)
                    {
                        return new DateTime(value.Year, value.Month - 1, 30);
                    }
                    else
                    {
                        return new DateTime(value.Year, value.Month - 1, value.Day);
                    }
            }

            return value;
        }

        /// <summary>
        /// Subtracts one week from the specified <see cref="DateTime">DateTime</see> value.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing one weeek from the specified <see cref="DateTime">DateTime</see>.</returns>
        public static DateTime SubtractWeek(this DateTime value)
        {
            return value.SubtractWeeks(1);
        }

        /// <summary>
        /// Subtracts the specified number of weeks from the specified <see cref="DateTime">DateTime</see> value.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <param name="numberOfWeeks">The number of weeks to subtract from the specified value.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> representing the specified number of weeks from the specified <see cref="DateTime">DateTime</see>.</returns>
        public static DateTime SubtractWeeks(this DateTime value, int numberOfWeeks)
        {
            return value.Subtract(new TimeSpan(7 * numberOfWeeks, 0, 0, 0));
        }

        /// <summary>
        /// Formats past dates in a more user friendly way.  For example,
        /// "12 hours ago".
        /// </summary>
        /// <param name="value">DateTime to compare to the current DateTime.</param>
        /// <returns>The resulting string.</returns>
        public static string ToAgoString(this DateTime value)
        {
            DateTime now = DateTime.Now;

            TimeSpan ts = now - value;

            if (ts.Days == 0)
            {
                double totalMinutes = Math.Round(ts.TotalMinutes);

                if (totalMinutes < 60)
                {
                    if (totalMinutes == 1)
                    {
                        return totalMinutes.ToString() + " minute ago";
                    }
                    else
                    {
                        return totalMinutes.ToString() + " minutes ago";
                    }
                }

                if (ts.Hours == 1)
                {
                    return ts.Hours.ToString() + " hour ago";
                }
                else
                {
                    return ts.Hours.ToString() + " hours ago";
                }
            }
            else if (ts.Days == 1)
            {
                return "Yesterday";
            }
            else if (ts.Days >= 2 && ts.Days <= 5)
            {
                return ts.Days.ToString() + " days ago";
            }
            else if (ts.Days >= 6 && ts.Days <= 12)
            {
                return "1 week ago";
            }
            else if (ts.Days >= 13 && ts.Days <= 19)
            {
                return "2 weeks ago";
            }

            return value.ToFriendlyString();
        }

        /// <summary>
        /// Formats the specified date as a properly DOD formatted <see cref="String">String</see>.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <param name="includeTime">If set to <c>True</c> the time will be included in the formatted <see cref="String">String</see>.</param>
        /// <returns>If includeTime is false, a date formatted as 31-DEC-1971.  Otherwise, a date formatted as 31-DEC-1971 12:30:00.</returns>
        public static string ToDODString(this DateTime value, bool includeTime)
        {
            if (includeTime == true)
            {
                return value.ToString("dd-MMM-yyyy HH:mm:ss").ToUpper();
            }
            else
            {
                return value.ToString("dd-MMM-yyyy").ToUpper();
            }
        }

        /// <summary>
        /// Represents dates in a more user friendly way.
        /// </summary>
        /// <param name="value">DateTime to use when generating the string.</param>
        /// <returns>Friendlier, more readable date string.</returns>
        public static string ToFriendlyString(this DateTime value)
        {
            string s = string.Empty;

            if (value.Date == DateTime.Today)
            {
                s = "Today";
            }
            else if (value.Date == DateTime.Today.AddDays(-1))
            {
                s = "Yesterday";
            }
            else if (value.Date > DateTime.Today.AddDays(-6))
            {
                s = value.ToString("dddd").ToString();
            }
            else
            {
                s = value.ToString("MMMM dd, yyyy");
            }

            s += " @ " + value.ToString("t").ToLower();

            return s;
        }

        /// <summary>
        /// Gets a <see cref="DateTime">DateTime</see> that represents the current hour.
        /// </summary>
        /// <param name="value">The date to evaluate.</param>
        /// <returns>A <see cref="DateTime">DateTime</see> that represents the current hour.</returns>
        public static DateTime ToHour(this DateTime value)
        {
            return value.SetTime(value.Hour);
        }

        /// <summary>
        /// Converts a regular DateTime to a RFC822 date string.
        /// </summary>
        /// <param name="value">DateTime to use when generating the string.</param>
        /// <returns>The specified date formatted as a RFC822 date string.</returns>
        public static string ToRFC822DateString(this DateTime value)
        {
            int offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;

            string timeZone = "+" + offset.ToString().PadLeft(2, '0');

            if (offset < 0)
            {
                int i = offset * -1;

                timeZone = "-" + i.ToString().PadLeft(2, '0');
            }

            return value.ToString(
                "ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'),
                System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }

        /// <summary>
        /// Formats a friendly string indicating the amound of time until a date.
        /// </summary>
        /// <param name="value">Date to use in calculation.</param>
        /// <returns>Friendly, compact string (e.g. 2d 4h 7m).</returns>
        public static string ToUntilString(this DateTime value)
        {
            TimeSpan diff = value - DateTime.Now;

            int days = diff.Days;
            int hours = diff.Hours;
            int minutes = diff.Minutes;

            return diff.Days + "d, " + diff.Hours + "h, " + diff.Minutes + "m";
        }

        #region Selectors

        /// <summary>
        /// Provides an easy way to navigate weeks, e.g. 3.Weeks().Ago.
        /// </summary>
        /// <param name="value">Number of weeks to navigate.</param>
        /// <returns>When appended with ".Ago" or ".FromNow", can be used to navigate weeks.</returns>
        /// <example>
        ///     <code language="c#">
        ///         Console.WriteLine(3.Weeks().Ago);
        ///     </code>
        /// </example>
        public static WeekSelector Weeks(this int value)
        {
            var ws = new WeekSelector();
            ws.ReferenceValue = value;
            return ws;
        }

        /// <summary>
        /// Provides an easy way to navigate years, e.g. 5.Years().FromNow.
        /// </summary>
        /// <param name="value">Number of years to navigate.</param>
        /// <returns>When appended with ".Ago" or ".FromNow", can be used to navigate years.</returns>
        /// <example>
        ///     <code language="c#">
        ///         Console.WriteLine(5.Years().FromNow);
        ///     </code>
        /// </example>
        public static YearsSelector Years(this int value)
        {
            var ws = new YearsSelector();
            ws.ReferenceValue = value;
            return ws;
        }

        /// <summary>
        /// Provides an easy way to navigate days, e.g. 19.Days().From(myDate).
        /// </summary>
        /// <param name="value">Number of days to navigate.</param>
        /// <returns>When appended with ".Ago" or ".FromNow", can be used to navigate days.</returns>
        /// <example>
        ///     <code language="c#">
        ///         Console.WriteLine(19.Days().From(new DateTime(2007,1,1)));
        ///     </code>
        /// </example>
        public static DaysSelector Days(this int value)
        {
            var ws = new DaysSelector();
            ws.ReferenceValue = value;
            return ws;
        }

        #endregion
        #endregion
    }

    #region TimeSelector

    /// <summary>
    /// Weeks, Years, Days helper class.
    /// </summary>
    public abstract class TimeSelector
    {
        /// <summary>
        /// Used by class to calculate time differences.
        /// </summary>
        private TimeSpan timeSpan;

        /// <summary>
        /// Gets a time in the past.
        /// </summary>
        public DateTime Ago
        {
            get
            {
                return DateTime.Now - this.timeSpan;
            }
        }

        /// <summary>
        /// Gets a time in the future.
        /// </summary>
        public DateTime FromNow
        {
            get
            {
                return DateTime.Now + this.timeSpan;
            }
        }

        /// <summary>
        /// Sets an internal reference value.
        /// </summary>
        internal int ReferenceValue
        {
            set
            {
                this.timeSpan = this.MyTimeSpan(value);
            }
        }

        /// <summary>
        /// Determines time in past from a date.
        /// </summary>
        /// <param name="dt">Date for which to relate the past date.</param>
        /// <returns>Date prior to this instance.</returns>
        public DateTime AgoSince(DateTime dt)
        {
            return dt - this.timeSpan;
        }

        /// <summary>
        /// Determines a time in the future.
        /// </summary>
        /// <param name="dt">Date for which to relate the future date.</param>
        /// <returns>A future date.</returns>
        public DateTime From(DateTime dt)
        {
            return dt + this.timeSpan;
        }

        /// <summary>
        /// Abstract time span helper method.
        /// </summary>
        /// <param name="refValue">Reference value.</param>
        /// <returns>A time span.</returns>
        protected abstract TimeSpan MyTimeSpan(int refValue);
    }

    /// <summary>
    /// Weeks helper class.
    /// </summary>
    public class WeekSelector : TimeSelector
    {
        /// <summary>
        /// Abstract time span helper method.
        /// </summary>
        /// <param name="refValue">Reference value.</param>
        /// <returns>A time span.</returns>
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(7 * refValue, 0, 0, 0);
        }
    }

    /// <summary>
    /// Days helper class.
    /// </summary>
    public class DaysSelector : TimeSelector
    {
        /// <summary>
        /// Abstract time span helper method.
        /// </summary>
        /// <param name="refValue">Reference value.</param>
        /// <returns>A time span.</returns>
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(refValue, 0, 0, 0);
        }
    }

    /// <summary>
    /// Years helper class.
    /// </summary>
    public class YearsSelector : TimeSelector
    {
        /// <summary>
        /// Abstract time span helper method.
        /// </summary>
        /// <param name="refValue">Reference value.</param>
        /// <returns>A time span.</returns>
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(365 * refValue, 0, 0, 0);
        }
    }

    #endregion

}