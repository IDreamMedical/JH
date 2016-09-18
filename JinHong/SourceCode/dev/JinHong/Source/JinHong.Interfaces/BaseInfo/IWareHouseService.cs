using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IWareHouseService : IApplicationService
    {

        bool CreateWareHouse(WareHouseInfo wareHouse);
        bool UpdateWareHouse(WareHouseInfo wareHouse);

        bool DelWareHouse(string wareHouseId);
        bool HasWareHouse(string wareHouseId, string name);

        DataTable GetWareHouseById(string wareHouseName);

        DataTable GetAllWareHouses();



    }
}
