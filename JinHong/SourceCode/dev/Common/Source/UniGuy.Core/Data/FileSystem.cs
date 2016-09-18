using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UniGuy.Core.Helpers;
using System.Configuration;
using System.IO;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 文件系统
    /// </summary>
    public class FileSystem
    {
        #region Fields
        /// <summary>
        /// 根目录
        /// </summary>
        private static string rootDir;
        #endregion

        #region Properties
        /// <summary>
        /// 获得或者设置根目录
        /// </summary>
        public static string RootDir
        {
            get { return rootDir; }
            set { rootDir = value; }
        }
        #endregion

        #region Ctor
        #endregion

        //TODO IdObject添加FileSystemOpenMethodAttribute和FileSystemSaveMethodAttribute，用来定义自定义打开和保存方法
        #region Methods
        /// <summary>
        /// 打开Id对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="id">对象标识</param>
        /// <returns></returns>
        public static IIdObject Open(Type type, string id)
        {
            string[] files = Directory.GetFiles(folder, string.Format("{0}_{1}.phi", type.Name, id), SearchOption.AllDirectories);
            if (files.Length > 0)
                return SerializationHelper.Deserialize(File.ReadAllBytes(files[0])) as IIdObject;
            return null;
        }
        /// <summary>
        /// 保存Id对象
        /// </summary>
        /// <param name="obj">对象实例</param>
        public static void Save(IIdObject obj)
        {
            Type type = obj.GetType();
            string file = string.Format(@"{0}\{1}\{1}_{2}.phi", folder, type.Name, obj.Id);
            DirectoryInfo dir = Directory.GetParent(file);
            if (!Directory.Exists(dir.FullName))
                Directory.CreateDirectory(dir.FullName);
            File.WriteAllBytes(file, SerializationHelper.Serialize(obj));
        }
        /// <summary>
        /// 删除Id对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="id">对象标识</param>
        public static void Delete(Type type, string id)
        {
            string file = string.Format(@"{0}\{1}\{1}_{2}.phi", folder, type.Name, id);
            File.Delete(file);
        }
        //public static void Delete(IIdObject
        public static bool Exists(Type type, string id)
        {
            string file = string.Format(@"{0}\{1}\{1}_{2}.phi", folder, type.Name, id);
            return File.Exists(file);
        }
        public static bool Exists(IIdObject obj)
        {
            return Exists(obj.GetType(), obj.Id);
        }
        public static string[] GetIds(Type type)
        {
            return Directory.GetFiles(folder, string.Format("{0}_*",type.Name), SearchOption.AllDirectories);
        }
        #endregion
    }
}
