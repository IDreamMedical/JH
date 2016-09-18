using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IParkingFeeService : IApplicationService
    {


        /// <summary>
        /// 新增停车费
        /// </summary>
        /// <param name="parkingFee"></param>
        /// <returns></returns>
        bool CreateParkingFee(ParkingFeesInfo parkingFee);

        /// <summary>
        /// 编辑停车费
        /// </summary>
        /// <param name="parkingFee"></param>
        /// <returns></returns>
        bool UpdateParkingFee(ParkingFeesInfo parkingFee);

        /// <summary>
        /// 删除停车费
        /// </summary>
        /// <param name="parkingFeeId"></param>
        /// <returns></returns>
        bool DelParkingFee(string parkingFeeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        DataTable GetParkingFeeDetail(string whereStr);

        /// <summary>
        /// 加载停车费用信息
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        DataTable LoadParkingFee(string whereStr);
    }
}
