using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Core;
using UniGuy.Core.Data;
using UniGuy.Entity;

namespace JinHong.Model
{
    [Table("Area")]

    public class Area : EntityBase, IIdObject, INamedObject, IDescribable
    {

        #region Fields
        private string _id;

        private string _name;
        private string _status;

        private string _description;

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
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
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
        public string Status
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

        public Area()
        {
        }

        #endregion


    }
}
