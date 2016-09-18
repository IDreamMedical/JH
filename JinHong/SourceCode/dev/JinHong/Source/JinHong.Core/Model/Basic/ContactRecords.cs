using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Core;
using UniGuy.Core.Data;
using UniGuy.Entity;

namespace JinHong.Model
{
    [Table("ContactRecords")]
    public class ContactRecords : EntityBase, IIdObject, INamedObject, IDescribable
    {

        #region Fields

        //  楼宇Id
        private string id;
        //  单元
        private string filePath;

        private string description;

        private string name;

        //  描述
        private string upLoadDate;

        private string upLoadPerson;


        // 楼宇
        private string status;


        private string fileName;

        private string contractId;


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

        [System.Xml.Serialization.XmlIgnore]
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

        [System.Xml.Serialization.XmlIgnore]
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
        public string ContractId
        {
            get { return contractId; }
            set
            {
                if (contractId != value)
                {
                    contractId = value;
                    OnPropertyChanged("ContractId");
                }
            }
        }
         [Column]
        public string FilePath
        {
            get { return filePath; }
            set
            {
                if (filePath != value)
                {
                    filePath = value;
                    OnPropertyChanged("FilePath");
                }
            }
        }
         [Column]
        public string UpLoadDate
        {
            get { return upLoadDate; }
            set
            {
                if (upLoadDate != value)
                {
                    upLoadDate = value;
                    OnPropertyChanged("UpLoadDate");
                }
            }
        }
         [Column]
        public string UpLoadPerson
        {
            get { return upLoadPerson; }
            set
            {
                if (upLoadPerson != value)
                {
                    upLoadPerson = value;
                    OnPropertyChanged("UpLoadPerson");
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
        public string FileName
        {
            get { return fileName; }
            set
            {
                if (fileName != value)
                {
                    fileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }




        #endregion

        #region Constructors

        public ContactRecords()
        {
        }

        public ContactRecords(string id)
        {
            this.Id = id;
        }
        #endregion
    }
}
