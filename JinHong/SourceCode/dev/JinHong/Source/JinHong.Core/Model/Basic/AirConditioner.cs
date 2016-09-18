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
    /// 楼宇信息
    /// </summary>
    [Table("AirConditioner")]
    public class AirConditioner : EntityBase, IIdObject, INamedObject, IDescribable
    {
        #region Fields

        //  楼宇Id
        private string id;
        //  名称
        private string name;
        //  描述
        private string description;
        // 状态
        private string status;

        private int powerRating;

        private int pages;


        //  TODO...

        #endregion

        #region Properties

        [PrimaryKey]



        public string Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        [Column]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        [Column]
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }




        [Column]
        public string Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        [Column]
        public int PowerRating
        {
            get { return powerRating; }
            set
            {
                if (powerRating != value)
                {
                    powerRating = value;
                    OnPropertyChanged("PowerRating");
                }
            }
        }


        [Column]
        public int Pages
        {
            get { return pages; }
            set
            {
                if (pages != value)
                {
                    pages = value;
                    OnPropertyChanged("Pages");
                }
            }
        }


        #endregion

        #region Constructors

        public AirConditioner()
        {
        }

        public AirConditioner(string id)
        {
            this.Id = id;
        }

        public AirConditioner(string id, string name = null, string description = null)
            : this(id)
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion

        //  TODO... Id, etc
    }
}
