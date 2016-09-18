using System;
using System.Data;
using System.Collections;

namespace UniGuy.Core.Extensions
{
    /// <summary>
    /// 	Extension methods for ADO.NET DataRows (DataTable / DataSet)
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// 	Gets the record value casted to the specified data type or the data types default value.
        /// </summary>
        /// <typeparam name = "T">The return data type</typeparam>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static T Get<T>(this DataRow row, string field)
        {
            return row.Get(field, default(T));
        }

        /// <summary>
        /// 	Gets the record value casted to the specified data type or the specified default value.
        /// </summary>
        /// <typeparam name = "T">The return data type</typeparam>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static T Get<T>(this DataRow row, string field, T defaultValue)
        {
            var value = row[field];
            if (value == DBNull.Value)
                return defaultValue;
            return value.ConvertTo(defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as byte array.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static byte[] GetBytes(this DataRow row, string field)
        {
            return (row[field] as byte[]);
        }

        /// <summary>
        /// 	Gets the record value casted as string or null.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static string GetString(this DataRow row, string field)
        {
            return row.GetString(field, null);
        }

        /// <summary>
        /// 	Gets the record value casted as string or the specified default value.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static string GetString(this DataRow row, string field, string defaultValue)
        {
            var value = row[field];
            return (value is string ? (string)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as Guid or Guid.Empty.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static Guid GetGuid(this DataRow row, string field)
        {
            var value = row[field];
            return (value is Guid ? (Guid)value : Guid.Empty);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTime or DateTime.MinValue.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static DateTime GetDateTime(this DataRow row, string field)
        {
            return row.GetDateTime(field, DateTime.MinValue);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTime or the specified default value.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static DateTime GetDateTime(this DataRow row, string field, DateTime defaultValue)
        {
            var value = row[field];
            return (value is DateTime ? (DateTime)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTimeOffset (UTC) or DateTime.MinValue.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static DateTimeOffset GetDateTimeOffset(this DataRow row, string field)
        {
            return new DateTimeOffset(row.GetDateTime(field), TimeSpan.Zero);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTimeOffset (UTC) or the specified default value.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static DateTimeOffset GetDateTimeOffset(this DataRow row, string field, DateTimeOffset defaultValue)
        {
            var dt = row.GetDateTime(field);
            return (dt != DateTime.MinValue ? new DateTimeOffset(dt, TimeSpan.Zero) : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as int or 0.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static int GetInt32(this DataRow row, string field)
        {
            return row.GetInt32(field, 0);
        }

        /// <summary>
        /// 	Gets the record value casted as int or the specified default value.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static int GetInt32(this DataRow row, string field, int defaultValue)
        {
            var value = row[field];
            return (value is int ? (int)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as long or 0.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static long GetInt64(this DataRow row, string field)
        {
            return row.GetInt64(field, 0);
        }

        /// <summary>
        /// 	Gets the record value casted as long or the specified default value.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static long GetInt64(this DataRow row, string field, int defaultValue)
        {
            var value = row[field];
            return (value is long ? (long)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as decimal or 0.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static decimal GetDecimal(this DataRow row, string field)
        {
            return row.GetDecimal(field, 0);
        }

        /// <summary>
        /// 	Gets the record value casted as decimal or the specified default value.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static decimal GetDecimal(this DataRow row, string field, long defaultValue)
        {
            var value = row[field];
            return (value is decimal ? (decimal)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as bool or false.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static bool GetBoolean(this DataRow row, string field)
        {
            return row.GetBoolean(field, false);
        }

        /// <summary>
        /// 	Gets the record value casted as bool or the specified default value.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static bool GetBoolean(this DataRow row, string field, bool defaultValue)
        {
            var value = row[field];
            return (value is bool ? (bool)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value as Type class instance or null.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static Type GetType(this DataRow row, string field)
        {
            return row.GetType(field, null);
        }

        /// <summary>
        /// 	Gets the record value as Type class instance or the specified default value.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static Type GetType(this DataRow row, string field, Type defaultValue)
        {
            var classType = row.GetString(field);
            if (classType.IsNotEmpty())
            {
                var type = Type.GetType(classType);
                if (type != null)
                    return type;
            }
            return defaultValue;
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or null.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static object GetTypeInstance(this DataRow row, string field)
        {
            return row.GetTypeInstance(field, null);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or the specified default type.
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static object GetTypeInstance(this DataRow row, string field, Type defaultValue)
        {
            var type = row.GetType(field, defaultValue);
            return (type != null ? Activator.CreateInstance(type) : null);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or null.
        /// </summary>
        /// <typeparam name = "T">The type to be casted to</typeparam>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static T GetTypeInstance<T>(this DataRow row, string field) where T : class
        {
            return (row.GetTypeInstance(field, null) as T);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or the specified default type.
        /// </summary>
        /// <typeparam name = "T">The type to be casted to</typeparam>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "type">The type.</param>
        /// <returns>The record value</returns>
        public static T GetTypeInstanceSafe<T>(this DataRow row, string field, Type type) where T : class
        {
            var instance = (row.GetTypeInstance(field, null) as T);
            return (instance ?? Activator.CreateInstance(type) as T);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or an instance from the specified type.
        /// </summary>
        /// <typeparam name = "T">The type to be casted to</typeparam>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static T GetTypeInstanceSafe<T>(this DataRow row, string field) where T : class, new()
        {
            var instance = (row.GetTypeInstance(field, null) as T);
            return (instance ?? new T());
        }

        /// <summary>
        /// 	Determines whether the record value is DBNull.Value
        /// </summary>
        /// <param name = "row">The data row.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>
        /// 	<c>true</c> if the value is DBNull.Value; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDBNull(this DataRow row, string field)
        {
            var value = row[field];
            return (value == DBNull.Value);
        }

        #region WJ
        /// <summary>
        /// 把DataRow转换为Hashtable(DataColumn有一个默认的DataType,不过也可能包含DBNull类型,这样的列在.NET中排序是会发生问题)
        /// 这个转换会忽略重复列名的列,因为Hashtable的键是唯一的;
        /// </summary>
        /// <param name="row"></param>
        /// <param name="ec"></param>
        /// <returns></returns>
        public static Hashtable ToHashtable(this DataRow row)
        {
            Hashtable ht = new Hashtable();
            foreach (DataColumn col in row.Table.Columns)
                if (!ht.ContainsKey(col.ColumnName))
                    ht.Add(col.ColumnName, row[col] is DBNull ? col.DataType.GetDefaultValueOfType() : row[col]);
            return ht;
        }
        public static Hashtable ToHashtableCaseIntensitive(this DataRow row)
        {
            Hashtable ht = new Hashtable(new CaseInsensitiveHashCodeProvider(), CaseInsensitiveComparer.Default);
            foreach (DataColumn col in row.Table.Columns)
                if (!ht.ContainsKey(col.ColumnName))
                    ht.Add(col.ColumnName, row[col] is DBNull ? col.DataType.GetDefaultValueOfType() : row[col]);
            return ht;
        }
        #endregion //   WJ

        #region Edge.Extensions
        //  http://edgeextensions.codeplex.com/SourceControl/list/changesets
        /// <summary>
        /// Returns the <see cref="Boolean">Boolean</see> value of the specified column.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <returns>The <see cref="Boolean">Boolean</see> value stored in the specified column.</returns>
        /// <remarks>If the specified column contains a null, the return value will be <c>False</c>.</remarks>
        public static bool GetBooleanValue(this DataRow input, string columnName)
        {
            return GetBooleanValue(input, columnName, false);
        }

        /// <summary>
        /// Returns the <see cref="Boolean">Boolean</see> value of the specified column.
        /// If the value is null, the supplied value is used.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <param name="nullValue">The <see cref="Boolean">Boolean</see> value to return if the desired column contains a null value.</param>
        /// <returns>The <see cref="Boolean">Boolean</see> value stored in the specified column.</returns>
        public static bool GetBooleanValue(this DataRow input, string columnName, bool nullValue)
        {
            if (input.IsNull(columnName) == false && input[columnName].GetType().ToString() == typeof(bool).ToString())
            {
                return Convert.ToBoolean(input[columnName]);
            }
            else
            {
                return nullValue;
            }
        }

        /// <summary>
        /// Returns the <see cref="Byte">Byte</see> value of the specified column.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <returns>The <see cref="Byte">Byte</see> value stored in the specified column.</returns>
        public static byte GetByteValue(this DataRow input, string columnName)
        {
            return GetByteValue(input, columnName, 0);
        }

        /// <summary>
        /// Returns the <see cref="Byte">Byte</see> value of the specified column.
        /// If the value is null, the supplied value is used.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <param name="nullValue">The <see cref="Byte">Byte</see> value to return if the desired column contains a null value.</param>
        /// <returns>The <see cref="Byte">Byte</see> value stored in the specified column.</returns>
        public static byte GetByteValue(this DataRow input, string columnName, byte nullValue)
        {
            if (input.IsNull(columnName) == false && input[columnName].GetType().ToString() == typeof(byte).ToString())
            {
                return Convert.ToByte(input[columnName]);
            }
            else
            {
                return nullValue;
            }
        }

        /// <summary>
        /// Returns the <see cref="DateTime">DateTime</see> value of the specified column.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <returns>The <see cref="DateTime">DateTime</see> value stored in the specified column.</returns>
        /// <remarks>If the specified column contains a null, the return value will be a Null value.</remarks>
        public static DateTime GetDateTimeValue(this DataRow input, string columnName)
        {
            return GetDateTimeValue(input, columnName, DateTime.MinValue);
        }

        /// <summary>
        /// Returns the <see cref="DateTime">DateTime</see> value of the specified column.
        /// If the value is null, the supplied value is used.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <param name="nullValue">The <see cref="DateTime">DateTime</see> value to return if the desired column contains a null value.</param>
        /// <returns>The <see cref="DateTime">DateTime</see> value stored in the specified column.</returns>
        public static DateTime GetDateTimeValue(this DataRow input, string columnName, DateTime nullValue)
        {
            if (input.IsNull(columnName) == false && input[columnName].GetType().ToString() == typeof(DateTime).ToString())
            {
                return Convert.ToDateTime(input[columnName]);
            }
            else
            {
                return nullValue;
            }
        }

        /// <summary>
        /// Returns the <see cref="Decimal">Decimal</see> value of the specified column.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <returns>The <see cref="Decimal">Decimal</see> value stored in the specified column.</returns>
        /// <remarks>If the specified column contains a null, the return value will be 0.</remarks>
        public static decimal GetDecimalValue(this DataRow input, string columnName)
        {
            return GetDecimalValue(input, columnName, 0);
        }

        /// <summary>
        /// Returns the <see cref="Decimal">Decimal</see> value of the specified column.
        /// If the value is null, the supplied value is used.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <param name="nullValue">The <see cref="Decimal">Decimal</see> value to return if the desired column contains a null value.</param>
        /// <returns>The <see cref="Decimal">Decimal</see> value stored in the specified column.</returns>
        public static decimal GetDecimalValue(this DataRow input, string columnName, decimal nullValue)
        {
            if (input.IsNull(columnName) == false && input[columnName].GetType().ToString() == typeof(decimal).ToString())
            {
                return Convert.ToDecimal(input[columnName]);
            }
            else
            {
                return nullValue;
            }
        }

        /// <summary>
        /// Returns the <see cref="Integer">Integer</see> value of the specified column.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <returns>The <see cref="Integer">Integer</see> value stored in the specified column.</returns>
        /// <remarks>If the specified column contains a null, the return value will be 0.</remarks>
        public static int GetInt32Value(this DataRow input, string columnName)
        {
            return GetInt32Value(input, columnName, 0);
        }

        /// <summary>
        /// Returns the <see cref="Integer">Integer</see> value of the specified column.
        /// If the value is null, the supplied value is used.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <param name="nullValue">The <see cref="Integer">Integer</see> value to return if the desired column contains a null value.</param>
        /// <returns>The <see cref="Integer">Integer</see> value stored in the specified column.</returns>
        public static int GetInt32Value(this DataRow input, string columnName, int nullValue)
        {
            if (input.IsNull(columnName) == false && input[columnName].GetType().ToString() == typeof(int).ToString())
            {
                return Convert.ToInt32(input[columnName]);
            }
            else
            {
                return nullValue;
            }
        }

        /// <summary>
        /// Returns the <see cref="Long">Long</see> value of the specified column.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <returns>The <see cref="Long">Long</see> value stored in the specified column.</returns>
        /// <remarks>If the specified column contains a null, the return value will be 0.</remarks>
        public static long GetInt64Value(this DataRow input, string columnName)
        {
            return GetInt64Value(input, columnName, 0);
        }

        /// <summary>
        /// Returns the <see cref="Long">Long</see> value of the specified column.
        /// If the value is null, the supplied value is used.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <param name="nullValue">The <see cref="Long">Long</see> value to return if the desired column contains a null value.</param>
        /// <returns>The <see cref="Long">Long</see> value stored in the specified column.</returns>
        public static long GetInt64Value(this DataRow input, string columnName, long nullValue)
        {
            if (input.IsNull(columnName) == false && input[columnName].GetType().ToString() == typeof(long).ToString())
            {
                return Convert.ToInt64(input[columnName]);
            }
            else
            {
                return nullValue;
            }
        }

        /// <summary>
        /// Returns the <see cref="Short">Short</see> value of the specified column.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <returns>The <see cref="Short">Short</see> value stored in the specified column.</returns>
        /// <remarks>If the specified column contains a null, the return value will be 0.</remarks>
        public static short GetInt16Value(this DataRow input, string columnName)
        {
            return GetInt16Value(input, columnName, 0);
        }

        /// <summary>
        /// Returns the <see cref="Short">Short</see> value of the specified column.
        /// If the value is null, the supplied value is used.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <param name="nullValue">The <see cref="Short">Short</see> value to return if the desired column contains a null value.</param>
        /// <returns>The <see cref="Short">Short</see> value stored in the specified column.</returns>
        public static short GetInt16Value(this DataRow input, string columnName, short nullValue)
        {
            if (input.IsNull(columnName) == false && input[columnName].GetType().ToString() == typeof(short).ToString())
            {
                return Convert.ToInt16(input[columnName]);
            }
            else
            {
                return nullValue;
            }
        }

        /// <summary>
        /// Returns the <see cref="String">String</see> value of the specified column.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <returns>The <see cref="String">String</see> value stored in the specified column.</returns>
        /// <remarks>If the specified column contains a null, the return value will be a blank string ("").</remarks>
        public static string GetStringValue(this DataRow input, string columnName)
        {
            return GetStringValue(input, columnName, string.Empty);
        }

        /// <summary>
        /// Returns the <see cref="String">String</see> value of the specified column.
        /// If the value is null, the supplied value is used.
        /// </summary>
        /// <param name="input">The <see cref="DataRow">DataRow</see> containing the desired data.</param>
        /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
        /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
        /// <returns>The <see cref="String">String</see> value stored in the specified column specifying that the value should be equal to "USAAC" if the database value is Null.</returns>
        public static string GetStringValue(this DataRow input, string columnName, string nullValue)
        {
            if (input.IsNull(columnName) == false && input[columnName].GetType().ToString() == typeof(string).ToString())
            {
                return Convert.ToString(input[columnName]);
            }
            else
            {
                return nullValue;
            }
        }
        #endregion
    }
}