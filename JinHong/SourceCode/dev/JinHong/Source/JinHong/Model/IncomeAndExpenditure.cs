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
    /// 收入支出项
    /// </summary>
    public class IncomeAndExpenditure : EntityBase, IIdObject
    {
        #region Fields

        //  Id, 一般自动生成
        private string id;
        private DateTime time;
        //  收入支出项类别
        private string category;
        // 收入支出数量(元), 支出为-
        private double amount;

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
        public DateTime Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged("Time");
                }
            }
        }

        [Column]
        public string Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    OnPropertyChanged("Category");
                }
            }
        }

        [Column]
        public double Amount
        {
            get { return amount; }
            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }

        #endregion

        #region Constructors

        public IncomeAndExpenditure()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public IncomeAndExpenditure(string category, double amount, DateTime time)
        {
            this.Category = category;
            this.Amount = amount;
            this.Time = time;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
