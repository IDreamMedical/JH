using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Core;

namespace UniGuy.Entity
{
    /// <summary>
    /// 用于测试
    /// </summary>
    public class Person:EntityBase, IIdObject
    {
        #region Fields

        private string id;
        private string name;
        private string gender;
        private double age;

        #endregion

        #region Properties

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

        public string Gender
        {
            get { return gender; }
            set
            {
                if (gender != value)
                {
                    gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }

        public double Age
        {
            get { return age; }
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged("Age");
                }
            }
        }

        #endregion

        #region Constructors

        public Person() { }

        public Person(string id) { this.Id = id; }

        public Person(string id, string name) : this(id) { this.Name = name; } 

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
