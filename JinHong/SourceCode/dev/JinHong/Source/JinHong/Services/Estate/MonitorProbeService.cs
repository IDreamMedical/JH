using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class MonitorProbeService : ApplicationService, IMonitorProbeService
    {

        private string resultSql = string.Empty;

        private const string baseSqlStr = "SELECT * FROM MonitorProbe";

        public bool CreateMonitorProbe(MonitorProbe monitorProbe)
        {
            return ServiceInstance.Insert(monitorProbe);

        }

        public bool UpdateMonitorProbe(MonitorProbe monitorProbe)
        {
            return ServiceInstance.Update(monitorProbe);
        }

        public bool DelMonitorProbe(string monitorProbeId)
        {
            return ServiceInstance.Delete<MonitorProbe>(monitorProbeId);
        }

        public bool HasMonitorProbe(string monitorProbeId, string monitorProbeName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetMonitorProbeByName(string probeName)
        {
            string selectById = baseSqlStr + "  where  name like '%{0}%'";
            resultSql = string.Format(selectById, probeName);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public DataTable GetAllMonitorProbes()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

    }
}
