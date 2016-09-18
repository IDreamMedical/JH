using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.ServiceContract;
using JinHong.Model;
using System.Data;


namespace JinHong.Services
{
    public class ParkingLotRentalService : ApplicationService, IParkingLotRentalService
    {

        private string resultSql = string.Empty;
        private const string baseSqlStr = @"SELECT  a.*, b.name as socialUnitName,c.name as ParkingLotName
                                            FROM ParkingLotRentalInfo a 
                                            join socialUnit b  on a.socialUnit = b.Id
                                            join ParkingLot c on a.ParkingLotId= c.id ";

        public DataTable GetParkingRentalByWhereStr(string whereStr)
        {
            string selectById = baseSqlStr + "  where b.name like '%{0}%'or  c.name like'%{1}%' or a.rentalName like '%{2}%'";
            resultSql = string.Format(selectById, whereStr, whereStr, whereStr);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public DataTable GetAllParkingRentals()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }




        public bool CreateParkingLotRental(ParkingLotRentalInfo parkingLotRental)
        {
            return ServiceInstance.Insert(parkingLotRental);
        }

        public bool UpdateParkingLotRental(ParkingLotRentalInfo parkingLotRental)
        {
            return ServiceInstance.Update(parkingLotRental);
        }

        public bool DelParkingLotRental(string parkingLotRentalId)
        {
            return ServiceInstance.Delete<ParkingLotRentalInfo>(parkingLotRentalId);
        }
    }
}
