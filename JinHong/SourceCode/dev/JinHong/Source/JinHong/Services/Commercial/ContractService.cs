using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class ContractService : ApplicationService, IContractService
    {
        private string resultSql = string.Empty;

        private const string baseSqlStr = "SELECT * FROM ContractInfo";

        public bool CreateContract(ContractInfo contract)
        {
            return ServiceInstance.Insert(contract);
        }

        public bool UpdateContract(ContractInfo contract)
        {
            return ServiceInstance.Update(contract);

        }

        public bool DelContract(string contractId)
        {
            return ServiceInstance.Delete<ContractInfo>(contractId);

        }

        public bool HasContract(string socialUnitId)
        {
            var resultSql = baseSqlStr + " where SocialUnitId='{0}'";
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }

        public DataTable GetContractByWhereStr(string whereStr)
        {
            string selectById = baseSqlStr + "  where Name like '%{0}%' or ContractNo like '%{1}%' ";
            resultSql = string.Format(selectById, whereStr, whereStr);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }


        public DataTable GetAllContracts()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }







    }
}
