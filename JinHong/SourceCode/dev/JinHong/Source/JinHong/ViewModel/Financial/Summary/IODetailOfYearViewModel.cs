using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JinHong.Model;
using System.Threading.Tasks;

namespace JinHong.ViewModel
{
    public class IODetailOfYearViewModel : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  收支汇总表
        private DataTable incomeAndExpenditureGatherTbl;
        //  年份 yyyy
        private int whereYear;
        //  TODO

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置收支明细表
        /// </summary>
        public DataTable IncomeAndExpenditureGatherTbl
        {
            get { return incomeAndExpenditureGatherTbl; }
            set
            {
                if (incomeAndExpenditureGatherTbl != value)
                {
                    incomeAndExpenditureGatherTbl = value;
                    OnPropertyChanged("IncomeAndExpenditureGatherTbl");
                    OnPropertyChanged("TotalAmount");
                }
            }
        }

        public double TotalAmount
        {
            get { return (double)IncomeAndExpenditureGatherTbl.Compute("Sum(Amount)", "true"); }
        }

        /// <summary>
        /// 获得或者设置年份
        /// </summary>
        public int WhereYear
        {
            get { return whereYear; }
            set
            {
                if (whereYear != value)
                {
                    whereYear = value;
                    OnPropertyChanged("WhereYear");
                }
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public IODetailOfYearViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            WhereYear = DateTime.Today.Year;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 根据year获得并设置IncomeAndExpenditureGatherTbl
        /// </summary>
        public void Query()
        {
            Query(null);
        }

        public void Query(Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    string sql = string.Format(@"select  a.Id,a.Name , IFNULL(b.Amount,0) as Amount from FeeItem  a 
                                                left  JOIN MonthFeeDetail  b    on a .Id=b.ItemId and b.TypeId=2
                                                where   ifnull(strftime('%Y', b.RcdDate,'localtime'),strftime('%Y','now','localtime'))='{0}';", WhereYear);

                    DataSet ds = GlobalVariables.Smc.Select(sql, null);
                    IncomeAndExpenditureGatherTbl = ds == null ? null : ds.Tables[0];

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        public void SaveFeeValue(MonthFeeDetail currentMonthFeeDetail)
        {
            Task.Factory.StartNew(() =>
            {
                var ds = GlobalVariables.Smc.Select(String.Format("select*from MonthFeeDetail where  ItemId='{0}' and Month='{1}' and strftime('%Y', RcdDate,'localtime')='{2}' ", currentMonthFeeDetail.ItemId, currentMonthFeeDetail.Month, WhereYear));


                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    currentMonthFeeDetail.Id = ds.Tables[0].Rows[0]["Id"] + "";
                    GlobalVariables.Smc.Update<MonthFeeDetail>(currentMonthFeeDetail);
                }
                else
                {
                    GlobalVariables.Smc.Insert<MonthFeeDetail>(currentMonthFeeDetail);
                }

            });
        }


        #endregion
    }
}
