using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class GrantKeyService : ApplicationService, IGrantKeyService
    {

        private string resultSql = string.Empty;

        private const string baseSqlStr = "SELECT a. *, b.Name  FROM GrantKey a  join  socialUnit b on a.socialUnitId= b.Id   ";

        public bool CreateGrantKey(GrantKey grantKey)
        {
            return ServiceInstance.Insert(grantKey);
        }

        public bool UpdateGrantKey(GrantKey grantKey)
        {
            return ServiceInstance.Update(grantKey);
        }

        public bool DelGrantKey(string grantKeyId)
        {
            return ServiceInstance.Delete<GrantKey>(grantKeyId);
        }

        public bool HasGrantKey(string grantKeyId, string grantKeyName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetGrantKeyByName(string whereStrName)
        {
            string selectById = baseSqlStr + "  where Name like '%{0}%' Or ContractName like '%{1}%' ";
            resultSql = string.Format(selectById, whereStrName, whereStrName);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }


        public DataTable GetAllGrantKeys()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }
    }
}
