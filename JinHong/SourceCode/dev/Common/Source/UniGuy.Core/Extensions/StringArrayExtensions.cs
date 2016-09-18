using System;
using System.Text;
using System.Collections.Generic;

namespace UniGuy.Core.Extensions
{
    /// <summary>
    /// 	Extension methods for string[]
    /// </summary>
    public static class StringArrayExtensions
    {
        #region ToString
        /// <summary>
        /// 	Returns a combined value of strings from a string array
        /// </summary>
        /// <param name = "values">The values.</param>
        /// <param name = "prefix">The prefix.</param>
        /// <param name = "suffix">The suffix.</param>
        /// <param name = "quotation">The quotation (or null).</param>
        /// <param name = "separator">The separator.</param>
        /// <returns>
        /// 	A <see cref = "System.String" /> that represents this instance.
        /// </returns>
        /// <remarks>
        /// 	Contributed by blaumeister, http://www.codeplex.com/site/users/view/blaumeiser
        /// </remarks>
        public static string ToString(string[] values, string prefix, string suffix, string quotation, string separator)
        {
            var sb = new StringBuilder();
            sb.Append(prefix);

            for (var i = 0; i < values.Length; i++)
            {
                if (i > 0)
                    sb.Append(separator);
                if (quotation != null)
                    sb.Append(quotation);
                sb.Append(values[i]);
                if (quotation != null)
                    sb.Append(quotation);
            }

            sb.Append(suffix);
            return sb.ToString();
        }

        public static string ToString(string[] values, string prefix, string suffix, string quotation)
        {
            return ToString(values, prefix, suffix, quotation, ",");
        }

        public static string ToString(string[] values, string prefix, string suffix)
        {
            return ToString(values, prefix, suffix, "\"");
        }

        public static string ToString(string[] values, string prefix)
        {
            return ToString(values, prefix, ")");
        }

        public static string ToString(string[] values)
        {
            return ToString(values, "(");
        }
        #endregion // ToString

        #region WJ
        /// <summary>
        /// 删除开头部分的Null
        /// </summary>
        /// <param name="this"></param>
        /// <param name="alsoEmptyString">是否同时删除吃空字符串</param>
        public static string[] TrimStart(string[] @this, bool alsoEmptyString)
        {
            int i = 0;
            while (i < @this.Length && (alsoEmptyString ? string.IsNullOrEmpty(@this[i]) : @this[i] == null))
                i++;
            if (i != 0)
            {
                Array.Copy(@this, i, @this, 0, @this.Length - i);
                Array.Resize(ref @this, @this.Length - i);
            }

            return @this;
        }
        public static string[] TrimStart(string[] @this)
        {
            return TrimStart(@this, true);
        }
        /// <summary>
        /// 删除结尾部分的Null
        /// </summary>
        /// <param name="this"></param>
        /// <param name="alsoEmptyString">是否同时删除吃空字符串</param>
        public static string[] TrimEnd(string[] @this, bool alsoEmptyString)
        {
            int i = @this.Length - 1;
            while (i >= 0 && (alsoEmptyString ? string.IsNullOrEmpty(@this[i]) : @this[i] == null))
                i--;
            if (i != @this.Length - 1)
            {
                Array.Resize(ref @this, @this.Length - i);
            }
            return @this;
        }

        public static string[] TrimEnd(string[] @this)
        {
            return TrimEnd(@this, true);
        }
        /// <summary>
        /// 删除开头和结尾部分的Null
        /// </summary>
        /// <param name="this"></param>
        /// <param name="alsoEmptyString"></param>
        public static string[] Trim(string[] @this, bool alsoEmptyString)
        {
            return TrimEnd(TrimStart(@this, alsoEmptyString), alsoEmptyString);
        }

        public static string[] Trim(string[] @this)
        {
            return Trim(@this, true);
        }
        /// <summary>
        /// 从字符串数组中查找第一个目标字符串
        /// </summary>
        /// <param name="this"></param>
        /// <param name="target"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string Find(string[] @this, string target, bool ignoreCase, MatchMode mode)
        {
            return Array.Find(@this, new Predicate<string>(delegate(string str)
            {
                switch (mode)
                {
                    case MatchMode.FullLength:
                        if (string.Compare(str, target, ignoreCase) == 0)
                            return true;
                        break;
                    case MatchMode.Contains:
                        if (str.IndexOf(target, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0)
                            return true;
                        break;
                    case MatchMode.StartsWith:
                        if (str.IndexOf(target, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
                            return true;
                        break;
                    case MatchMode.EndsWith:
                        if (str.IndexOf(target, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) + target.Length == str.Length)
                            return true;
                        break;
                }
                return false;
            }));
        }

        public static string Find(string[] @this, string target, bool ignoreCase)
        {
            return Find(@this, target, ignoreCase, MatchMode.FullLength);
        }

        public static string Find(string[] @this, string target)
        {
            return Find(@this, target, false);
        }
        /// <summary>
        /// 从字符串数组中查找所有目标字符串
        /// </summary>
        /// <param name="this"></param>
        /// <param name="target"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string[] FindAll(string[] @this, string target, bool ignoreCase, MatchMode mode)
        {
            return Array.FindAll(@this, new Predicate<string>(delegate(string str)
            {
                switch (mode)
                {
                    case MatchMode.FullLength:
                        if (string.Compare(str, target, ignoreCase) == 0)
                            return true;
                        break;
                    case MatchMode.Contains:
                        if (str.IndexOf(target, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0)
                            return true;
                        break;
                    case MatchMode.StartsWith:
                        if (str.IndexOf(target, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
                            return true;
                        break;
                    case MatchMode.EndsWith:
                        if (str.IndexOf(target, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) + target.Length == str.Length)
                            return true;
                        break;
                }
                return false;
            }));
        }

        public static string[] FindAll(string[] @this, string target, bool ignoreCase)
        {
            return FindAll(@this, target, ignoreCase, MatchMode.FullLength);
        }

        public static string[] FindAll(string[] @this, string target)
        {
            return FindAll(@this, target, false);
        }

        public static bool Contains(string[] @this, string target)
        {
            return Find(@this, target) != null;
        }

        public static bool ContainsAny(string[] @this, params string[] targets)
        {
            foreach (string target in targets)
            {
                if (Contains(@this, target))
                    return true;
                continue;
            }
            return false;
        }
        /// <summary>
        /// 从字符串数组中查找第一个符合目标模式字符串
        /// </summary>
        /// <param name="this"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string FindWithPattern(string[] @this, string pattern)
        {
            return Array.Find(@this, new Predicate<string>(delegate(string str)
            {
                return System.Text.RegularExpressions.Regex.IsMatch(str, pattern);
            }));
        }
        /// <summary>
        /// 从字符串数组中查找所有符合目标模式字符串
        /// </summary>
        /// <param name="this"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string[] FindAllWithPattern(string[] @this, string pattern)
        {
            return Array.FindAll(@this, new Predicate<string>(delegate(string str)
            {
                return System.Text.RegularExpressions.Regex.IsMatch(str, pattern);
            }));
        }

        /// <summary>
        /// 给字符串数组中的每个字符串Trim
        /// </summary>
        /// <param name="this"></param>
        public static void TrimEach(this string[] @this)
        {
            for (int i = 0; i < @this.Length; i++)
                if (@this[i] != null)
                    @this[i] = @this[i].Trim();
        }
        #endregion //   WJ

        #region Internal Types
        public enum MatchMode
        {
            FullLength,
            StartsWith,
            Contains,
            EndsWith
        }
        #endregion //   Internal Types
    }
}