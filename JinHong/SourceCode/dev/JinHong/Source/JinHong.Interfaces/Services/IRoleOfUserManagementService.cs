using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IRoleOfUserManagementService:IApplicationService
    {

        void CreateRoleOfUser();

        void DelRoleOfUser();

        DataTable GetRoleOfUserByName(string name);

        DataTable GetAllRoleOfUsers();
    }
}
