using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using UniGuy.Core.Data;
using UniGuy.Core.Helpers;

namespace JinHong.WebService
{
    /// <summary>
    /// BusinessService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class BusinessService : System.Web.Services.WebService
    {
        private ServiceMain _DBService = null;

        public ServiceMain DBService
        {
            get { return _DBService ?? (_DBService = new ServiceMain()); }
        }

        /// <summary>
        /// 获取楼号信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetBuildingInfo()
        {
            string sql = string.Empty;
            string parameters = string.Empty;
            return DBService.Select(sql, parameters);
        }

        [WebMethod]
        public DataSet GetAllRoomInfo()
        {
            string sql = string.Empty;
            string parameters = string.Empty;
            return DBService.Select(sql, parameters);
        }

        [WebMethod]
        public DataSet GetRoomInfoByType(string typeId)
        {
            string sql = string.Empty;
            string parameters = string.Empty;
            return DBService.Select(sql, parameters);
        }

        [WebMethod]
        public bool Save(string typeName, string strObj)
        {
            string sql = string.Empty;
            string parameters = string.Empty;
            return true;
        }




    }
}
