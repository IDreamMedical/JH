
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IParkingLotRentalService : IApplicationService
    {

        bool CreateParkingLotRental(ParkingLotRentalInfo parkingLotRental);
        bool UpdateParkingLotRental(ParkingLotRentalInfo parkingLotRental);

        bool DelParkingLotRental(string  parkingLotRentalId);



        DataTable GetParkingRentalByWhereStr(string whereStr);
        DataTable GetAllParkingRentals();
    }
}
