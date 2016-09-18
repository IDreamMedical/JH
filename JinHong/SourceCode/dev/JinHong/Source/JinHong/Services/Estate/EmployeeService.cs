using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services.Estate
{
    public class EmployeeService : ApplicationService, IEmployeeService
    {

        private string resultSql = string.Empty;

        private const string baseSqlStr = "SELECT * FROM EmployeeInfo";

        public bool CreateEmployee(EmployeeInfo employee)
        {
            return ServiceInstance.Insert(employee);
        }

        public bool UpdateEmployee(EmployeeInfo employee)
        {
            return ServiceInstance.Update(employee);
        }

        public bool DelEmployee(string employeeId)
        {
            return ServiceInstance.Delete<EmployeeInfo>(employeeId);
        }

        public bool HasEmployee(string employeeId, string employeeName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetEmployeeByName(string name)
        {
            string selectById = baseSqlStr + "  where Name like '%{0}%' ";
            resultSql = string.Format(selectById, name);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public DataTable GetAllEmployees()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }
    }
}
