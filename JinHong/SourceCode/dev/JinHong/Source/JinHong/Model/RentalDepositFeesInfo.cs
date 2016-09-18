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
    /// 收入信息(包括未交的, Id采用内部Guid)
    /// TODO, 日收入报表, 未交房租押金, 已交房租押金均采用该表
    /// </summary>
    public class RentalDepositFeesInfo : EntityBase, IIdObject
    {
        #region Fields

        //  楼宇Id
        private string id;

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
        #endregion

        #region Constructors

        public RentalDepositFeesInfo()
        {
        }

        public RentalDepositFeesInfo(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion

        //  TODO
    }
}
