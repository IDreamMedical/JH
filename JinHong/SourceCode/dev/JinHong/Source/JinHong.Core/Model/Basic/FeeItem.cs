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
    /// 费用交费主记录信息
    /// </summary>
    /// 
    [Table("FeeItem")]
    public class FeeItem : EntityBase, IIdObject
    {
        #region Fields

        //  交费活动Id
        private string id;

        private string name;

        private string description;

        private int status;



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



        /// <summary>
        /// 获得或者设置交费日期
        /// </summary>
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
        public int Status
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


        #endregion

        #region Constructors

        public FeeItem()
        {
        }

        public FeeItem(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion


    }
}
