using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UniGuy.Entity;

namespace JinHong.ServiceContract
{
    public interface IRoleService : IApplicationService
    {

        bool CreateRole(Role role);
        bool UpdateRole(Role role);

        bool DelRole(string roleId);

        DataTable GetRoleByName(string name);

        DataTable GetAllRoles();
        bool HasRole(string roleId, string name);

    }
}
