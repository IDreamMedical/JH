using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IMonitorProbeService : IApplicationService
    {

        bool CreateMonitorProbe(MonitorProbe monitorProbe);
        bool UpdateMonitorProbe(MonitorProbe monitorProbe);

        bool DelMonitorProbe(string monitorProbeId);

        /// <summary>
        /// 是这幢楼层是否存在相同的探头
        /// </summary>
        /// <param name="monitorProbeId"></param>
        /// <param name="monitorProbeName"></param>
        /// <returns></returns>
        bool HasMonitorProbe(string monitorProbeId, string monitorProbeName);

        DataTable GetMonitorProbeByName(string companyName);

        DataTable GetAllMonitorProbes();
    }
}
