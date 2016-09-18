using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// Sql 语句的参数字典
    /// </summary>
    [Serializable]
    public class SqlParameterDictionary:Dictionary<string, object>, IXmlSerializable
    {
        #region Interface implementations

        #region IXmlSerializable

        /* 序列化样式:
         * ------
         * <SqlParameterDictionary>
         *      <Item Key="key1" Value="abc"/>
         *      <Item Key="key1" QualifiedName="double">
         *          [double]
         *      </Item>
         *      ...
         * </SqlParameterDictionary>
         */

        public XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            string xmlRoot = UniGuy.Core.Helpers.SerializationHelper.GetXmlRoot(this.GetType());
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == xmlRoot)
            {
                if (reader.IsEmptyElement)
                    reader.Read();
                else
                {
                    reader.ReadStartElement();

                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Item")
                    {
                        string key = reader["Key"];
                        object value = null;
                        string qualifiedName = reader["QualifiedName"];
                        if (qualifiedName == null)
                        {
                            value = reader["Value"];
                            reader.Read();
                        }
                        else
                        {
                            XmlSerializer xs = new XmlSerializer(Type.GetType(qualifiedName));
                            reader.ReadStartElement();
                            value = xs.Deserialize(reader);
                            reader.ReadEndElement();
                        }
                        this[key] = value;
                    }

                    reader.ReadEndElement();
                }
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            foreach (var kv in this)
            {
                writer.WriteStartElement("Item");
                writer.WriteAttributeString("Key", kv.Key);
                if (kv.Value is string)
                {
                    writer.WriteAttributeString("Value", (string)kv.Value);
                }
                else if (kv.Value != null)
                {
                    Type t = kv.Value.GetType();
                    writer.WriteAttributeString("QualifiedName", t.AssemblyQualifiedName);
                    XmlSerializer xs = new XmlSerializer(t);
                    xs.Serialize(writer, kv.Value);
                }
                writer.WriteEndElement();
            }
        }

        #endregion

        #endregion

        #region Fields
        /// <summary>
        /// 一个单态可重用的参数字典
        /// </summary>
        private static SqlParameterDictionary parameters;
        #endregion  //  Fields

        #region Properties
        /// <summary>
        /// 获得单态的参数字典实例,不必每次都创建新的
        /// </summary>
        public static SqlParameterDictionary ClearedParameters
        {
            get
            {
                if (parameters == null)
                    parameters = new SqlParameterDictionary();
                else
                    parameters.Clear();
                return parameters;
            }
        }
        #endregion //   Properties

        #region Constructors
        /// <summary>
        /// 构造器,不区分大小写,否则sql中的遮罩太容易出错
        /// </summary>
        public SqlParameterDictionary() : base(StringComparer.InvariantCultureIgnoreCase) { }
        public SqlParameterDictionary(IDictionary<string, object> parameters) : base(parameters) { }
        #endregion  //  Constructors

        #region Methods
        /// <summary>
        /// 新增参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="parameterValue">参数值</param>
        /// <returns></returns>
        /// <remarks>可以级联操作</remarks>
        public SqlParameterDictionary Append(string parameterName, object parameterValue)
        {
            Add(parameterName, parameterValue);
            return this;
        }

        #endregion  //  Methods
    }
}
