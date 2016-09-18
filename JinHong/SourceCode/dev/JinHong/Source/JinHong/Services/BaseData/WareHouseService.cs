using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class WareHouseService : ApplicationService, IWareHouseService
    {

        private string resultSql = string.Empty;
        private const string SelectById = "SELECT * FROM WareHouseInfo";

        public DataTable GetWareHouseById(string wareHouseName)
        {
            string selectById = "SELECT * FROM WareHouseInfo where Name like '%{0}%'";
            resultSql = string.Format(selectById, wareHouseName);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public DataTable GetAllWareHouses()
        {
            var ds = ServiceInstance.Select(SelectById, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public bool HasWareHouse(string id, string name)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                sql = string.Format(SelectById + " where  name='{0}'", name);
            }
            else
            {
                sql = string.Format(SelectById + " where  id!='{0}' and  name='{1}'", id, name);
            }

            var ds = ServiceInstance.Select(sql);
            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;

        }


        public bool CreateWareHouse(WareHouseInfo wareHouse)
        {
            return ServiceInstance.Save(wareHouse);
        }

        public bool UpdateWareHouse(Model.WareHouseInfo wareHouse)
        {
            throw new NotImplementedException();
        }

        public bool DelWareHouse(string wareHouseId)
        {
            throw new NotImplementedException();
        }
    }
}
