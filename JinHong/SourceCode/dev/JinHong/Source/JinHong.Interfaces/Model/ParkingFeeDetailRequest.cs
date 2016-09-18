using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinHong.Interfaces.Model
{
    public class ParkingFeeDetailRequest
    {
        #region private var 
        /// <summary>
        /// 车主
        /// </summary>
        private string _carOwnerName;

        /// <summary>
        /// 费用状态
        /// </summary>
        private string _parkingFeeStatus;

        /// <summary>
        /// 费用状态
        /// </summary>
        private DateTime _startDate;

        /// <summary>
        /// 结束日期
        /// </summary>
        private DateTime _endDate;

        #endregion
    }
}
