using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 租赁业务导航
    /// </summary>
    public class RentalBizNavViewModel
    {

        /// 1. 新来的。 可以租单元和仓库；
        /// 2. 已经有了，退租单元，续租， 退租仓库
        #region var

        //
        private ICommand _rentalRoomCommand;

        private ICommand _reverseRoomCommand;

        private ICommand _rentalWareHouseCommand;

        private ICommand _reverseWareHouseCommand;

        /// <summary>
        /// 续租
        /// </summary>
        private ICommand _reletCommand;

        #endregion

        public RentalBizNavViewModel()
        {

        }
    }
}
