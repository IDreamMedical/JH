using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace UniGuy.Core.DataStructures
{
    [XmlRoot("TableColumn")]
    [Serializable]
    public class TableColumn: IXmlSerializable
    {
        private string name;
        private Type type;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Type Type
        {
            get { return type; }
            set { type = value; }
        }

        public TableColumn() { }
        public TableColumn(string name) { this.Name = name; }
        public TableColumn(string name, Type type) : this(name) { this.Type = type; }

        #region IXmlSerializable 成员

        public virtual XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            string xmlRoot = UniGuy.Core.Helpers.SerializationHelper.GetXmlRoot(this.GetType());

            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == xmlRoot)
            {
                //  读取属性
                string attrValue = reader["Name"];
                if (attrValue != null)
                    Name = attrValue;
                attrValue = reader["Type"];
                if (attrValue != null)
                    Type = Type.GetType(attrValue);

                System.Diagnostics.Debug.Assert(reader.IsEmptyElement);
                reader.Read();
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            if (Name != null)
                writer.WriteAttributeString("Name", Name);
            if (Type != null)
                writer.WriteAttributeString("Type", Type.AssemblyQualifiedName);
        }

        #endregion
    }
}
