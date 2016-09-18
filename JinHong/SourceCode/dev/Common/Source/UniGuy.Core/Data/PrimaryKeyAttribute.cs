using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    [AttributeUsageAttribute(AttributeTargets.Property, Inherited = false, AllowMultiple = false), Serializable]
    public class PrimaryKeyAttribute: ColumnAttribute
    {
        private PrimaryKeyType primaryKeyType = PrimaryKeyType.Identity;

        public PrimaryKeyAttribute()
        {
        }

        public PrimaryKeyAttribute(string columnName)
            : this()
        {
            this.columnName = columnName;
        }

        public PrimaryKeyAttribute(PrimaryKeyType primaryKeyType, string columnName):this(columnName)
        {
            this.primaryKeyType = primaryKeyType;
        }

        public PrimaryKeyAttribute(PrimaryKeyType primaryKeyType, string columName, DBType dbType)
            : this(primaryKeyType, columName)
        {
            this.DbType = dbType;
        }

        /// <summary>
        /// 主键类型，默认为自增的
        /// </summary>
        public PrimaryKeyType PrimaryKeyType
        {
            set
            {
                this.primaryKeyType = value;
            }
            get
            {
                return this.primaryKeyType;
            }
        }
    }
}
