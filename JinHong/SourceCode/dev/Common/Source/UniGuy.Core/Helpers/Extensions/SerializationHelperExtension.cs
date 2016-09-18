using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;
using UniGuy.Core.Helpers;

//  WJ
//  Created 20110428
//  LastModified    20110428
namespace UniGuy.Core.Helpers
{
    public static class SerializationHelperExtension
    {
        #region Binary serialization
        public static void BinarySerialize(this object @this, string file)
        {
            SerializationHelper.BinarySerialize(file, @this);
        }

        public static byte[] BinaryObjectToBytes<T>(this T @this)
        {
            return SerializationHelper.BinaryObjectToBytes(@this);
        }

        public static T BinaryBytesToObject<T>(this byte[] @this)
        {
            return SerializationHelper.BinaryBytesToObject<T>(@this);
        }

        public static byte[] Serialize(this object @this)
        {
            return SerializationHelper.Serialize(@this);
        }

        public static object Deserialize(this byte[] @this)
        {
            return SerializationHelper.Deserialize(@this);
        }
        #endregion //   Binary serialization

        #region Xml serialization

        public static string GetXmlRoot<T>(this T @this)
        {
            return SerializationHelper.GetXmlRoot(typeof(T));
        }

        public static string GetXmlRoot(this Type @this)
        {
            return SerializationHelper.GetXmlRoot(@this);
        }

        public static void XmlSerialize<T>(this T @this, string file)
        {
            SerializationHelper.XmlSerialize<T>(file, @this);
        }

        public static string XmlSerializeToString<T>(this T t)
        {
            return SerializationHelper.XmlSerializeToString<T>(t);
        }

        #endregion //   Xml serialization

    }
}
