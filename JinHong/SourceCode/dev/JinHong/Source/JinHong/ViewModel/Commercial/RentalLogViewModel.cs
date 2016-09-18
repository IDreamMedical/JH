using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 租赁日记记录表 包括租赁，退租，变更记录
    /// </summary>
    public class RentalLogViewModel : AbstractPageViewModel
    {
        #region Fields

        private DataTable leaseTbl;



        #endregion

        #region Properties

        public DataTable LeaseTbl1
        {
            get { return leaseTbl; }
            set
            {
                if (leaseTbl != value)
                {
                    leaseTbl = value;
                    OnPropertyChanged("LeaseTbl1");
                }
            }
        }


        #endregion

        #region Constructors

        public RentalLogViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
