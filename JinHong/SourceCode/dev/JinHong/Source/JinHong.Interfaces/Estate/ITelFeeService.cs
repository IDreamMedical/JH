using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface ITelFeeService : IApplicationService
    {


        DataTable GetTelFeeByName(string name);

        DataTable GetAllTelFees();

    }
}
