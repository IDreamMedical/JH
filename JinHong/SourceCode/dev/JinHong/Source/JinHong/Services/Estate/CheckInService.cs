using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class CheckInService : ApplicationService, ICheckInService
    {
        private string resultSql = string.Empty;

        private const string baseSqlStr = "SELECT * FROM CheckIn";

        public bool CreateCheckIn(CheckIn checkIn)
        {
            return ServiceInstance.Insert(checkIn);
        }

        public bool UpdateCheckIn(CheckIn checkIn)
        {
            return ServiceInstance.Update(checkIn);
        }

        public bool DelCheckIn(string checkInId)
        {
            return ServiceInstance.Delete<CheckIn>(checkInId);
        }

        public bool HasCheckIn(string CheckInId, string CheckInName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetCheckInByName(string companyName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllCheckIns()
        {
            throw new NotImplementedException();
        }
    }
}
