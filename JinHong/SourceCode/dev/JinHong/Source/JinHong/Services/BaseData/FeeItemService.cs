using JinHong.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JinHong.Model;

namespace JinHong.Services
{
    public class FeeItemService : ApplicationService, IFeeItemService
    {
        private string resultSql = string.Empty;
        private const string SelectById = "SELECT * FROM FeeItem";
        public DataTable GetFeeItemByName(string name)
        {
            string selectById = "SELECT * FROM FeeItem where Name like '%{0}%'";
            resultSql = string.Format(selectById, name);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }

        public DataTable GetAllFeeItems()
        {
            var ds = ServiceInstance.Select(SelectById, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }


        public bool HasFeeItem(string id, string name)
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
        public bool CreateFeeItem(FeeItem feeItem)
        {
            return ServiceInstance.Insert(feeItem);
        }

        public bool UpdateFeeItem(FeeItem feeItem)
        {
            return ServiceInstance.Update(feeItem);
        }

        public bool DelFeeItem(string feeItemId)
        {
            return ServiceInstance.Delete<FeeItem>(feeItemId);
        }

    }
}
