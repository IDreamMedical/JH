using System;
using System.Xml;
using System.Collections.Generic;

namespace UniGuy.Core.Extensions
{
    /// <summary>
    /// 	Extension methods for the XmlNode / XmlDocument classes and its sub classes
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// 	Appends a child to a XML node
        /// </summary>
        /// <param name = "parentNode">The parent node.</param>
        /// <param name = "name">The name of the child node.</param>
        /// <returns>The newly cerated XML node</returns>
        public static XmlNode CreateChildNode(this XmlNode parentNode, string name)
        {
            var document = (parentNode is XmlDocument ? (XmlDocument)parentNode : parentNode.OwnerDocument);
            XmlNode node = document.CreateElement(name);
            parentNode.AppendChild(node);
            return node;
        }

        /// <summary>
        /// 	Appends a child to a XML node
        /// </summary>
        /// <param name = "parentNode">The parent node.</param>
        /// <param name = "name">The name of the child node.</param>
        /// <param name = "namespaceUri">The node namespace.</param>
        /// <returns>The newly cerated XML node</returns>
        public static XmlNode CreateChildNode(this XmlNode parentNode, string name, string namespaceUri)
        {
            var document = (parentNode is XmlDocument ? (XmlDocument)parentNode : parentNode.OwnerDocument);
            XmlNode node = document.CreateElement(name, namespaceUri);
            parentNode.AppendChild(node);
            return node;
        }

        /// <summary>
        /// 	Appends a CData section to a XML node
        /// </summary>
        /// <param name = "parentNode">The parent node.</param>
        /// <returns>The created CData Section</returns>
        public static XmlCDataSection CreateCDataSection(this XmlNode parentNode)
        {
            return parentNode.CreateCDataSection(string.Empty);
        }

        /// <summary>
        /// 	Appends a CData section to a XML node and prefills the provided data
        /// </summary>
        /// <param name = "parentNode">The parent node.</param>
        /// <param name = "data">The CData section value.</param>
        /// <returns>The created CData Section</returns>
        public static XmlCDataSection CreateCDataSection(this XmlNode parentNode, string data)
        {
            var document = (parentNode is XmlDocument ? (XmlDocument)parentNode : parentNode.OwnerDocument);
            var node = document.CreateCDataSection(data);
            parentNode.AppendChild(node);
            return node;
        }

        /// <summary>
        /// 	Returns the value of a nested CData section.
        /// </summary>
        /// <param name = "parentNode">The parent node.</param>
        /// <returns>The CData section content</returns>
        public static string GetCDataSection(this XmlNode parentNode)
        {
            foreach (var node in parentNode.ChildNodes)
            {
                if (node is XmlCDataSection)
                    return ((XmlCDataSection)node).Value;
            }
            return null;
        }

        /// <summary>
        /// 	Gets an attribute value
        /// </summary>
        /// <param name = "node">The node.</param>
        /// <param name = "attributeName">The Name of the attribute.</param>
        /// <returns>The attribute value</returns>
        public static string GetAttribute(this XmlNode node, string attributeName)
        {
            return GetAttribute(node, attributeName, null);
        }

        /// <summary>
        /// 	Gets an attribute value
        /// </summary>
        /// <param name = "node">The node.</param>
        /// <param name = "attributeName">The Name of the attribute.</param>
        /// <param name = "defaultValue">The default value to be returned if no matching attribute exists.</param>
        /// <returns>The attribute value</returns>
        public static string GetAttribute(this XmlNode node, string attributeName, string defaultValue)
        {
            var attribute = node.Attributes[attributeName];
            return (attribute != null ? attribute.InnerText : defaultValue);
        }

        /// <summary>
        /// 	Gets an attribute value converted to the specified data type
        /// </summary>
        /// <typeparam name = "T">The desired return data type</typeparam>
        /// <param name = "node">The node.</param>
        /// <param name = "attributeName">The Name of the attribute.</param>
        /// <returns>The attribute value</returns>
        public static T GetAttribute<T>(this XmlNode node, string attributeName)
        {
            return GetAttribute(node, attributeName, default(T));
        }

        /// <summary>
        /// 	Gets an attribute value converted to the specified data type
        /// </summary>
        /// <typeparam name = "T">The desired return data type</typeparam>
        /// <param name = "node">The node.</param>
        /// <param name = "attributeName">The Name of the attribute.</param>
        /// <param name = "defaultValue">The default value to be returned if no matching attribute exists.</param>
        /// <returns>The attribute value</returns>
        public static T GetAttribute<T>(this XmlNode node, string attributeName, T defaultValue)
        {
            var value = GetAttribute(node, attributeName);

            if (value.IsNotEmpty())
            {
                if (typeof(T) == typeof(Type))
                    return (T)(object)Type.GetType(value, true);

                return value.ConvertTo(defaultValue);
            }

            return defaultValue;
        }

        /// <summary>
        /// 	Creates or updates an attribute with the passed value.
        /// </summary>
        /// <param name = "node">The node.</param>
        /// <param name = "name">The name.</param>
        /// <param name = "value">The value.</param>
        public static void SetAttribute(this XmlNode node, string name, object value)
        {
            SetAttribute(node, name, value != null ? value.ToString() : null);
        }

        //  wj added. 上边方法对值类型数据有个object的装箱过程
        //  比如现在设置的value为bool类型, 要求为false的时候删除属性就得用这个方法
        public static void SetAttribute<T>(this XmlNode node, string name, T value)
        {
            SetAttribute(node, name, value.IsDefault() ? null : value.ToString());
        }

        /// <summary>
        /// 	Creates or updates an attribute with the passed value.
        /// </summary>
        /// <param name = "node">The node.</param>
        /// <param name = "name">The name.</param>
        /// <param name = "value">The value.</param>
        public static void SetAttribute(this XmlNode node, string name, string value)
        {
            /* wj commented
            if (node != null)
            {
                var attribute = node.Attributes[name, node.NamespaceURI];
                if (attribute == null)
                {
                    attribute = node.OwnerDocument.CreateAttribute(name, node.OwnerDocument.NamespaceURI);
                    node.Attributes.Append(attribute);
                }
                attribute.InnerText = value;
            }
            */

            //  wj added
            if (node != null)
            {
                XmlAttribute attribute = node.Attributes[name, node.NamespaceURI];
                //  如果设置为空则去掉属性(空和""是不一样的)
                if (value == null)
                {
                    if (attribute != null)
                        attribute.OwnerElement.Attributes.Remove(attribute);
                }
                else
                {
                    if (attribute == null)
                    {
                        attribute = node.OwnerDocument.CreateAttribute(name, node.OwnerDocument.NamespaceURI);
                        node.Attributes.Append(attribute);
                    }
                    attribute.Value = value;
                }
            }
        }

    #region WJ appended
        /// <summary>
        /// 以CData的方式设置InnerText;
        /// 至于GetInnerTextWithCData一般不需要实现, InnerText属性会自动解析;
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        public static void SetInnerTextWithCData(this XmlNode @this, string value)
        {
            @this.RemoveAll();
            @this.AppendChild((@this is XmlDocument ? @this : @this.OwnerDocument).CreateCDataSection(value));
        }
        public static string GetInnerTextWithCData(this XmlNode @this)
        {
            return GetCDataSection(@this);
        }
        #endregion //   WJ appended
    }

}