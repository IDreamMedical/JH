using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class SocialUnitService : ApplicationService, ISocialUnitService
    {

        private string resultSql = string.Empty;

        private const string baseSqlStr = "SELECT * FROM SocialUnitInfo";


        public DataTable GetSocialUnitById(string wareHouseName)
        {
            string selectById = "SELECT * FROM SocialUnitInfo where Name like '%{0}%'";
            resultSql = string.Format(selectById, wareHouseName);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public DataTable GetAllSocialUnits()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public bool HasContract(string socialUnitId)
        {


            string sql = string.Format("select *from  ContractInfo where SocialUnitId='{0}'AND   ExpirateDate>  date('now')", socialUnitId);
            var ds = ServiceInstance.Select(sql, null);
            var dt = ds == null ? null : ds.Tables[0];
            return dt != null && dt.Rows.Count > 0;

        }


        public bool CreateSocialUnit(SocialUnitInfo socialUnit)
        {
            return ServiceInstance.Insert(socialUnit);
        }

        public bool UpdateSocialUnit(SocialUnitInfo socialUnit)
        {
            return ServiceInstance.Update<SocialUnitInfo>(socialUnit);

        }

        public bool DelSocialUnit(string socialUnitId)
        {
            return ServiceInstance.Delete<SocialUnitInfo>(socialUnitId);
        }

        public bool HasSocialUnit(string socialUnitId, string socialUnitName)
        {
            resultSql = string.Empty;
            if (string.IsNullOrEmpty(socialUnitId))
            {
                resultSql = string.Format(baseSqlStr + " where   name='{0}'", socialUnitName);
            }
            else
            {
                resultSql = string.Format(baseSqlStr + " where  id!='{0}' and   name='{1}'", socialUnitId, socialUnitName);
            }

            var ds = ServiceInstance.Select(resultSql);
            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }


        public DataSet GetSocialUnitDetail(string companyId)
        {
            throw new NotImplementedException();
        }
    }
}
