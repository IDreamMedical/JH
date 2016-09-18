using JinHong.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.Model;
using System.Data;

namespace JinHong.Services
{
    public class DepositFeeService : ApplicationService, IDepositFeeService
    {
        public bool CreateDepositFee(DepositFeeInfo depositFee)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDepositFee(DepositFeeInfo depositFee)
        {
            throw new NotImplementedException();
        }

        public bool DelDepositFee(string depositFeeId)
        {
            throw new NotImplementedException();
        }

        public bool HasDepositFee(string depositFeeId)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDepositFeeByName(string companyName)
        {
            var mStartDate = DateTime.Now.AddDays(-14);
            var mEndDate = DateTime.Now;
            return GetDepositFee(mStartDate, mEndDate, companyName);
        }

        public DataTable GetDepositFee(DateTime startDate, DateTime endDate, string companyName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllDepositFees()
        {
            throw new NotImplementedException();
        }
    }
}
