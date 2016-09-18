using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IDepositFeeService : IApplicationService
    {


        bool CreateDepositFee(DepositFeeInfo depositFee);
        bool UpdateDepositFee(DepositFeeInfo depositFee);

        bool DelDepositFee(string depositFeeId);

        bool HasDepositFee(string depositFeeId);


        DataTable GetDepositFeeByName(string companyName);

        /// <summary>
        /// 获取一段时间内已经交纳押金
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        DataTable GetDepositFee(DateTime startDate, DateTime endDate, string companyName);
        DataTable GetAllDepositFees();
    }
}
