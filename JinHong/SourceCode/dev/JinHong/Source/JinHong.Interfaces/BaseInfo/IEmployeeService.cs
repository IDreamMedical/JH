using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IEmployeeService : IApplicationService
    {

        bool CreateEmployee(EmployeeInfo employee);
        bool UpdateEmployee(EmployeeInfo employee);

        bool DelEmployee(string employeeId);

        bool HasEmployee(string employeeId, string employeeName);

        DataTable GetEmployeeByName(string name);

        DataTable GetAllEmployees();
    }
}
