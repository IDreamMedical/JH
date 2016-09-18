using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Core;
using UniGuy.Core.Data;
using UniGuy.Entity;

namespace JinHong.Model
{
    public class RoleMapToUser : EntityBase, IIdObject, IDescribable
    {

        #region Private field


        private string _id;


        private string _roleId;


        private string _userId;


        private string _description;
        #endregion

        #region Public Prop
        

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
        public string RoleId
        {
            get { return _roleId; }

            set
            {
                if (_roleId != value)
                {
                    _roleId = value;
                    OnPropertyChanged("RoleId");
                }
            }

        }

        [Column]
        public string UserId
        {
            get { return _userId; }

            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged("UserId");
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
        #endregion
    }
}
