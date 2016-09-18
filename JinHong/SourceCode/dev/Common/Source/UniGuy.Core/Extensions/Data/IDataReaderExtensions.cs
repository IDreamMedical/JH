#region Edge.Extensions
//  http://edgeextensions.codeplex.com/SourceControl/list/changesets

// <copyright file="IDataReaderExtensions.cs" company="Edge Extensions Project">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Kevin Nessland</author>
// <email>kevinnessland@gmail.com</email>
// <date>2009-07-08</date>
// <summary>Contains extension methods for the IDataReader object.</summary>
using System;
using System.Data;

/// <summary>
/// Provides extension methods to the <see cref="IDataReader">IDataReader</see> object.
/// </summary>
public static class IDataReaderExtensions
{
    /// <summary>
    /// Gets the value of the specified column (by name) as a Boolean.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <returns>The <see cref="Boolean">Boolean</see> value of the column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a Boolean, or an exception is generated.</remarks>
    public static bool GetBooleanValue(this IDataReader input, string columnName)
    {
        return GetBooleanValue(input, columnName, false);
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a Boolean.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <param name="nullValue">The <see cref="Boolean">Boolean</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Boolean">Boolean</see> value of the column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a Boolean, or an exception is generated.</remarks>
    public static bool GetBooleanValue(this IDataReader input, string columnName, bool nullValue)
    {
        bool returnValue = false;

        try
        {
            int intOrdinal = input.GetOrdinal(columnName);

            if (input.IsDBNull(intOrdinal) == false)
            {
                returnValue = input.GetBoolean(intOrdinal);
            }
            else
            {
                returnValue = nullValue;
            }
        }
        catch (Exception)
        {
            returnValue = nullValue;
        }

        return returnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a Boolean.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <returns>The <see cref="Boolean">Boolean</see> value of the column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a Boolean, or an exception is generated.</remarks>
    public static bool GetBooleanValue(this IDataReader input, int columnIndex)
    {
        return GetBooleanValue(input, columnIndex, false);
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a Boolean.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <param name="nullValue">The <see cref="Boolean">Boolean</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Boolean">Boolean</see> value of the column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a Boolean, or an exception is generated.</remarks>
    public static bool GetBooleanValue(this IDataReader input, int columnIndex, bool nullValue)
    {
        bool returnValue = false;

        try
        {
            if (input.IsDBNull(columnIndex) == false)
            {
                returnValue = input.GetBoolean(columnIndex);
            }
            else
            {
                returnValue = nullValue;
            }
        }
        catch (Exception)
        {
            returnValue = nullValue;
        }

        return returnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a byte.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <returns>The <see cref="Byte">Byte</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a string.</remarks>
    public static byte GetByteValue(this IDataReader input, string columnName)
    {
        return GetByteValue(input, columnName, 0);
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a byte.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Byte">Byte</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a string.</remarks>
    public static byte GetByteValue(this IDataReader input, string columnName, byte nullValue)
    {
        byte bytReturnValue = 0;

        try
        {
            int intOrdinal = input.GetOrdinal(columnName);
            if (input.IsDBNull(intOrdinal) == false)
            {
                bytReturnValue = input.GetByte(intOrdinal);
            }
            else
            {
                bytReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            bytReturnValue = nullValue;
        }

        return bytReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a byte.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <returns>The <see cref="Byte">Byte</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a string.</remarks>
    public static byte GetByteValue(this IDataReader input, int columnIndex)
    {
        return GetByteValue(input, columnIndex, 0);
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a byte.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Byte">Byte</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a string.</remarks>
    public static byte GetByteValue(this IDataReader input, int columnIndex, byte nullValue)
    {
        byte bytReturnValue = 0;

        try
        {
            if (input.IsDBNull(columnIndex) == false)
            {
                bytReturnValue = input.GetByte(columnIndex);
            }
            else
            {
                bytReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            bytReturnValue = nullValue;
        }

        return bytReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a <see cref="DateTime">DateTime</see> object.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <returns>The <see cref="DateTime">DateTime</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a <see cref="DateTime">DateTime</see> object.</remarks>
    public static DateTime GetDateTimeValue(this IDataReader input, string columnName)
    {
        return GetDateTimeValue(input, columnName);
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a <see cref="DateTime">DateTime</see> object.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <returns>The <see cref="DateTime">DateTime</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a <see cref="DateTime">DateTime</see> object.</remarks>
    public static DateTime GetDateTimeValue(this IDataReader input, int columnIndex)
    {
        return GetDateTimeValue(input, columnIndex);
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a <see cref="DateTime">DateTime</see> object.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <param name="nullValue">The <see cref="DateTime">DateTime</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="DateTime">DateTime</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a <see cref="DateTime">DateTime</see> object.</remarks>
    public static DateTime GetDateTimeValue(this IDataReader input, int columnIndex, DateTime nullValue)
    {
        DateTime returnValue = default(DateTime);

        try
        {
            if (input.IsDBNull(columnIndex) == false)
            {
                returnValue = input.GetDateTime(columnIndex);
            }
            else
            {
                returnValue = nullValue;
            }
        }
        catch (Exception)
        {
            returnValue = nullValue;
        }

        return returnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a <see cref="DateTime">DateTime</see> object.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <param name="nullValue">The <see cref="DateTime">DateTime</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="DateTime">DateTime</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a <see cref="DateTime">DateTime</see> object.</remarks>
    public static DateTime GetDateTimeValue(this IDataReader input, string columnName, DateTime nullValue)
    {
        DateTime returnValue = default(DateTime);

        try
        {
            int intOrdinal = input.GetOrdinal(columnName);
            if (input.IsDBNull(intOrdinal) == false)
            {
                returnValue = input.GetDateTime(intOrdinal);
            }
            else
            {
                returnValue = nullValue;
            }
        }
        catch (Exception)
        {
            returnValue = nullValue;
        }

        return returnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a <see cref="Decimal">Decimal</see> object.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <returns>The <see cref="Decimal">Decimal</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a <see cref="Decimal">Decimal</see> object.</remarks>
    public static decimal GetDecimalValue(this IDataReader input, string columnName)
    {
        return GetDecimalValue(input, columnName);
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a <see cref="Decimal">Decimal</see> object.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Decimal">Decimal</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a <see cref="Decimal">Decimal</see> object.</remarks>
    public static decimal GetDecimalValue(this IDataReader input, string columnName, decimal nullValue)
    {
        decimal decReturnValue = default(decimal);

        try
        {
            int intOrdinal = input.GetOrdinal(columnName);

            if (input.IsDBNull(intOrdinal) == false)
            {
                decReturnValue = input.GetDecimal(intOrdinal);
            }
            else
            {
                decReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            decReturnValue = nullValue;
        }

        return decReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a <see cref="Decimal">Decimal</see> object.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <returns>The <see cref="Decimal">Decimal</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a <see cref="Decimal">Decimal</see> object.</remarks>
    public static decimal GetDecimalValue(this IDataReader input, int columnIndex)
    {
        return GetDecimalValue(input, columnIndex);
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a <see cref="Decimal">Decimal</see> object.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Decimal">Decimal</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a <see cref="Decimal">Decimal</see> object.</remarks>
    public static decimal GetDecimalValue(this IDataReader input, int columnIndex, decimal nullValue)
    {
        decimal decReturnValue = default(decimal);

        try
        {
            if (input.IsDBNull(columnIndex) == false)
            {
                decReturnValue = input.GetDecimal(columnIndex);
            }
            else
            {
                decReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            decReturnValue = nullValue;
        }

        return decReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a double-precision floating point number.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <returns>The <see cref="Double">Double</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed. Therefore, the data retrieved must already be a double-precision floating point number.</remarks>
    public static double GetDoubleValue(this IDataReader input, string columnName)
    {
        return GetDoubleValue(input, columnName);
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a double-precision floating point number.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Double">Double</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed. Therefore, the data retrieved must already be a double-precision floating point number.</remarks>
    public static double GetDoubleValue(this IDataReader input, string columnName, double nullValue)
    {
        double dblReturnValue = 0;

        try
        {
            int intOrdinal = input.GetOrdinal(columnName);

            if (input.IsDBNull(intOrdinal) == false)
            {
                dblReturnValue = input.GetDouble(intOrdinal);
            }
            else
            {
                dblReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            dblReturnValue = nullValue;
        }

        return dblReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a double-precision floating point number.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <returns>The <see cref="Double">Double</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed. Therefore, the data retrieved must already be a double-precision floating point number.</remarks>
    public static double GetDoubleValue(this IDataReader input, int columnIndex)
    {
        return GetDoubleValue(input, columnIndex);
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a double-precision floating point number.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Double">Double</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed. Therefore, the data retrieved must already be a double-precision floating point number.</remarks>
    public static double GetDoubleValue(this IDataReader input, int columnIndex, double nullValue)
    {
        double dblReturnValue = 0;

        try
        {
            if (input.IsDBNull(columnIndex) == false)
            {
                dblReturnValue = input.GetDouble(columnIndex);
            }
            else
            {
                dblReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            dblReturnValue = nullValue;
        }

        return dblReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a 16-bit signed integer.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <returns>The <see cref="Int16">Int16</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 16-bit signed integer.</remarks>
    public static short GetInt16Value(this IDataReader input, string columnName)
    {
        return GetInt16Value(input, columnName, 0);
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a 16-bit signed integer.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <param name="nullValue">The <see cref="Int16">Int16</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Int16">Int16</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 16-bit signed integer.</remarks>
    public static short GetInt16Value(this IDataReader input, string columnName, short nullValue)
    {
        short shtReturnValue = default(short);

        try
        {
            int intOrdinal = input.GetOrdinal(columnName);

            if (input.IsDBNull(intOrdinal) == false)
            {
                shtReturnValue = input.GetInt16(intOrdinal);
            }
            else
            {
                shtReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            shtReturnValue = nullValue;
        }

        return shtReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a 16-bit signed integer.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <returns>The <see cref="Int16">Int16</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 16-bit signed integer.</remarks>
    public static short GetInt16Value(this IDataReader input, int columnIndex)
    {
        return GetInt16Value(input, columnIndex);
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a 16-bit signed integer.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <param name="nullValue">The <see cref="Int16">Int16</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Int16">Int16</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 16-bit signed integer.</remarks>
    public static short GetInt16Value(this IDataReader input, int columnIndex, short nullValue)
    {
        short shtReturnValue = default(short);

        try
        {
            if (input.IsDBNull(columnIndex) == false)
            {
                shtReturnValue = input.GetInt16(columnIndex);
            }
            else
            {
                shtReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            shtReturnValue = nullValue;
        }

        return shtReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a 32-bit signed integer.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <returns>The <see cref="String">String</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 32-bit signed integer.</remarks>
    public static int GetInt32Value(this IDataReader input, string columnName)
    {
        return GetInt32Value(input, columnName);
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a 32-bit signed integer.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="String">String</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 32-bit signed integer.</remarks>
    public static int GetInt32Value(this IDataReader input, string columnName, int nullValue)
    {
        int intReturnValue = default(int);

        try
        {
            int intOrdinal = input.GetOrdinal(columnName);

            if (input.IsDBNull(intOrdinal) == false)
            {
                intReturnValue = input.GetInt32(intOrdinal);
            }
            else
            {
                intReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            intReturnValue = nullValue;
        }

        return intReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a 32-bit signed integer.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <returns>The <see cref="String">String</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 32-bit signed integer.</remarks>
    public static int GetInt32Value(this IDataReader input, int columnIndex)
    {
        return GetInt32Value(input, columnIndex, 0);
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a 32-bit signed integer.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="String">String</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 32-bit signed integer.</remarks>
    public static int GetInt32Value(this IDataReader input, int columnIndex, int nullValue)
    {
        int intReturnValue = default(int);

        try
        {
            if (input.IsDBNull(columnIndex) == false)
            {
                intReturnValue = input.GetInt32(columnIndex);
            }
            else
            {
                intReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            intReturnValue = nullValue;
        }

        return intReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a 64-bit signed integer.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <returns>The <see cref="Int64">Int64</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 64-bit signed integer.</remarks>
    public static long GetInt64Value(this IDataReader input, string columnName)
    {
        return GetInt64Value(input, columnName, 0);
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a 64-bit signed integer.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <param name="nullValue">The <see cref="Int64">Int64</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Int64">Int64</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 64-bit signed integer.</remarks>
    public static long GetInt64Value(this IDataReader input, string columnName, long nullValue)
    {
        long lngReturnValue = 0;

        try
        {
            int intOrdinal = input.GetOrdinal(columnName);

            if (input.IsDBNull(intOrdinal) == false)
            {
                lngReturnValue = input.GetInt64(intOrdinal);
            }
            else
            {
                lngReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            lngReturnValue = nullValue;
        }

        return lngReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a 64-bit signed integer.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <returns>The <see cref="Int64">Int64</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 64-bit signed integer.</remarks>
    public static long GetInt64Value(this IDataReader input, int columnIndex)
    {
        return GetInt64Value(input, columnIndex, 0);
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a 64-bit signed integer.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <param name="nullValue">The <see cref="Int64">Int64</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="Int64">Int64</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a 64-bit signed integer.</remarks>
    public static long GetInt64Value(this IDataReader input, int columnIndex, long nullValue)
    {
        long lngReturnValue = 0;

        try
        {
            if (input.IsDBNull(columnIndex) == false)
            {
                lngReturnValue = input.GetInt64(columnIndex);
            }
            else
            {
                lngReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            lngReturnValue = nullValue;
        }

        return lngReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a string.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <returns>The value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a string.</remarks>
    public static string GetStringValue(this IDataReader input, string columnName)
    {
        return GetStringValue(input, columnName, string.Empty);
    }

    /// <summary>
    /// Gets the value of the specified column (by name) as a string.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="String">String</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a string.</remarks>
    public static string GetStringValue(this IDataReader input, string columnName, string nullValue)
    {
        string strReturnValue = null;

        try
        {
            int intOrdinal = input.GetOrdinal(columnName);
            if (input.IsDBNull(intOrdinal) == false)
            {
                strReturnValue = input.GetString(intOrdinal);
            }
            else
            {
                strReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            strReturnValue = nullValue;
        }

        return strReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a string.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <returns>The <see cref="String">String</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a string.</remarks>
    public static string GetStringValue(this IDataReader input, int columnIndex)
    {
        return GetStringValue(input, columnIndex, string.Empty);
    }

    /// <summary>
    /// Gets the value of the specified column (by index) as a string.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <param name="nullValue">The <see cref="String">String</see> value to return if the desired column contains a null value.</param>
    /// <returns>The <see cref="String">String</see> value of the specified column.</returns>
    /// <remarks>No conversions are performed; therefore, the data retrieved must already be a string.</remarks>
    public static string GetStringValue(this IDataReader input, int columnIndex, string nullValue)
    {
        string strReturnValue = null;

        try
        {
            if (input.IsDBNull(columnIndex) == false)
            {
                strReturnValue = input.GetString(columnIndex);
            }
            else
            {
                strReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            strReturnValue = nullValue;
        }

        return strReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by name) in its native format.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnName">Name of the column to retrieve the desired data from.</param>
    /// <param name="nullValue">The value to return if the desired column contains a null value.</param>
    /// <returns>The value of the specified column.</returns>
    public static object GetValue(this IDataReader input, string columnName, object nullValue)
    {
        object objReturnValue = null;

        try
        {
            int intOrdinal = input.GetOrdinal(columnName);

            if (input.IsDBNull(intOrdinal) == false)
            {
                objReturnValue = input.GetValue(intOrdinal);
            }
            else
            {
                objReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            objReturnValue = nullValue;
        }

        return objReturnValue;
    }

    /// <summary>
    /// Gets the value of the specified column (by index) in its native format.
    /// If the value is null, the supplied value is used.
    /// </summary>
    /// <param name="input">The <see cref="IDataReader">IDataReader</see> containing the desired data.</param>
    /// <param name="columnIndex">The zero-based column ordinal.</param>
    /// <param name="nullValue">The value to return if the desired column contains a null value.</param>
    /// <returns>The value of the specified column.</returns>
    public static object GetValue(this IDataReader input, int columnIndex, object nullValue)
    {
        object objReturnValue = null;

        try
        {
            if (input.IsDBNull(columnIndex) == false)
            {
                objReturnValue = input.GetValue(columnIndex);
            }
            else
            {
                objReturnValue = nullValue;
            }
        }
        catch (Exception)
        {
            objReturnValue = nullValue;
        }

        return objReturnValue;
    }
}
#endregion
