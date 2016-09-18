using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    [AttributeUsage(AttributeTargets.Class, Inherited=false, AllowMultiple=false), Serializable]
    public class TableAttribute:Attribute
    {
        /// <summary>
        /// 保存表名的字段
        /// </summary>
        protected string tableName;

        public TableAttribute() { }

        public TableAttribute(string tableName)
        {
            this.tableName = tableName;
        }

        public string TableName
        {
            set { this.tableName = value; }
            get { return this.tableName; }
        }
    }
}
