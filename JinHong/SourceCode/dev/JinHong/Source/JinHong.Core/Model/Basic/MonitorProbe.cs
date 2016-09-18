using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Entity;
using UniGuy.Core;
using UniGuy.Core.Data;

namespace JinHong.Model
{
    /// <summary>
    /// 摄像头位置
    /// </summary>
    [Table("MonitorProbe")]
    public class MonitorProbe : EntityBase, IIdObject, IDescribable
    {
        #region Fields
        private string _id;
        private string _regionName;
        private string _description;
        private string _notes;
        private int _status;

        #endregion

        #region Properties

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

        [Column]
        public string RegionName
        {
            get { return _regionName; }
            set
            {
                if (_regionName != value)
                {
                    _regionName = value;
                    OnPropertyChanged("RegionName");
                }
            }
        }

        [Column]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }




        [Column]
        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }
        [Column]
        public int Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }



        #endregion

        #region Constructors

        public MonitorProbe()
        {
        }
        #endregion

        #region Methods

        //  TODO

        #endregion

    }
}
