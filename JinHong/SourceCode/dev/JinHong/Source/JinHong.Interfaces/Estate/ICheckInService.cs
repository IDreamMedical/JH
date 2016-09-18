using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface ICheckInService : IApplicationService
    {

        bool CreateCheckIn(CheckIn CheckIn);
        bool UpdateCheckIn(CheckIn CheckIn);

        bool DelCheckIn(string CheckInId);

        bool HasCheckIn(string CheckInId, string CheckInName);

        DataTable GetCheckInByName(string companyName);

        DataTable GetAllCheckIns();
    }
}
