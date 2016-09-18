using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IFeeItemService : IApplicationService
    {

        bool CreateFeeItem(FeeItem feeItem);
        bool UpdateFeeItem(FeeItem feeItem);

        bool DelFeeItem(string feeItemId);

        bool HasFeeItem(string feeItemId, string feeItemName);
        DataTable GetFeeItemByName(string name);

        DataTable GetAllFeeItems();

    }
}
