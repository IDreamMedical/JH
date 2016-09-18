
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JinHong.ServiceContract;
using UniGuy.Entity;

namespace JinHong.Services
{
    public class RoleService : ApplicationService, IRoleService
    {

        private string resultSql = string.Empty;
        private const string SelectById = "SELECT * FROM Role";

        public bool CreateRole(Role role)
        {
            return ServiceInstance.Insert(role);


        }

        public bool UpdateRole(Role role)
        {
            return ServiceInstance.Update(role);
        }

        public bool DelRole(string roleId)
        {
            return ServiceInstance.Delete<Role>(roleId);
        }


        public DataTable GetRoleByName(string name)
        {
            string selectById = SelectById + " where Name like '%{0}%'";
            resultSql = string.Format(selectById, name);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }

        public DataTable GetAllRoles()
        {
            var ds = ServiceInstance.Select(SelectById, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }


        public bool HasRole(string roleId, string name)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(roleId))
            {
                sql = string.Format(SelectById + " where  name='{0}'", name);
            }
            else
            {
                sql = string.Format(SelectById + " where  id!='{0}' and  name='{1}'", roleId, name);
            }

            var ds = ServiceInstance.Select(sql);
            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }
    }
}
