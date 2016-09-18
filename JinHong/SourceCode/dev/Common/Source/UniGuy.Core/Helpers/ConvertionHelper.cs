using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UniGuy.Core.Extensions;
using System.ComponentModel;

namespace UniGuy.Core.Helpers
{
    public class ConvertionHelper
    {
        /// <summary>
        /// 把对象转换为字符串表示
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object ConvertToString(object obj)
        {
            if (obj != null)
            {
                if (obj is string)
                    return (string)obj;
                /*
                if (obj is DateTime)
                    return ((DateTime)obj).Ticks.ToString();
                 */
                //  [-or-]
                if (obj is DateTime)
                    return ((DateTime)obj).ToString(@"yyyy-MM-dd HH\:mm\:ss");
                /*
                if (obj is TimeSpan)
                    return ((TimeSpan)obj).Ticks.ToString();
                 */
                if (obj is TimeSpan)
                    return ((TimeSpan)obj).ToString();
                //  [-or-]
                if (obj.GetType().IsEnum)
                    return Enum.GetName(obj.GetType(), obj);
                //  TODO ... Not perfect
                if (obj is string[])
                {
                    return ((string[])obj).Join('|');
                }
                return obj;
            }
            return null;
        }
        /// <summary>
        /// 把对象字典转换为逗号分割的字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string ConvertDictToString(IDictionary dict)
        {
            if (dict != null)
            {
                StringBuilder bldr = new StringBuilder();
                foreach (object obj in dict.Keys)
                    bldr.Append((string)obj).Append(", ");
                bldr.Remove(bldr.Length - 2, 2);
                return bldr.ToString();
            }
            return null;
        }

        public static object DoConversion(object value, Type toType)
        {
            if ((value is IConvertible) || (value == null))
            {
                try
                {
                    return System.Convert.ChangeType(value, toType);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                System.ComponentModel.TypeConverter typeConverter = TypeDescriptor.GetConverter(value);

                if (typeConverter.CanConvertTo(toType))
                {
                    return typeConverter.ConvertTo(value, toType);
                }
            }

            return null;
        }

        /// <summary>
        /// 把字符串转换为对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object ConvertToObject(Type type, object value)
        {
            if (type != null && value != null)
            {
                if (type == typeof(string))
                    return value is string?value:value.ToString();
                if (type == typeof(DateTime?))
                {
                    try
                    {
                        return DateTime.Parse((string)value);
                    }
                    catch { }
                }
                if (type == typeof(TimeSpan?))
                    return TimeSpan.Parse((string)value);
                if (type == typeof(long?))
                {
                    long l = -1;
                    if (long.TryParse((string)value, out l))
                        return l;
                    return null;
                }
                if (type == typeof(double?))
                {
                    double d = -1;
                    if (double.TryParse(value is string?(string)value:value.ToString(), out d))
                        return d;
                    return null;
                }
                if (type.IsEnum)
                {
                    return Enum.Parse(type, (string)value);
                }
                //  TODO ... not perfect
                if (type == typeof(string[]))
                {
                    return ((string)value).Split('|');
                }
                //return value.GetType().IsSubclassOf(type) ? value : null;
                if (type.IsAssignableFrom(value.GetType()))
                    return value;
                try
                {
                    return Convert.ChangeType(value, type);
                }
                catch { }
                return null;
                //if (typeof(IIdObject).IsAssignableFrom(type))
                //return ObjectCache.Get(type, str);
            }
            return null;
        }
        /// <summary>
        /// 把逗号分割的字符串转换为对象列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static SortedList ConvertStringToDict(Type type, string str)
        {
            if (type != null && str != null)
            {
                SortedList list = null;
                foreach (string s in str.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    object obj = ConvertToObject(type, s);
                    if (obj != null)
                    {
                        if (list == null)
                            list = new SortedList();
                        //if (obj is IIdObject)
                        //list.Add(((IIdObject)obj).Id, obj);
                        else
                            list.Add(obj.ToString(), obj);
                    }
                }
                return list;
            }
            return null;
        }
    }
}
