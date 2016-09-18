using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.ServiceContract;
using System.Data;

namespace JinHong.Services
{
    public class TelFeeService : ApplicationService, ITelFeeService
    {

        private string resultSql = string.Empty;
        private const string SelectById = @"select a.Name, a.Id  
                                            as SocialUnitId, b.* 
                                            from  SocialUnitInfo  a
                                            left join  TelecomFeesInfo b  on a.Id=b.SocialUnitId ";

        public DataTable GetTelFeeByName(string name)
        {
            string selectById = SelectById + " where Name like '%{0}%'";
            resultSql = string.Format(selectById, name);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }

        public DataTable GetAllTelFees()
        {
            var ds = ServiceInstance.Select(SelectById, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

    }
}
