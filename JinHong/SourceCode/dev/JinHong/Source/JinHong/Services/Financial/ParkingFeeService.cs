using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    /// <summary>
    /// 停车费用信息
    /// </summary>
    public class ParkingFeeService : ApplicationService, IParkingFeeService
    {


        public bool CreateParkingFee(ParkingFeesInfo parkingFee)
        {
            throw new NotImplementedException();
        }

        public bool UpdateParkingFee(ParkingFeesInfo parkingFee)
        {
            throw new NotImplementedException();
        }

        public bool DelParkingFee(string parkingFeeId)
        {
            throw new NotImplementedException();
        }

        public DataTable GetParkingFeeDetail(string whereStr)
        {
            throw new NotImplementedException();
        }

        public DataTable LoadParkingFee(string whereStr)
        {
            throw new NotImplementedException();
        }
    }
}
