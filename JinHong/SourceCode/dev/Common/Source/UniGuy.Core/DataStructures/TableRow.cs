using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using UniGuy.Core.Extensions;

namespace UniGuy.Core.DataStructures
{
    /// <summary>
    /// 表行
    /// </summary>
    /// <typeparam name="TColumn"></typeparam>
    /// <remarks>注意Xml持久化只支持TableColumn中的Type为字符串</remarks>
    [Serializable]
    [XmlRoot("TableRow")]
    public class TableRow : IXmlSerializable
    {
        private Table table;
        internal object[] cells;

        public Table Table
        {
            get { return table; }
            internal set { table = value; }
        }

        internal TableRow() { }
        internal TableRow(Table table)
        {
            this.Table = table;
            InitializeCells();
        }

        public object this[TableColumn column]
        {
            get
            {
                int columnIndex = Table.Columns.IndexOf(column, EqualityComparer<TableColumn>.Default);
                return this[columnIndex, column.Type];
            }
            set
            {
                int columnIndex = Table.Columns.IndexOf(column, EqualityComparer<TableColumn>.Default);
                this[columnIndex, column.Type] = value;
            }
        }

        public object this[string columnName]
        {
            get
            {
                TableColumn column = Table.Columns.First(col => col.Name == columnName);
                if (column == null)
                    throw new Exception("Column with specified name not found.");
                return this[column];
            }
            set
            {
                TableColumn column = Table.Columns.First(col => col.Name == columnName);
                if (column == null)
                    throw new Exception("Column with specified name not found.");
                this[column] = value;
            }
        }

        private object this[int index, Type type]
        {
            get
            {
                if (cells == null)
                    return type.GetDefaultValueOfType();

                object obj = cells[index];
                if (!(type == null || (obj == null && type.IsByRef) || obj.IsAssignableTo(type)))
                    throw new Exception("Type mismatch.");

                return obj;
            }
            set
            {
                if (!(type == null || value.IsAssignableTo(type)))
                    throw new Exception("Type mismatch.");

                cells[index] = value;
            }
        }

        public object this[int index]
        {
            get
            {
                return this[index, Table.Columns[index].Type];
            }
            set
            {
                this[index, Table.Columns[index].Type] = value;
            }
        }

        /// <summary>
        /// 根据列定义初始化行数据
        /// </summary>
        internal void InitializeCells()
        {
            System.Diagnostics.Debug.Assert(Table.Columns != null);

            cells = new object[Table.Columns.Length];
            for (int i = 0; i < Table.Columns.Length; i++)
                cells[i] = Table.Columns[i].Type == null ? null : Table.Columns[i].Type.GetDefaultValueOfType();
        }

        #region IXmlSerializable 成员

        public virtual XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            //  根节点名称
            string xmlRoot = UniGuy.Core.Helpers.SerializationHelper.GetXmlRoot(this.GetType());

            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == xmlRoot)
            {
                if (reader.IsEmptyElement)
                    reader.Read();
                else
                {
                    reader.ReadStartElement();
                    List<object> cellList = new List<object>();
                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Cell")
                    {
                        Type type = null;
                        if (reader["Type"] != null)
                            type = Type.GetType(reader["Type"]);
                        object cellValue = null;
                        if (reader["Value"] != null)
                            cellValue = reader["Value"];
                        if (type != null)
                            cellValue = cellValue.ConvertTo(type);
                        cellList.Add(cellValue);
                        reader.Read();
                    }

                    cells = cellList.ToArray();
                    reader.ReadEndElement();
                }
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            if (Table.Columns != null)
            {
                for (int i = 0; i < Table.Columns.Length; i++)
                {
                    TableColumn column = Table.Columns[i];
                    writer.WriteStartElement("Cell");
                    writer.WriteAttributeString("Name", column.Name);
                    if (column.Type != null)
                        writer.WriteAttributeString("Type", column.Type.AssemblyQualifiedName);
                    if (!EqualityComparer<object>.Default.Equals(cells[i], column.Type==null?null:column.Type.GetDefaultValueOfType()))
                        writer.WriteAttributeString("Value", (string)cells[i].ConvertTo(typeof(string)));
                    writer.WriteEndElement();
                }
            }
        }

        #endregion
    }
}
