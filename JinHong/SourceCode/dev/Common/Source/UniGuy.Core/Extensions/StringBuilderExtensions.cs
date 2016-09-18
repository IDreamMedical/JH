using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using UniGuy.Core.DataStructures;

namespace UniGuy.Core.Extensions
{
    public static class StringBuilderExtensions
    {
        public static int IndexOf(this StringBuilder @this, char value, int startIndex)
        {
            if (startIndex < 0 || startIndex >= @this.Length)
                throw new ArgumentException("start index not between 0 and length.");

            for (int i = startIndex; i < @this.Length; i++)
                if (@this[i] == value)
                    return i;
            return -1;
        }
        public static int LastIndexOf(this StringBuilder @this, char value, int startIndex)
        {
            if (startIndex < 0 || startIndex >= @this.Length)
                throw new ArgumentException("start index not between 0 and length.");

            for (int i = startIndex; i >= 0; i--)
                if (@this[i] == value)
                    return i;
            return -1;
        }
        public static int IndexOf(this StringBuilder @this, string value, int startIndex)
        {
            if (value.IsNullOrEmpty())
                throw new ArgumentException("value can not be null or empty.");
            if (startIndex < 0 || startIndex >= @this.Length)
                throw new ArgumentException("start index not between 0 and length.");

            int length = value.Length;
            for (int i = startIndex; i + length <= @this.Length; i++)
                if (@this.ToString(i, length) == value)
                    return i;
            return -1;
        }
        public static int LastIndexOf(this StringBuilder @this, string value, int startIndex)
        {
            if (value.IsNullOrEmpty())
                throw new ArgumentException("value can not be null or empty.");
            if (startIndex < 0 || startIndex >= @this.Length)
                throw new ArgumentException("start index not between 0 and length.");

            int length = value.Length;
            for (int i = startIndex - length; i >= 0; i--)
                if (@this.ToString(i, length) == value)
                    return i;
            return -1;
        }
        /// <summary>
        /// 对字符串替换参数,value""表示删除，null表示不替换
        /// </summary>
        /// <param name="this">字符串</param>
        /// <param name="trt">参数转换委托</param>
        /// <param name="leftMark">参数左侧标记</param>
        /// <param name="rightMark">参数右侧标记</param>
        /// <returns></returns>
        public static void ReplaceParameters(this StringBuilder @this, Treatment<string> trt, char leftMark, char rightMark)
        {
            int pos = 0;
        rp0:
            pos = IndexOf(@this, leftMark, pos);
            if (pos >= 0)
            {
                int end = IndexOf(@this, rightMark, pos);
                if (end >= 0)
                {
                    string key = @this.ToString(pos + 1, end - pos - 1);
                    string value = trt(key);
                    //  null不替换
                    if (value != null)
                    {
                        @this.Remove(pos, end - pos + 1).Insert(pos, value);
                        pos += value.Length;
                    }
                    else
                    {
                        pos++;
                    }
                    goto rp0;
                }
            }
        }

        /// <summary>
        /// 对字符串替换参数,value""表示删除，null表示不替换
        /// </summary>
        /// <param name="this">字符串</param>
        /// <param name="trt">参数转换委托</param>
        /// <param name="leftMark">参数左侧标记</param>
        /// <param name="rightMark">参数右侧标记</param>
        /// <returns></returns>
        public static void ReplaceParameters(this StringBuilder @this, Treatment<string> trt, string leftMark, string rightMark)
        {
            int pos = 0;
        rp0:
            pos = IndexOf(@this, leftMark, pos);
            if (pos >= 0)
            {
                int end = IndexOf(@this, rightMark, pos);
                if (end >= 0)
                {
                    int leftMarkLength = leftMark.Length;
                    int rightMarkLength = rightMark.Length;
                    string key = @this.ToString(pos + leftMarkLength, end - pos - leftMarkLength);
                    string value = trt(key);
                    //  null不替换
                    if (value != null)
                    {
                        @this.Remove(pos, end - pos + rightMarkLength).Insert(pos, value);
                        pos += value.Length;
                    }
                    else
                    {
                        pos++;
                    }
                    goto rp0;
                }
            }
        }

        /// <summary>
        /// 使用默认标记替换字符串中的参数
        /// </summary>
        /// <param name="this"></param>
        /// <param name="trt"></param>
        /// <returns></returns>
        public static void ReplaceParameters(this StringBuilder @this, Treatment<string> trt)
        {
            ReplaceParameters(@this, trt, '{', '}');
        }

        /// <summary>
        /// 对字符串替换参数
        /// </summary>
        /// <param name="this">要被处理的字符串</param>
        /// <param name="parameters">参数字典</param>
        /// <param name="leftMark">参数左侧标记</param>
        /// <param name="rightMark">参数右侧标记</param>
        /// <returns>处理后的字符串</returns>
        /// <remarks>这种处理方法对于@this很大的情况下效率不一定高, 也比较占内存</remarks>
        public static void ReplaceParameters(this StringBuilder @this, IDictionary<string, string> parameters, string leftMark, string rightMark)
        {
            if (parameters != null)
                foreach (string key in parameters.Keys)
                    @this.Replace(leftMark + key + rightMark, parameters[key]);
        }
        /// <summary>
        /// 对于{parameter}参数格式的字符串替换参数方法，value""表示删除，null表示不替换
        /// </summary>
        /// <param name="this"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static void ReplaceParameters(this StringBuilder @this, IDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                int pos = 0;
            rp0:
                pos = IndexOf(@this, '{', pos);
                if (pos >= 0)
                {
                    int end = IndexOf(@this, '}', pos);
                    if (end >= 0)
                    {
                        string key = @this.ToString(pos + 1, end - pos - 1);
                        if (parameters.ContainsKey(key) && parameters[key] != null)
                        {
                            @this.Remove(pos, end - pos + 1).Insert(pos, parameters[key]);
                            pos += parameters[key].Length;
                        }
                        else
                        {
                            pos++;
                        }
                        goto rp0;
                    }
                }
            }
        }
    }
}