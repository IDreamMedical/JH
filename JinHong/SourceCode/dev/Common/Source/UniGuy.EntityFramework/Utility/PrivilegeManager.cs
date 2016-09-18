using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Entity;
using UniGuy.Core.Extensions;

namespace UniGuy.Utility
{
    public static class PrivilegeManager
    {
        /// <summary>
        /// 判断某个角色是否拥有某项权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="privilegeName"></param>
        /// <returns></returns>
        public static bool HasPrivilege(Role role, string privilegeName)
        {
            if (role == null)
                throw new ArgumentNullException("role");
            return !role.Privileges.FirstOrDefault(p => p.Name == privilegeName).IsDefault();
        }

        /// <summary>
        /// 判断某个用户是否拥有某项权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="privilegeName"></param>
        /// <returns></returns>
        public static bool HasPrivilege(User user, string privilegeName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return !user.Roles.FirstOrDefault(r => HasPrivilege(r, privilegeName)).IsDefault();
        }
    }
}
