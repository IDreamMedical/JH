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
    /// 物业管理费信息(包括未交的, Id采用内部Guid)
    /// TODO, 未转物业管理费, 已转物业管理费均采用该表
    /// </summary>
    public class PropertyManagementFeesInfo : EntityBase, IIdObject
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

        public PropertyManagementFeesInfo()
        {
        }

        public PropertyManagementFeesInfo(string id)
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
