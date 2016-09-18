using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using UniGuy.Core.Extensions;

namespace UniGuy.Core.DataStructures
{
    /// <summary>
    /// 表(用于替换System.Data中的DataTable)
    /// </summary>
    [XmlRoot("Table")][Serializable]
    public class Table:Collection<TableRow>, IXmlSerializable
    {
        private readonly object _syncRoot = new object();

        private TableColumn[] columns;

        public TableColumn[] Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public Table() { }

        public Table(params TableColumn[] columns) : this() { this.Columns = columns; }

        public Table(params string[] columnNames):this()
        {
            TableColumn[] columns = new TableColumn[columnNames.Length];
            for (int i = 0; i < columnNames.Length; i++)
                columns[i] = new TableColumn() { Name = columnNames[i] };
            this.Columns = columns;
        }


        public void AddColumn(TableColumn column)
        {
            lock (_syncRoot)
            {
                if (Columns == null)
                    Columns = new TableColumn[] { column };
                else
                    Columns = Columns.Add(column);
                //  调整各行cells
                foreach (TableRow row in this)
                    row.cells.Add(null);
            }
        }
        public void AddColumn(string columnName, Type type)
        {
            AddColumn(new TableColumn(columnName, type));
        }
        public void AddColumn(string columnName)
        {
            AddColumn(new TableColumn(columnName));
        }
        public void InsertColumn(int index, TableColumn column)
        {
            lock (_syncRoot)
            {
                if (Columns == null)
                    throw new Exception("Columns can not be null.");
                else
                    Columns = Columns.Insert(index, column);
                //  调整各行cells
                foreach (TableRow row in this)
                    row.cells.Insert(index, null);
            }
        }
        public void InsertColumn(int index, string columnName, Type type)
        {
            InsertColumn(index, new TableColumn(columnName, type));
        }
        public void InsertColumn(int index, string columnName)
        {
            InsertColumn(index, new TableColumn(columnName));
        }
        public void RemoveColumn(int index)
        {
            lock (_syncRoot)
            {
                if (Columns == null)
                    throw new Exception("Columns can not be null.");
                else
                    Columns = Columns.Remove(index);
                //  调整各行cells
                foreach (TableRow row in this)
                    row.cells.Remove(index);
            }
        }

        public void ClearColumns()
        {
            lock (_syncRoot)
            {
                if (Columns == null)
                    throw new Exception("Columns can not be null.");
                else
                    Columns = null;
                //  调整各行cells
                foreach (TableRow row in this)
                    row.cells = null;
            }
        }

        public TableRow CreateRow()
        {
            TableRow row = new TableRow { Table = this };
            row.InitializeCells();
            return row;
        }

        #region Overrides

        protected override void InsertItem(int index, TableRow item)
        {
            //  反持久化的时候会用到
            if (item.Table == null)
                item.Table = this;
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, TableRow item)
        {
            //  反持久化的时候会用到
            if (item.Table == null)
                item.Table = this;
            base.SetItem(index, item);
        }
        #endregion

        #region IXmlSerializable 成员

        public XmlSchema GetSchema()
        {
            return null;
        }
        /*
         * 序列化样式:
         *  <SomeTable>
         *      <SomeTable.Columns>
         *          <SomeColumn Name="aa" Type="[AssemblyQualifiedName]"/>
         *          <SomeColumn Name="bb" Type="[AssemblyQualifiedName]"/>
         *          ...
         *      </SomeTable.Columns>
         *      <SomeTable.Rows>
         *          <SomeRow>
         *              <Cell Name="aa" Type="System.String" Value="aa1" .../>
         *              <Cell Name="bb" Type="System.Int32" Value="123" .../>
         *          </SomeRow>
         *          ...
         *      </SomeTable.Rows>
         *  </SomeTable>
         */
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

                    //  读取Columns定义
                    if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == xmlRoot + ".Columns")
                    {
                        if (reader.IsEmptyElement)
                            reader.Read();
                        else
                        {
                            reader.ReadStartElement();
                            XmlSerializer xs = new XmlSerializer(typeof(TableColumn));
                            string xmlRootTColumn = UniGuy.Core.Helpers.SerializationHelper.GetXmlRoot(typeof(TableColumn));
                            while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == xmlRootTColumn)
                                this.AddColumn((TableColumn)xs.Deserialize(reader));
                            reader.ReadEndElement();
                        }
                    }

                    //  读取Rows数据
                    if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == xmlRoot + ".Rows")
                    {
                        if (reader.IsEmptyElement)
                            reader.Read();
                        else
                        {
                            reader.ReadStartElement();
                            XmlSerializer xs = new XmlSerializer(typeof(TableRow));
                            string xmlRootTRow = UniGuy.Core.Helpers.SerializationHelper.GetXmlRoot(typeof(TableRow));
                            while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == xmlRootTRow)
                                this.Add((TableRow)xs.Deserialize(reader));
                            reader.ReadEndElement();
                        }
                    }

                    reader.ReadEndElement();
                }
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            string xmlRoot = UniGuy.Core.Helpers.SerializationHelper.GetXmlRoot(this.GetType());

            XmlSerializer xs = null;
            if (Columns != null)
            {
                writer.WriteStartElement(xmlRoot + ".Columns");
                xs = new XmlSerializer(typeof(TableColumn));
                foreach (TableColumn column in Columns)
                    xs.Serialize(writer, column);
                writer.WriteEndElement();
            }

            writer.WriteStartElement(xmlRoot + ".Rows");
            xs = new XmlSerializer(typeof(TableRow));
            foreach (TableRow row in this)
                xs.Serialize(writer, row);
            writer.WriteEndElement();
        }

        #endregion
    }
}
