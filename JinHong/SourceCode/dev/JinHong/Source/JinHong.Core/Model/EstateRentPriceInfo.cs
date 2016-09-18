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
    /// 租金和物业费信息
    /// </summary>
    [Table("EstateRentPriceInfo")]
    public class EstateRentPriceInfo : EntityBase, IIdObject
    {
        #region Fields

        private string id;
        //  户名, 营业执照到期日等信息通过该Id从SocialUnitInfo获取
        private string socialUnitId;
        //  区域, 面积等信息通过这个Id从RoomInfo获取
        private string roomId;
        //  租赁期限, 租金/月, 物业费/月 根据这个Id从LeasingInfo获取
        private string leasingId;

        //  备注
        private string notes;

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
        public string SocialUnitId
        {
            get { return socialUnitId; }
            set
            {
                if (socialUnitId != value)
                {
                    socialUnitId = value;
                    OnPropertyChanged("SocialUnitId");
                }
            }
        }

        [Column]
        public string RoomId
        {
            get { return roomId; }
            set
            {
                if (roomId != value)
                {
                    roomId = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }

        [Column]
        public string LeasingId
        {
            get { return leasingId; }
            set
            {
                if (leasingId != value)
                {
                    leasingId = value;
                    OnPropertyChanged("LeasingId");
                }
            }
        }

        [Column]
        public string Notes
        {
            get { return notes; }
            set
            {
                if (notes != value)
                {
                    notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }

        #endregion

        #region Constructors

        public EstateRentPriceInfo()
        {
        }

        public EstateRentPriceInfo(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
