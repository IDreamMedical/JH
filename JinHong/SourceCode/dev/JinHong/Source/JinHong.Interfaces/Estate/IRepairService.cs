using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract.Estate
{
    public interface IRepairService : IApplicationService
    {

        bool CreateRepairRecord(RepairRecord repair);
        bool UpdateRepairRecord(RepairRecord repair);

        bool DelRepairRecord(string repairId);

        bool HasRepairRecord(string repairId, string repairName);

        DataTable GetRepairRecordByName(string companyName);

        DataTable GetAllRepairRecords();

    }
}
