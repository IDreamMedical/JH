using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using System.IO;
using UniGuy.Core;
using UniGuy.Core.Data;
using UniGuy.Core.Helpers;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
//using UniGuy.Entity;

namespace JinHong.WebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ServiceMain : System.Web.Services.WebService
    {
        #region Fields

        const string DB_FILE_NAME = @"App_Data\JinHong.db";

        //  TODO 暂时使用SqliteDatabase, 之后替换为MySqlDatabase; 本机MySql安装后服务没有启动成功, 还没查到原因
        private string cs = null;
        private SQLiteDatabase db = null;
        private DatabaseProxy dbp = null;

        #endregion

        #region Properties

        public string Cs
        {
            get
            {
                return cs ?? (cs = string.Format(@"Data Source={0};Pooling=true;", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DB_FILE_NAME)));
            }
        }

        public SQLiteDatabase Db
        {
            get { return db ?? (db = new SQLiteDatabase()); }
        }

        public DatabaseProxy Dbp
        {
            get { return dbp ?? (dbp = new DatabaseProxy(Db)); }
        }

        #endregion

        #region Methods

        #region For test

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /*
        [WebMethod]
        public Person EchoPerson(Person person)
        {
            return person;
        }*/

        #endregion

        #region Database

        #region  增删改查

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">SqlParameterDictionary对象的序列化字符串</param>
        /// <returns></returns>
        [WebMethod]
        public DataSet Select(string sql, string parameters)
        {
            SqlParameterDictionary par = parameters == null ? null : SerializationHelper.XmlDeserializeFromString<SqlParameterDictionary>(parameters);
            return Db.Select(Cs, sql, par);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>对象的序列化结果</returns>
        [WebMethod]
        public object Scalar(string sql, string parameters)
        {
            SqlParameterDictionary par = parameters == null ? null : SerializationHelper.XmlDeserializeFromString<SqlParameterDictionary>(parameters);
            return Db.Scalar(Cs, sql, par);
            //return obj == null ? null : SerializationHelper.XmlSerializeToString(obj);
        }

        /// <summary>
        /// 非查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [WebMethod]
        public int NonQuery(string sql, string parameters)
        {
            SqlParameterDictionary par = parameters == null ? null : SerializationHelper.XmlDeserializeFromString<SqlParameterDictionary>(parameters);
            return Db.NonQuery(Cs, sql, par);
        }

        #endregion

        #region Object oriented using serialization

        /// <summary>
        /// 获得某个类型(IIdObject的所有Id
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetIds(string typeName)
        {
            Type type = Type.GetType(typeName);
            return Dbp.GetIds(Cs, type).ToArray();
        }

        /// <summary>
        /// 获得某个列的数据
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetColumnData(string typeName, string columnName)
        {
            Type type = Type.GetType(typeName);
            return Dbp.GetColumnData(Cs, type, columnName).ToArray();
        }

        /// <summary>
        /// 判断某个Id的指定类型对象是否存在
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Exists(string typeName, string id)
        {
            Type type = Type.GetType(typeName);
            return Dbp.Exists(Cs, type, id);
        }

        /// <summary>
        /// 根据Id载入指定IIdObject类型的对象
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <param name="id">类型Id</param>
        /// <returns>对象的Xml序列化字符串</returns>
        [WebMethod]
        public string Load(string typeName, string id)
        {
            Type type = Type.GetType(typeName);
            //Type type = Type.GetType(typeName);
            object obj = Dbp.Select(Cs, type, id);
            return obj == null ? null : SerializationHelper.XmlSerializeToString(obj);
        }

        /// <summary>
        /// 删除指定类型指定标识的对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Delete(string typeName, string id)
        {
            Type type = Type.GetType(typeName);
            return Dbp.Delete(Cs, type, id);
        }

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="typeName">对象类型</param>
        /// <param name="objContent">对象内容的序列化结果</param>
        /// <returns></returns>
        [WebMethod]
        public bool Insert(string typeName, string strObj)
        {
            Type type = Type.GetType(typeName);
            IIdObject obj = (IIdObject)SerializationHelper.XmlDeserializeFromString(type, strObj);
            return Dbp.Insert(Cs, obj);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="objContent"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Update(string typeName, string strObj)
        {
            Type type = Type.GetType(typeName);
            IIdObject obj = (IIdObject)SerializationHelper.XmlDeserializeFromString(type, strObj);
            return Dbp.Update(Cs, obj);
        }

        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="objContent"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Save(string typeName, string strObj)
        {
            Type type = Type.GetType(typeName);
            IIdObject obj = (IIdObject)SerializationHelper.XmlDeserializeFromString(type, strObj);
            return Dbp.Save(Cs, obj);
        }

        #endregion

        #endregion

        #region File system

        const string ROOT_PATH = @"App_Data\FileSystem";
        private string GetAbsolutePath(string path)
        {
            if (Path.IsPathRooted(path))
                return path;
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ROOT_PATH, path);
        }

        //  是否存在文件
        [WebMethod]
        public bool ExistsFile(string path)
        {
            return File.Exists(GetAbsolutePath(path));
        }

        //  是否存在目录
        [WebMethod]
        public bool ExistsDirectory(string path)
        {
            return Directory.Exists(GetAbsolutePath(path));
        }

        [WebMethod]
        public void DeleteDirectory(string path)
        {
            Directory.Delete(path);
        }

        [WebMethod]
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        [WebMethod]
        public void RenameDirectory(string path, string newPath)
        {
            Directory.Move(path, newPath);
        }

        [WebMethod]
        public void RenameFile(string path, string newPath)
        {
            File.Move(path, newPath);
        }

        //  获得文件信息
        [WebMethod]
        public string GetFile(string path)
        {
            return GetAbsolutePath(path);
        }

        //  获得目录信息
        [WebMethod]
        public string GetDirectory(string path)
        {
            return GetAbsolutePath(path);
        }

        //  获得子目录信息
        [WebMethod]
        public string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(GetAbsolutePath(path));
        }

        //  获得子文件信息
        [WebMethod]
        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(GetAbsolutePath(path));
        }

        //  获得子文件信息
        [WebMethod]
        public string[] GetFilessWithPattern(string path, string searchPattern)
        {
            return Directory.GetFiles(GetAbsolutePath(path), searchPattern);
        }

        //  下载文件
        [WebMethod]
        public byte[] DownloadFile(string path)
        {
            byte[] buffer = null;
            //if (File.Exists(GetAbsolutePath(path)))
            //{
            using (FileStream fs = new FileStream(GetAbsolutePath(path), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                fs.Close();
            }
            // }
            return buffer;
        }

        //  上传文件
        [WebMethod]
        public void UploadFile(string path, byte[] bsFile)
        {
            FileStream fs = new FileStream(GetAbsolutePath(path), FileMode.Create, FileAccess.Write);
            fs.Write(bsFile, 0, bsFile.Length);
            fs.Close();
            fs.Dispose();
        }

        //  获得文件校验码
        [WebMethod]
        public byte[] GetFileMD5(string path)
        {
            using (MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider())
            using (FileStream fs = new FileStream(GetAbsolutePath(path), FileMode.Open))
                return hashProvider.ComputeHash(fs);
        }

        #endregion

        #endregion
    }
}