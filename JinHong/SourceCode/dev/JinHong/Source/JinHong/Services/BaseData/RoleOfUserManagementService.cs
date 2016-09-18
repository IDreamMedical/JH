using JinHong.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class RoleOfUserManagementService :ApplicationService, IRoleOfUserManagementService
    {
        private string resultSql = string.Empty;
        private const string SelectById = "SELECT a.Id,a.RoleId,b.Name as RoleName, a.UserId,c. UserName  "
                                          + " FROM RoleMapToUser a join Role b  on a.RoleId=b.Id "
                                          + "join User c  on a.UserId=c.Id";

        public void CreateRoleOfUser()
        {
            throw new NotImplementedException();
        }

        public void DelRoleOfUser()
        {
            throw new NotImplementedException();
        }

        public DataTable GetRoleOfUserByName(string name)
        {
            string selectById = SelectById + " where c.Name like '%{0}%'";
            resultSql = string.Format(selectById, name);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }

        public DataTable GetAllRoleOfUsers()
        {
            var ds = ServiceInstance.Select(SelectById, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }
    }
}
