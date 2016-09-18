using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IInstallAirService : IApplicationService
    {
        bool CreateInstallAirRecord(InstallAirRecord installAirRecord);
        bool UpdateInstallAirRecord(InstallAirRecord installAirRecord);

        bool DelInstallAirRecord(string installAirRecordId);

        bool HasInstallAirRecord(string installAirRecordId, string installAirRecordName);

        DataTable GetInstallAirRecordByName(string companyName);

        DataTable GetAllInstallAirRecords();
    }
}
