using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections.Specialized;

namespace UniGuy.Core.Extensions
{
    #region Edge.Extensions
    //  http://edgeextensions.codeplex.com/SourceControl/list/changesets
    // <copyright file="DataTableExtensions.cs" company="Edge Extensions Project">
    // Copyright (c) 2009 All Rights Reserved
    // </copyright>
    // <author>Kevin Nessland</author>
    // <email>kevinnessland@gmail.com</email>
    // <date>2009-08-06</date>
    // <summary>Contains DataTable extension methods.</summary>
    /// <summary>
    /// Provides various DataTable-related extensions.
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Creates and adds a System.Data.DataColumn object to the System.Data.DataColumnCollection.
        /// </summary>
        /// <param name="table">DataTable to add the columns to.</param>
        /// <param name="columnName">Name of the column to add.</param>
        /// <returns>The new column.</returns>
        public static DataColumn AddColumn(this DataTable table, string columnName)
        {
            DataColumn column = new DataColumn(columnName);
            table.Columns.Add(column);

            return column;
        }

        #region WJ appended
        /// <summary>
        /// 取出表中指定名称的列并转换为字符串数组
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this DataTable @this, string columnName)
        {
            string[] strs = new string[@this.Rows.Count];
            for (int i = 0; i < @this.Rows.Count; i++)
            {
                strs[i] = @this.Rows[i][columnName].ToStringEx();
            }
            return strs;
        }
        /// <summary>
        /// 取出表中指定索引的列并转换为字符串数组
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this DataTable @this, int index)
        {
            string[] strs = new string[@this.Rows.Count];
            for (int i = 0; i < @this.Rows.Count; i++)
            {
                strs[i] = @this.Rows[i][index].ToStringEx();
            }
            return strs;
        }

        /// <summary>
        /// 取出某行转换为字符串字典
        /// </summary>
        /// <param name="this"></param>
        /// <param name="rowIndex">行号</param>
        /// <returns></returns>
        public static T ToStringDicionary<T>(this DataTable @this, int rowIndex) where T : IDictionary<string, string>, new()
        {
            System.Diagnostics.Debug.Assert(rowIndex >= 0 && rowIndex < @this.Rows.Count);

            T dict = new T();
            foreach (DataColumn column in @this.Columns)
                dict[column.ColumnName] = @this.Rows[rowIndex][column].ToStringEx();

            return dict;
        }
        /// <summary>
        /// 取出第一行转换为字符串字典
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToStringDicionary<T>(this DataTable @this) where T : IDictionary<string, string>, new()
        {
            return ToStringDicionary<T>(@this, 0);
        }
        #endregion
    }
    #endregion
}