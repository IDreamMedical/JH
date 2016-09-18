using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Windows.ViewModels;

namespace JinHong.ViewModel
{
    public class WareHouseRentalViewModel : ViewModelBase
    {
        #region Field

        private string _id;

        private string _name;

        private DateTime _effectDate;

        private DateTime _expirateDate;

        /// <summary>
        /// 租赁户确认人
        /// </summary>
        private string _leasingName;


        private string _reason;


        private string _empName;

        private string _empId;
        private string _wareHouseNum;




        #endregion



        #region Property
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _id = value;
                    OnPropertyChanged("Name");
                }
            }
        }


        public DateTime EffectDate
        {
            get { return _effectDate; }
            set
            {
                if (_effectDate != value)
                {
                    _effectDate = value;
                    OnPropertyChanged("EffectDate");
                }
            }
        }

        public DateTime ExpirateDate
        {
            get { return _expirateDate; }
            set
            {
                if (_expirateDate != value)
                {
                    _effectDate = value;
                    OnPropertyChanged("ExpirateDate");
                }
            }
        }






        public string Reason
        {
            get { return _reason; }
            set
            {

                if (_reason != value)
                {
                    _reason = value;
                    OnPropertyChanged("Reason");
                }
            }
        }

        public string EmpName
        {
            get { return _empName; }
            set
            {

                if (_empName != value)
                {
                    _empName = value;
                    OnPropertyChanged("EmpName");
                }
            }
        }


        public string EmpId
        {
            get { return _empId; }
            set
            {

                if (_empId != value)
                {
                    _empId = value;
                    OnPropertyChanged("EmpId");
                }
            }
        }

        public string WareHouseNum
        {
            get { return _wareHouseNum; }
            set
            {

                if (_wareHouseNum != value)
                {
                    _wareHouseNum = value;
                    OnPropertyChanged("WareHouseNum");
                }
            }
        }


        public string LeasingName
        {
            get { return _leasingName; }
            set
            {
                if (_leasingName != value)
                {
                    _leasingName = value;
                    OnPropertyChanged("LeasingName");
                }
            }
        }



        #endregion



    }
}
