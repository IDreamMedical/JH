using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;

namespace UniGuy.Core.Helpers
{
    /// <summary>
    /// 辅助类
    /// </summary>
    public class SerializationHelper
    {
        #region Basic

        #endregion  Basic

        #region General

        #region Binary serialization
        /// <summary>
        /// 以二进制方式序列化对象
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="obj">对象</param>
        public static void BinarySerialize(string file, object obj)
        {
            using (FileStream fs = new FileStream(file, FileMode.Create))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(fs, obj);
                fs.Close();
            }
        }

        /// <summary>
        /// 以二进制方式反序列化对象
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>对象</returns>
        public static object BinaryDeserialize(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BinaryFormatter b = new BinaryFormatter();
                object obj = b.Deserialize(fs);
                fs.Close();
                return obj;
            }
        }

        /// <summary>
        /// 将对象序列化成字节数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static byte[] BinaryObjectToBytes<T>(T t)
        {
            using (MemoryStream mem = new MemoryStream())
            {
                BinaryFormatter binary = new BinaryFormatter();
                binary.Serialize(mem, t);
                return mem.ToArray();
            }
        }
        /// <summary>
        /// 将字节数组反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buff"></param>
        /// <returns></returns>
        public static T BinaryBytesToObject<T>(byte[] bytes)
        {
            using (MemoryStream mem = new MemoryStream(bytes))
            {
                mem.Position = 0;
                BinaryFormatter binary = new BinaryFormatter();
                return (T)binary.Deserialize(mem);
            }

        }
        /// <summary> 
        /// （结构体）数据转换 
        /// </summary> 
        /// <returns> </returns> 
        private byte[] StructToBytes<T>(T obj)
        {
            int size = Marshal.SizeOf(obj);
            byte[] bytes = new byte[size];
            IntPtr arrPtr = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
            Marshal.StructureToPtr(obj, arrPtr, true);
            return bytes;
        }
        private T BytesToStruct<T>(byte[] bytes)
        {
            IntPtr arrPtr = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
            return (T)Marshal.PtrToStructure(arrPtr, typeof(T));
        }
        public static byte[] Serialize(object obj)
        {
            BinaryFormatter binaryF = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                binaryF.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);

                byte[] buffer = new byte[(int)ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();

                return buffer;
            }
        }
        public static object Deserialize(byte[] buffer)
        {
            BinaryFormatter binaryF = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(buffer, 0, buffer.Length, false))
            {
                object obj = binaryF.Deserialize(ms);
                ms.Close();

                return obj;
            }
        }
        public byte[] StructToBytes(object structObj)
        {
            //得到结构体的大小 
            int size = Marshal.SizeOf(structObj);
            //分配结构体大小的内存空间 
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structObj, buffer, true);
                //创建byte数组 
                byte[] bytes = new byte[size];
                //从内存空间拷到byte数组 
                Marshal.Copy(buffer, bytes, 0, size);
                //返回byte数组 
                return bytes;
            }
            finally
            {
                //释放内存空间 
                Marshal.FreeHGlobal(buffer);
            }
        }
        public object BytesToStruct(byte[] bytes, Type strcutType)
        {
            //获取结构大小 
            int size = Marshal.SizeOf(strcutType);
            //byte数组长度小于结构的大小 
            if (size > bytes.Length)
            {
                //返回空 
                return null;
            }
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                //将byte数组拷到分配好的内存空间 
                Marshal.Copy(bytes, 0, buffer, size);
                //返回结构 
                return Marshal.PtrToStructure(buffer, strcutType);
            }
            finally
            {
                //释放内存空间 
                Marshal.FreeHGlobal(buffer);
            }
        }
        #endregion //   Binary serialization

        #region Xml serialization

        /// <summary>
        /// 获得某个类的Xml序列化根节点名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetXmlRoot<T>()
        {
            string strRoot =  typeof(T).Name;
            if (typeof(T).IsGenericType)
                strRoot = strRoot.Remove(strRoot.IndexOf('`'));

            object[] rootAttrs = typeof(T).GetCustomAttributes(typeof(XmlRootAttribute), false);
            if (rootAttrs.Length > 0)
                strRoot = ((XmlRootAttribute)rootAttrs[0]).ElementName;
            return strRoot;
        }

        /// <summary>
        /// 获得某个类的Xml序列化根节点名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetXmlRoot(Type type)
        {
            string strRoot = type.Name;
            object[] rootAttrs = type.GetCustomAttributes(typeof(XmlRootAttribute), false);
            if (rootAttrs.Length > 0)
                strRoot = ((XmlRootAttribute)rootAttrs[0]).ElementName;
            return strRoot;
        }

        /// <summary>
        /// 以Xml方式序列化对象
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        public static void XmlSerialize(string file,Type type, object obj)
        {
            XmlSerializer xs = new XmlSerializer(type);

            //  如果不存在文件夹,则自动建立
            DirectoryInfo di = new FileInfo(file).Directory;
            if (!di.Exists)
                di.Create();

            using (FileStream fs = new FileStream(file, FileMode.Create))
            {
                XmlWriterSettings xws = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
                XmlWriter xtw = XmlWriter.Create(fs, xws);
                xs.Serialize(xtw, obj);
                xtw.Close();
                fs.Close();
            }
        }

        /// <summary>
        /// 自动获得对象类型并以该类型进行序列化
        /// </summary>
        /// <param name="file"></param>
        /// <param name="obj"></param>
        public static void XmlSerialize(string file, object obj)
        {
            XmlSerialize(file, obj.GetType(), obj);
        }

        public static void XmlSerialize<T>(string file, T t)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(file, FileMode.Create))
            {
                XmlWriterSettings xws = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
                XmlWriter xtw = XmlWriter.Create(fs, xws);
                xs.Serialize(xtw, t);
                xtw.Close();
                fs.Close();
            }
        }

        /// <summary>
        /// 以Xml方式反序列化对象
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>对象</returns>
        public static object XmlDeserialize(string file, Type type)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                XmlSerializer xs = new XmlSerializer(type);
                object obj = xs.Deserialize(fs);
                fs.Close();
                return obj;
            }
        }
        public static T XmlDeserialize<T>(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                T t = (T)xs.Deserialize(fs);
                fs.Close();
                return t;
            }
        }

        public static byte[] XmlSerializeToBytes(object obj)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriterSettings xws = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
                XmlWriter xtw = XmlWriter.Create(ms, xws);
                xs.Serialize(xtw, obj);
                xtw.Close();
                return ms.GetBuffer();
            }
        }
        public static byte[] XmlSerializeToBytes<T>(T t)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriterSettings xws = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
                XmlWriter xtw = XmlWriter.Create(ms, xws);
                xs.Serialize(xtw, t);
                xtw.Close();
                return ms.GetBuffer();
            }
        }

        public static object XmlDeserializeFromBytes(Type type, byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                XmlSerializer xs = new XmlSerializer(type);
                object obj = xs.Deserialize(ms);
            return obj;
            }
        }
        public static T XmlDeserializeFromBytes<T>(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                T t = (T)xs.Deserialize(ms);
                return t;
            }
        }

        public static string XmlSerializeToString(object obj)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlWriterSettings xws = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
            XmlWriter xw = XmlWriter.Create(sw, xws);
            xs.Serialize(xw, obj);
            return sb.ToString();
        }
        public static string XmlSerializeToString(Type type, object obj)
        {
            XmlSerializer xs = new XmlSerializer(type);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlWriterSettings xws = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
            XmlWriter xw = XmlWriter.Create(sw, xws);
            xs.Serialize(xw, obj);
            return sb.ToString();
        }
        public static string XmlSerializeToString<T>(T t)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlWriterSettings xws = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
            XmlWriter xw = XmlWriter.Create(sw, xws);
            xs.Serialize(xw, t);
            return sb.ToString();
        }

        public static object XmlDeserializeFromString(Type type, string xml)
        {
            XmlSerializer xs = new XmlSerializer(type);
            StringReader sr = new StringReader(xml);
            XmlReader xr = XmlReader.Create(sr);
            return xs.Deserialize(xr);
        }
        public static T XmlDeserializeFromString<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringReader sr = new StringReader(xml);
            XmlReader xr = XmlReader.Create(sr);
            return (T)xs.Deserialize(xr);
        }
        #endregion //   Xml serialization

        #endregion  //  General

        #region Others

        #region Xml serialization(供具体包含这些类型成员的类的Xml序列化方法调用)

        #region Dictionay
        /// <summary>
        /// 序列化IDictionary<string, T>
        /// 序列化样式:
        /// <code>
        /// <TDictionary>
        ///     <DictionaryEntry Key="Key0">
        ///         <TValue.../>
        ///     </DictionaryEntry>
        ///     <DictionaryEntry Key="Key1">
        ///         <TValue.../>
        ///     </DictionaryEntry>
        ///     ...
        /// </TDictionary>
        /// </code>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static TDictionary ReadDictionary<TValue, TDictionary>(XmlReader reader) where TDictionary:class, IDictionary<string, TValue>, new()
        {
            TDictionary dictionary = null;
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == GetXmlRoot<TDictionary>())
            {
                dictionary = new TDictionary();

                if (reader.IsEmptyElement)
                    reader.Read();
                else
                {
                    reader.ReadStartElement();
                    XmlSerializer xs = new XmlSerializer(typeof(TValue));
                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "DictionaryEntry")
                    {
                        string key = reader["Key"];
                        if (reader.IsEmptyElement)
                            reader.Read();
                        else
                        {
                            reader.ReadStartElement();
                            dictionary.Add(key, (TValue)xs.Deserialize(reader));

                            reader.ReadEndElement();
                        }
                    }

                    reader.ReadEndElement();
                }
            }
            return dictionary;
        }
        public static void WriteDictionary<TValue, TDictionary>(XmlWriter writer, TDictionary dictionary) where TDictionary : class, IDictionary<string, TValue>, new()
        {
            writer.WriteStartElement(GetXmlRoot<TDictionary>());

            XmlSerializer xs = new XmlSerializer(typeof(TValue));
            foreach (string key in dictionary.Keys)
            {
                writer.WriteStartElement("DictionaryEntry");
                writer.WriteAttributeString("Key", key);
                xs.Serialize(writer, dictionary[key]);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }
        #endregion //   Dictionary

        #region List
        /// <summary>
        /// 序列化IList
        /// -------------------------------
        /// 序列化样式:
        /// <code>
        /// <TList>
        ///     <TValue.../>
        ///     <TValue.../>
        ///     ...
        /// </TList>
        /// </code>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TList"></typeparam>
        /// <param name="reader"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static TList ReadList<TValue, TList>(XmlReader reader) where TList : class, IList<TValue>, new()
        {
            TList list = null;

            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == GetXmlRoot<TList>())
            {
                list = new TList();

                if (reader.IsEmptyElement)
                    reader.Read();
                else
                {
                    reader.ReadStartElement();
                    XmlSerializer xs = new XmlSerializer(typeof(TValue));
                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == GetXmlRoot<TValue>())
                    {
                        list.Add((TValue)xs.Deserialize(reader));
                    }

                    reader.ReadEndElement();
                }
            }

            return list;
        }

        public static void WriteList<TValue, TList>(XmlWriter writer, TList list) where TList : class, IList<TValue>, new()
        {
            writer.WriteStartElement(GetXmlRoot<TList>());

            XmlSerializer xs = new XmlSerializer(typeof(TValue));
            foreach (TValue value in list)
            {
                xs.Serialize(writer, value);
            }

            writer.WriteEndElement();
        }
        #endregion //   List
        #endregion //   Xml serialization

        #endregion //   Others
    }
}
