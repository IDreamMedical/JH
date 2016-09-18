using JinHong.Core;
using JinHong.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JinHong.Services
{
    public class ModuleService : ApplicationService, IModuleService
    {
        private string resultSql = string.Empty;
        private const string baseSqlStr = "SELECT * FROM Module";

        public bool CreateModule(Module module)
        {
            return ServiceInstance.Insert(module);
        }

        public bool UpdateModule(Module module)
        {
            return ServiceInstance.Update(module);
        }

        public bool DelModule(string moduleId)
        {
            return ServiceInstance.Delete<Module>(moduleId);
        }

        public DataTable GetModuleByName(string moduleName)
        {
            string selectById = baseSqlStr + " where Name like '%{0}%'";
            resultSql = string.Format(selectById, moduleName);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public DataTable GetAllModules()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }

        public bool HasModule(string moduleId, string moduleName)
        {
            resultSql = string.Empty;
            if (string.IsNullOrEmpty(moduleId))
            {
                resultSql = string.Format(baseSqlStr + " where  name='{0}'", moduleName);
            }
            else
            {
                resultSql = string.Format(baseSqlStr + " where   id!='{0}' and  name='{1}'", moduleId, moduleName);
            }
            var ds = ServiceInstance.Select(resultSql);
            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }

        public DataTable GetModules(string roleId)
        {
            string mSql = string.Format(@"select m.Id ,m.Name, m.PId,m.IsSys,m.Status,m.Description from Module  m
                    join ModuleMapToRole m2r on m.Id= m2r.ModuleId
                    where m2r.RoleId='{0}'", roleId);
            var ds = ServiceInstance.Select(mSql);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }
    }
}
