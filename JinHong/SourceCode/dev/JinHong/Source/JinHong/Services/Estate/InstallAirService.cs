using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class InstallAirService : ApplicationService, IInstallAirService
    {

        private string resultSql = string.Empty;

        private const string baseSqlStr = @"SELECT a.*,  b.Name as SocialUnitName, c.Name as RoomName   FROM InstallAirRecord a 
                                            join  socialUnit b on a.socialUnitId= b.Id  
                                            join  RoomInfo c  on a.RoomId= c.Id   ";

        public bool CreateInstallAirRecord(InstallAirRecord installAirRecord)
        {
            return ServiceInstance.Insert(installAirRecord);
        }

        public bool UpdateInstallAirRecord(InstallAirRecord installAirRecord)
        {
            return ServiceInstance.Update(installAirRecord);
        }

        public bool DelInstallAirRecord(string installAirRecordId)
        {
            return ServiceInstance.Delete<InstallAirRecord>(installAirRecordId);
        }

        public bool HasInstallAirRecord(string installAirRecordId, string installAirRecordName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetInstallAirRecordByName(string whereStrName)
        {
            string selectById = baseSqlStr + "  where SocialUnitName like '%{0}%' Or RoomName like '%{1}%' ";
            resultSql = string.Format(selectById, whereStrName, whereStrName);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }


        public DataTable GetAllInstallAirRecords()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }
    }
}
