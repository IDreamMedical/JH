using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JinHong.Model;
using System.Threading.Tasks;
using UniGuy.Core.Extensions;

namespace JinHong.ViewModel
{
    public class IODetailOfMonthViewModel : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  收支汇总表
        private DataTable incomeAndExpenditureGatherTbl;

        //  筛选条件(日期, 只用年月)
        private DateTime whereDate;

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
        /// 获得或者设置筛选条件(日期, 只用年月)
        /// </summary>
        public DateTime WhereDate
        {
            get { return whereDate; }
            set
            {
                if (whereDate != value)
                {
                    whereDate = value;
                    OnPropertyChanged("WhereDate");
                }
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public IODetailOfMonthViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            WhereDate = DateTime.Today;
        }

        #endregion

        #region Methods

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
                    // 查询收支费用, 对于月统计等等级, 也需要对Date聚合分组, 采用取字符串前半部分或日期函数进行
                    //  TODO
                    string sql = string.Format(@"select  a.Id,a.Name , IFNULL(b.Amount,0) as Amount  from FeeItem  a 
                                                left  JOIN MonthFeeDetail  b    on a .Id=b.ItemId and  TypeId=0 ", WhereDate.ToString("yyyy-MM"));
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
                var ds = GlobalVariables.Smc.Select(String.Format("select*from MonthFeeDetail where  ItemId='{0}' and Month='{1}' and strftime('%Y', RcdDate,'localtime')='{2}'", currentMonthFeeDetail.ItemId, currentMonthFeeDetail.Month, WhereDate.ToString("yyyy")));


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
