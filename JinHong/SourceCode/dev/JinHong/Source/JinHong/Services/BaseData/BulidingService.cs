using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using JinHong.ServiceContract;
using JinHong.Model;

namespace JinHong.Services
{
    public class BuildingService : ApplicationService, IBuildingService
    {

        private string resultSql = string.Empty;
        private const string baseSqlStr = "SELECT * FROM BuildingInfo";

        public bool CreateBuilding(BuildingInfo building)
        {
            return ServiceInstance.Insert(building);
        }

        public bool UpdateBuilding(BuildingInfo building)
        {
            return ServiceInstance.Update(building);
        }

        public bool DelBuilding(string buildingId)
        {
            return ServiceInstance.Delete<BuildingInfo>(buildingId);
        }

        public bool HasBuilding(string buildingId, string buildingName)
        {
            resultSql = string.Empty;
            if (string.IsNullOrEmpty(buildingId))
            {
                resultSql = string.Format(baseSqlStr + " where  name='{0}'", buildingName);
            }
            else
            {
                resultSql = string.Format(baseSqlStr + " where   id!='{0}' and  name='{1}'", buildingId, buildingName);
            }

            var ds = ServiceInstance.Select(resultSql);
            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;

        }


        public DataTable GetBuildingByName(string name)
        {
            string selectById = baseSqlStr + " where Name like '%{0}%'";
            resultSql = string.Format(selectById, name);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }

        public DataTable GetAllBuildings()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }
    }
}
