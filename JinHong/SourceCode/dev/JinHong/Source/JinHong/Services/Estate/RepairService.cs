using JinHong.ServiceContract.Estate;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class RepairService : ApplicationService, IRepairService
    {
        public bool CreateRepairRecord(RepairRecord repair)
        {
            return ServiceInstance.Insert(repair);
        }

        public bool UpdateRepairRecord(RepairRecord repair)
        {
            return ServiceInstance.Update(repair);
        }

        public bool DelRepairRecord(string repairId)
        {
            return ServiceInstance.Delete<RepairRecord>(repairId);
        }

        public bool HasRepairRecord(string repairId, string repairName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetRepairRecordByName(string companyName)
        {
            
            throw new NotImplementedException();
        }

        public DataTable GetAllRepairRecords()
        {
            throw new NotImplementedException();
        }
    }
}
