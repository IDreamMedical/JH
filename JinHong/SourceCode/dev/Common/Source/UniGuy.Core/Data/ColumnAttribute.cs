using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace UniGuy.Core.Data
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Class, Inherited=false, AllowMultiple=false), Serializable]
    public class ColumnAttribute:Attribute
    {
        public const int DEFAULT_COLUMNSIZE = 100;
        protected string columnName;
        protected int columnSize = DEFAULT_COLUMNSIZE;
        protected DBType dbType = DBType.Default;
        protected SerializationType serializationType = SerializationType.None;
        protected ColumnIoType columnIoType = ColumnIoType.ReadWrite;

        /// <summary>
        /// 如果为数组可使用该属性
        /// </summary>
        protected Type arrayItemType;

        public ColumnAttribute() { }
        public ColumnAttribute(string columnName)
            : this()
        {
            this.columnName = columnName;
        }
        public ColumnAttribute(string columnName, int columnSize)
            : this(columnName)
        {
            this.columnSize = columnSize;
        }
        public ColumnAttribute(string columnName, int columnSize, DBType dbType)
            : this(columnName, columnSize)
        {
            this.dbType = dbType;
        }
        public ColumnAttribute(string columnName, DBType dbType)
            : this(columnName)
        {
            this.dbType = dbType;
        }

        /// <summary>
        /// 获得或者设置列名
        /// </summary>
        public string ColumnName
        {
            set { this.columnName = value; }
            get { return columnName; }
        }

        /// <summary>
        /// 获得或者设置列长度
        /// </summary>
        public int ColumnSize
        {
            set { this.columnSize = value; }
            get { return columnSize; }
        }

        /// <summary>
        /// 获得或者设置字段类型
        /// </summary>
        public DBType DbType
        {
            set { this.dbType = value; }
            get { return dbType; }
        }

        /// <summary>
        /// 获得或者设置序列化方法
        /// </summary>
        public SerializationType SerializationType
        {
            get { return serializationType; }
            set { serializationType = value; }
        }
        /// <summary>
        /// 获得或者设置字段读取写入方式
        /// </summary>
        public ColumnIoType ColumnIoType
        {
            get { return columnIoType; }
            set { columnIoType = value; }
        }

        /// <summary>
        /// TODO Not used anywhere NOW...
        /// </summary>
        public Type ArrayItemType
        {
            get { return arrayItemType; }
            set { arrayItemType = value; }
        }
    }
}
