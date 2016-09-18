using System;
using UniGuy.Core;
using UniGuy.Core.Data;
using UniGuy.Entity;
namespace JinHong.Model
{
    /// <summary>
    /// UnLeaseDetail:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class UnLeaseDetail : EntityBase, IIdObject
    {
        public UnLeaseDetail()
        { }
        #region Model
        private string _id;
        private string _roomid;
        private DateTime? _terminatedate;
        private int _isexpired;
        private string _powerstr;
        private string _waterstr;
        private string _roomstr;
        private string _keystr;
        private string _airstr;
        private string _airctrlstr;
        private string _parkingcardstr;
        private string _telstr;
        private string _invoicestr;
        private string _socialunitid;
        private string _notes;
        private string _unleasename;
        private string _unleasesurename;
        private string _wyname;
        private string _wysurename;
        private string _zsname;
        private string _zssurename;
        private DateTime? _unleasedate;
        private DateTime? _wydate;
        private DateTime? _zsdate;
        private DateTime? _rentaltimetodate;
        private DateTime? _parkingfeetimetodate;
        private DateTime? _wytimetodate;
        private string _contractno;
        /// <summary>
        /// 
        /// </summary>
        [PrimaryKey]
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
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string RoomId
        {
            set
            {
                if (_roomid != value)
                {
                    _roomid = value;
                    OnPropertyChanged("RoomId");
                }
            }
            get { return _roomid; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime? TerminateDate
        {
            set
            {
                if (_terminatedate != value)
                {
                    _terminatedate = value;
                    OnPropertyChanged("TerminateDate");
                }
            }
            get { return _terminatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public int IsExpired
        {
            set
            {
                if (_isexpired != value)
                {
                    _isexpired = value;
                    OnPropertyChanged("IsExpired");
                }
            }
            get { return _isexpired; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// 
         [Column]
        public string PowerStr
        {
            set
            {
                if (_powerstr != value)
                {
                    _powerstr = value;
                    OnPropertyChanged("PowerStr");
                }
            }
            get { return _powerstr; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string WaterStr
        {
            set
            {
                if (_waterstr != value)
                {
                    _waterstr = value;
                    OnPropertyChanged("WaterStr");
                }
            }
            get { return _waterstr; }
        }
        /// <summary>
        /// 
        /// </summary>
         [Column]
        public string RoomStr
        {
            set
            {
                if (_roomstr != value)
                {
                    _roomstr = value;
                    OnPropertyChanged("RoomStr");
                }
            }
            get { return _roomstr; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string KeyStr
        {
            set
            {
                if (_keystr != value)
                {
                    _keystr = value;
                    OnPropertyChanged("KeyStr");
                }
            }
            get { return _keystr; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string AirStr
        {
            set
            {
                if (_airstr != value)
                {
                    _airstr = value;
                    OnPropertyChanged("AirStr");
                }
            }
            get { return _airstr; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string AirCtrlStr
        {
            set
            {
                if (_airctrlstr != value)
                {
                    _airctrlstr = value;
                    OnPropertyChanged("AirCtrlStr");
                }
            }
            get { return _airctrlstr; }
        }
        /// <summary>
        /// 
        /// </summary>
         [Column]
        public string ParkingCardStr
        {
            set
            {
                if (_parkingcardstr != value)
                {
                    _parkingcardstr = value;
                    OnPropertyChanged("ParkingCardStr");
                }
            }
            get { return _parkingcardstr; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string TelStr
        {
            set
            {
                if (_telstr != value)
                {
                    _telstr = value;
                    OnPropertyChanged("TelStr");
                }
            }
            get { return _telstr; }
        }
        /// <summary>
        /// 
        /// </summary>
         [Column]
        public string InvoiceStr
        {
            set
            {
                if (_invoicestr != value)
                {
                    _invoicestr = value;
                    OnPropertyChanged("InvoiceStr");
                }
            }
            get { return _invoicestr; }
        }
        /// <summary>
        /// 
        /// </summary>
         [Column]
        public string SocialUnitId
        {
            set
            {
                if (_socialunitid != value)
                {
                    _socialunitid = value;
                    OnPropertyChanged("SocialUnitId");
                }
            }
            get { return _socialunitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Notes
        {
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged("Notes");
                }
            }
            get { return _notes; }
        }
        /// <summary>
        /// 
        /// </summary>
         [Column]
        public string UnLeaseName
        {
            set
            {
                if (_unleasename != value)
                {
                    _unleasename = value;
                    OnPropertyChanged("UnLeaseName");
                }
            }
            get { return _unleasename; }
        }
        /// <summary>
        /// 
        /// </summary>
         [Column]
        public string UnLeaseSureName
        {
            set
            {
                if (_unleasesurename != value)
                {
                    _unleasesurename = value;
                    OnPropertyChanged("UnLeaseSureName");
                }
            }
            get { return _unleasesurename; }
        }
        /// <summary>
        /// 
        /// </summary>
       [Column]
        public string WYName
        {
            set
            {
                if (_wyname != value)
                {
                    _wyname = value;
                    OnPropertyChanged("WYName");
                }
            }
            get { return _wyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string WYSureName
        {
            set
            {
                if (_wysurename != value)
                {
                    _wysurename = value;
                    OnPropertyChanged("WYSureName");
                }
            }
            get { return _wysurename; }
        }
        /// <summary>
        /// 
        /// </summary>

        [Column]
        public string ZSName
        {
            set
            {
                if (_zsname != value)
                {
                    _zsname = value;
                    OnPropertyChanged("ZSName");
                }
            }
            get { return _zsname; }
        }
        /// <summary>
        /// 
        /// </summary>
         [Column]
        public string ZSSureName
        {
            set
            {
                if (_zssurename != value)
                {
                    _zssurename = value;
                    OnPropertyChanged("ZSSureName");
                }
            }
            get { return _zssurename; }
        }
        /// <summary>
        /// 
        /// </summary>
         [Column]
        public DateTime? UnLeaseDate
        {
            set
            {
                if (_unleasedate != value)
                {
                    _unleasedate = value;
                    OnPropertyChanged("UnLeaseDate");
                }
            }
            get { return _unleasedate; }
        }
        /// <summary>
        /// 
        /// </summary>
         [Column]
        public DateTime? WYDate
        {
            set
            {
                if (_wydate != value)
                {
                    _wydate = value;
                    OnPropertyChanged("WYDate");
                }
            }
            get { return _wydate; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime? ZSDate
        {
            set
            {
                if (_zsdate != value)
                {
                    _zsdate = value;
                    OnPropertyChanged("ZSDate");
                }
            }
            get { return _zsdate; }
        }
        /// <summary>
        /// 
      
        /// </summary>
        [Column]
        public DateTime? RentalTimeToDate
        {
            set
            {
                if (_rentaltimetodate != value)
                {
                    _rentaltimetodate = value;
                    OnPropertyChanged("RentalTimeToDate");
                }
            }
            get { return _rentaltimetodate; }
        }
        /// <summary>
        /// 
        /// </summary>
         [Column]
        public DateTime? ParkingFeeTimeToDate
        {
            set
            {
                if (_parkingfeetimetodate != value)
                {
                    _parkingfeetimetodate = value;
                    OnPropertyChanged("ParkingFeeTimeToDate");
                }
            }
            get { return _parkingfeetimetodate; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime? WYTimeToDate
        {
            set
            {
                if (_wytimetodate != value)
                {
                    _wytimetodate = value;
                    OnPropertyChanged("WYTimeToDate");
                }
            }
            get { return _wytimetodate; }
        }
        /// <summary>
        /// 
        /// </summary>

        [Column]
        public string ContractNo
        {
            set
            {
                if (_contractno != value)
                {
                    _contractno = value;
                    OnPropertyChanged("ContractNo");
                }
            }
            get { return _contractno; }
        }
        #endregion Model

    }
}

