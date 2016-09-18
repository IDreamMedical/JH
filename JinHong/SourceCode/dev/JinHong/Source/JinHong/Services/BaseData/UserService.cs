using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.ServiceContract;
using System.Data;

namespace JinHong.Services
{
    public class UserService : ApplicationService, IUserService
    {

        private string resultSql = string.Empty;
        private const string SelectById = "SELECT * FROM User";

        public void CreateUser()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser()
        {
            throw new NotImplementedException();
        }

        public void DelUser()
        {
            throw new NotImplementedException();
        }

        public DataTable GetUserByName(string name)
        {
            string selectById = "SELECT * FROM User where Name like '%{0}%'";
            resultSql = string.Format(selectById, name);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }

        public DataTable GetAllUsers()
        {
            var ds = ServiceInstance.Select(SelectById, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }


        public bool HasUser(string id, string name)
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

        /// <summary>
        /// 根据用户id用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetUserInfo(string userId)
        {
            string mSql = string.Format(@"select r2u.UserId,r2u.RoleId, u.UserName,r.Name as RoleName, e.Id as                    EmployeeId,e.Name as EmployeeName from User u
                                JOIN  RoleMapToUser r2u on u.Id = r2u.UserId
                                JOIN Role r on r2u.RoleId = r.Id
                                JOIN EmployeeInfo e on e.Id = u.EmployeeId where r2u.UserId='{0}'", userId);
            var ds = ServiceInstance.Select(mSql);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }
    }
}
