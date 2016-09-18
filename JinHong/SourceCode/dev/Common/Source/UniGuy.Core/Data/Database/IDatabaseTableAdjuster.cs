using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 数据库表调整器接口
    /// </summary>
    /// <remarks>
    /// 很多时候我们的数据库表希望程序自动创建; 尤其是面向对象编程, 对象的持久化总还会用到数据库表, 但是手工建立容易出错也麻烦;
    /// 这个接口就是用来根据表名和列定义自动建表的
    /// </remarks>
    public interface IDatabaseTableAdjuster
    {
        /// <summary>
        /// 是否存在表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        bool ExistsTable(string tableName);
        /// <summary>
        /// 创建表(必须不存在表)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnDefinitions">列定义</param>
        void CreateTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions);
        /// <summary>
        /// 修改表(必须存在表, 根据列定义重新调整表结构; 可能抛异常, 比如把列长改小但已经有较长数据在表中; 这个方法可能删除列, 导致数据丢失)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnDefinitions">列定义</param>
        void ModifyTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions);
        /// <summary>
        /// 扩展修改表(同上,不过列和列长都是只增不减,不丢数据, 所以正常情况下不会异常)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnDefinitions">列定义</param>
        void AppendModifyTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions);
        
    }

    /// <summary>
    /// 数据库表调整器基类, 具体实现不一定继承此类, 可以选择上面接口, 由于单继承, 继承接口更通用
    /// 这个类不过是先实现了一个EnsureTable
    /// </summary>
    public abstract class DatabaseTableAdjusterBase : IDatabaseTableAdjuster
    {
        #region IDatabaseTableAdjuster 成员

        public abstract bool ExistsTable(string tableName);

        public abstract void CreateTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions);

        public abstract void ModifyTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions);

        public abstract void AppendModifyTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions);

        /// <summary>
        /// 如果没有表则建表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnDefinitions"></param>
        public void CreateTableIfNotExist(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions)
        {
            if (!ExistsTable(tableName))
                CreateTable(tableName, columnDefinitions);
        }

        /// <summary>
        /// 如果没有表则建表, 否则AppendModify
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnDefinitions"></param>
        public void EnsureTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions)
        {
            if (!ExistsTable(tableName))
                CreateTable(tableName, columnDefinitions);
            else
                AppendModifyTable(tableName, columnDefinitions);
        }
        #endregion
    }

    /// <summary>
    /// 数据库表列定义结构
    /// </summary>
    /// <remarks>
    /// 本设计只把数据库看做数据存放工具, 不考虑扩展功能, 甚至数据类型都不考虑, 一律只认为有字符串类型
    /// </remarks>
    public struct DataColumnDefinition: IEquatable<DataColumnDefinition>
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName;
        /// <summary>
        /// 列长
        /// </summary>
        public int Length;
        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool IsNullable;

        public DataColumnDefinition(string columnName, int length) : this(columnName, length, false) { }
        public DataColumnDefinition(string columnName, int length, bool isNullable)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");
            this.ColumnName = columnName;
            this.Length = length;
            this.IsNullable = isNullable; 
        }

        #region IEquatable<DataColumnDefinition> 成员

        /// <summary>
        /// 判断是否相等, 名称不区分大小写, 因为往往进行此判断之前已经用NameEquals判断过并确保名称相符
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DataColumnDefinition other)
        {
            return (!Object.ReferenceEquals(other, null)
            &&  StringComparer.InvariantCultureIgnoreCase.Compare(this.ColumnName, other.ColumnName) == 0
            && this.Length == other.Length
                && this.IsNullable == other.IsNullable);
        }

        /// <summary>
        /// 判断名称是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public bool NameEquals(DataColumnDefinition other, bool caseSensitive)
        {
            return (!Object.ReferenceEquals(other, null)
                && (caseSensitive?
                StringComparer.InvariantCulture.Compare(this.ColumnName, other.ColumnName)
                : StringComparer.InvariantCultureIgnoreCase.Compare(this.ColumnName, other.ColumnName)) == 0
                );
        }

        public override int GetHashCode()
        {
            return ColumnName.GetHashCode() ^ Length.GetHashCode() ^ IsNullable.GetHashCode();
        }

        public static bool operator ==(DataColumnDefinition a, DataColumnDefinition b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(DataColumnDefinition a, DataColumnDefinition b)
        {
            return !a.Equals(b);
        }

        #endregion
    }
}
