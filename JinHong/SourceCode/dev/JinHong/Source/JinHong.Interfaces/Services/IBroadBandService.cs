using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IBroadBandService : IApplicationService
    {

        bool CreateBroadBandFee(BroadBandFee broadBandFee);
        bool UpdateBroadBandFee(BroadBandFee broadBandFee);

        bool DelBroadBandFee(string BroadBandFeeId);

        bool HasBroadBandFee(string BroadBandFeeId, string BroadBandFeeName);

        DataTable GetBroadBandFeeByName(string companyName);

        DataTable GetAllBroadBandFees();
    }
}
