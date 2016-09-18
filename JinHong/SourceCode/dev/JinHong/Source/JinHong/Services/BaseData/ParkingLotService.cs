using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class ParkingLotService : ApplicationService, IParkingLotService
    {

        private string resultSql = string.Empty;
        private const string baseSqlStr = "SELECT * FROM ParkingLotInfo";

        public bool CreateParkingLot(ParkingLotInfo parking)
        {
            return ServiceInstance.Insert(parking);
        }

        public bool UpdateParkingLot(ParkingLotInfo parking)
        {
            return ServiceInstance.Update(parking);
        }

        public bool DelParkingLot(string  parkingId)
        {
            return ServiceInstance.Delete<ParkingLotInfo>(parkingId);
        }

        public bool HasParking(string parkingId, string parkingName) 
        {
            resultSql = string.Empty;
            if (string.IsNullOrEmpty(parkingId))
            {
                resultSql = string.Format(baseSqlStr + " where  name='{0}'", parkingName);
            }
            else
            {
                resultSql = string.Format(baseSqlStr + " where   id!='{0}' and  name='{1}'", parkingId, parkingName);
            }

            var ds = ServiceInstance.Select(resultSql);
            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        
        }

        public DataTable GetParkingLotByName(string name)
        {
            string selectById = baseSqlStr + " where Name like '%{0}%'";
            resultSql = string.Format(selectById, name);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public DataTable GetAllParkingLots()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }
    }
}
