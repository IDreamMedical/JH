using JinHong.ServiceContract;
using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class BroadBandService : ApplicationService, IBroadBandService
    {
        public bool CreateBroadBandFee(BroadBandFee broadBandFee)
        {
            return ServiceInstance.Insert(broadBandFee);
        }

        public bool UpdateBroadBandFee(BroadBandFee broadBandFee)
        {
            return ServiceInstance.Update(broadBandFee);
        }

        public bool DelBroadBandFee(string broadBandFeeId)
        {
            return ServiceInstance.Delete<BroadBandFee>(broadBandFeeId);
        }

        public bool HasBroadBandFee(string broadBandFeeId, string broadBandFeeName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetBroadBandFeeByName(string companyName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllBroadBandFees()
        {
            throw new NotImplementedException();
        }
    }
}
