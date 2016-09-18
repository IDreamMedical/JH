using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IParkingLotService : IApplicationService
    {

        bool CreateParkingLot(ParkingLotInfo parking);
        bool UpdateParkingLot(ParkingLotInfo parking);

        bool DelParkingLot(string  parkingId);
        bool HasParking(string parkingId, string parkingName);


        DataTable GetParkingLotByName(string name);

        DataTable GetAllParkingLots();
    }
}
