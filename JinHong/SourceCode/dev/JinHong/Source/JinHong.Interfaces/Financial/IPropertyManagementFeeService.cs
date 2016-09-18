﻿using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    /// <summary>
    /// 物业费
    /// </summary>
    public interface IPropertyManagementFeeService : IApplicationService
    {


        bool CreatePropertyManagementFee(RentalFeesInfo rentalFee);

        bool UpdateRentalFee(RentalFeesInfo rentalFee);

        bool DelRentalFee(string rentalFeeId);

        bool HasRentalFee(string rentalFeeId);


        DataTable GetRentalFeeByName(string companyName);

        /// <summary>
        /// 获取一段时间内已经交纳押金
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        DataTable GetRentalFee(DateTime startDate, DateTime endDate, string companyName);

        DataTable GetAllRentalFees();



    }
}
