using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using UniGuy.Core.Data;
using UniGuy.Core.Helpers;
using UniGuy.Core;
using JinHong.ServiceMainReference;
using System.IO;
using System.Security.Cryptography;
using UniGuy.Core.Extensions;

namespace JinHong
{
    public class ServiceClientExceptionOccurredEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
        public ServiceClientExceptionOccurredEventArgs(Exception ex) : base() { this.Exception = ex; }
    }

    public delegate void ServiceClientExceptionOccurredEventHandler(object sender, ServiceClientExceptionOccurredEventArgs e);

    /// <summary>
    /// JinHong WebService客户端, 只提供基本的数据库CRUD方法, 复杂逻辑在客户端处理;
    /// 对于复杂参数, 通过序列化后的字符串进行传递, 当然用二进制序列化快, 但这里还是用了Xml序列化; 可能会有性能影响, 但本系统主要是查表等, 单个对象的增删改性能影响应该不大;
    /// </summary>
    public class ServiceMainClient
    {
        #region Events

        public event ServiceClientExceptionOccurredEventHandler ServiceClientExceptionOccurred;

        public void OnServiceClientExceptionOccurred(Exception ex)
        {
            if (ServiceClientExceptionOccurred != null)
                ServiceClientExceptionOccurred(this, new ServiceClientExceptionOccurredEventArgs(ex));
        }

        #endregion

        #region Methods

        #region Database

        public DataSet Select(string sql, SqlParameterDictionary parameters = null)
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    return client.Select(sql, parameters == null ? null : SerializationHelper.XmlSerializeToString(parameters));
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }

            return null;
        }

        public T Scalar<T>(string sql, SqlParameterDictionary parameters = null) 
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    object obj = client.Scalar(sql, parameters==null?null:SerializationHelper.XmlSerializeToString(parameters));
                    //return strObject == null ? default(T) : SerializationHelper.XmlDeserializeFromString<T>(strObject);
                    return obj == null ? default(T):(T)Convert.ChangeType(obj, typeof(T));
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }

            return default(T);
        }

        public int NonQuery(string sql, SqlParameterDictionary parameters = null)
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    return client.NonQuery(sql, parameters == null ? null : SerializationHelper.XmlSerializeToString(parameters));
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }
            return -1;
        }

        public string[] GetIds<T>() where T:IIdObject
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    return client.GetIds(typeof(T).AssemblyQualifiedName);
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }
            return null;
        }

        public string[] GetColumnData<T>(string columnName) where T : IIdObject
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    return client.GetColumnData(typeof(T).AssemblyQualifiedName, columnName);
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }
            return null;
        }

        public bool Exists<T>(string id) where T : IIdObject
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    return client.Exists(typeof(T).AssemblyQualifiedName, id);
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }
            return false;
        }

        public T Load<T>(string id) where T : IIdObject
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    string strObject = client.Load(typeof(T).AssemblyQualifiedName, id);
                    return strObject == null ? default(T) : SerializationHelper.XmlDeserializeFromString<T>(strObject);
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }
            return default(T);
        }

        public bool Delete<T>(string id) where T : IIdObject
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    return client.Delete(typeof(T).AssemblyQualifiedName, id);
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }
            return false;
        }

        public bool Insert<T>(T obj) where T : IIdObject
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    return client.Insert(typeof(T).AssemblyQualifiedName, SerializationHelper.XmlSerializeToString<T>(obj));
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }
            return false;
        }

        public bool Update<T>(T obj) where T : IIdObject
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    return client.Update(typeof(T).AssemblyQualifiedName, SerializationHelper.XmlSerializeToString<T>(obj));
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }
            return false;
        }

        /// <summary>
        /// TODO:
        /// 这个保存方法可能会导致错误, 原因是会自动建表, 而自动建表对某些类型数据库暂时没有支持
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Save<T>(T obj) where T : IIdObject
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    return client.Save(typeof(T).AssemblyQualifiedName, SerializationHelper.XmlSerializeToString<T>(obj));
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }
            return false;
        }

        public bool Save2<T>(T obj) where T : IIdObject
        {
            using (ServiceMainSoapClient client = new ServiceMainSoapClient())
            {
                try
                {
                    string typeName = typeof(T).AssemblyQualifiedName;
                    string strObject = SerializationHelper.XmlSerializeToString<T>(obj);
                    return client.Exists(typeName, obj.Id) ? client.Update(typeName, strObject) :
                        client.Insert(typeName, strObject);
                }
                catch (Exception ex)
                {
                    OnServiceClientExceptionOccurred(ex);
                }
            }
            return false;
        }

        //  TODO

        #endregion

        #region File system

        public bool ExistsDirectory(string path)
        {
            
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                {
                    return client.ExistsDirectory(path);
                }
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
            
            return false;
        }

        public bool ExistsFile(string path)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                return client.ExistsFile(path);
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
            return false;
        }

        public void DeleteDirectory(string path)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                    client.DeleteDirectory(path);
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                    client.DeleteFile(path);
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
        }

        public void RenameDirectory(string path, string newPath)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                    client.RenameDirectory(path, newPath);
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
        }

        public void RenameFile(string path, string newPath)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                    client.RenameFile(path, newPath);
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
        }

        /// <summary>
        /// 获得文件信息
        /// </summary>
        /// <param name="address"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetFile(string path)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                return client.GetFile(path);
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
            return null;
        }

        /// <summary>
        /// 获得文件夹信息
        /// </summary>
        /// <param name="address"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetDirectory(string path)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                return client.GetDirectory(path);
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
            return null;
        }

        public string[] GetDirectories(string path)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                return client.GetDirectories(path);
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
            return null;
        }

        public string[] GetFiles(string path)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                return client.GetFiles(path);
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
            return null;
        }

        /// <summary>
        /// 下载文件到文件
        /// </summary>
        /// <param name="address"></param>
        /// <param name="path"></param>
        public void DownloadFile(string path, string localPath)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                {
                    byte[] bsFile = client.DownloadFile(path);
                    FileInfo fi = new FileInfo(localPath);
                    if (!Directory.Exists(fi.DirectoryName))
                        Directory.CreateDirectory(fi.DirectoryName);
                    using (FileStream fs = new FileStream(localPath, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(bsFile, 0, (int)bsFile.Length);
                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
        }

        /// <summary>
        /// 上传文件到文件
        /// </summary>
        /// <param name="address"></param>
        /// <param name="path"></param>
        /// <param name="cFile"></param>
        /// <returns></returns>
        public void UploadFile(string path, string localPath)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                {
                    byte[] bsFile = null;
                    using (FileStream fs = new FileStream(localPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        bsFile = new byte[fs.Length];
                        fs.Read(bsFile, 0, (int)fs.Length);
                        fs.Close();
                    }
                    client.UploadFile(path, bsFile);
                }
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }
        }

        public byte[] GetFileMD5(string path)
        {
            try
            {
                using (ServiceMainSoapClient client = new ServiceMainSoapClient())
                    return client.GetFileMD5(path);
            }
            catch (Exception ex)
            {
                OnServiceClientExceptionOccurred(ex);
            }

            return null;
        }

        public bool IsFileMD5Equals(string path, string localPath)
        {
            byte[] hash1 = GetFileMD5(path);
            byte[] hash2;
            using (MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider())
            using (FileStream fs = new FileStream(localPath, FileMode.Open))
                hash2 = hashProvider.ComputeHash(fs);
            if (hash1 != null && hash2 != null && hash1.Length == hash2.Length)
                return hash1.IsEquals(hash2);
            return false;
        }

        #endregion

        #endregion

        //  TODO
    }
}
