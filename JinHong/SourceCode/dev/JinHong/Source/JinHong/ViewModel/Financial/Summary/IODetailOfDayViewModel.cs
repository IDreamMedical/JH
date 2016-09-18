using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JinHong.Model;
using System.Threading.Tasks;

namespace JinHong.ViewModel
{
    public class IODetailOfDayViewModel : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  按日期,单位分组的收入信息表; 这些信息是各类收支信息的集合; 需要分组查询后合并数据到一个表中
        private DataTable dailyIncomeInfoTbl;
        private DataRow selectedIncomeInfoRow;
        //  当前选中的收入支出信息
        private IncomeAndExpenditure selectedIncomeAndExpenditure;

        //  筛选条件(日期)
        private DateTime whereDate;

        //  TODO

        #endregion

        #region Properties

        public DataTable DailyIncomeInfoTbl
        {
            get { return dailyIncomeInfoTbl; }
            set
            {
                if (dailyIncomeInfoTbl != value)
                {
                    dailyIncomeInfoTbl = value;
                    OnPropertyChanged("DailyIncomeInfoTbl");
                }
            }
        }

        public DataRow SelectedIncomeInfoRow
        {
            get { return selectedIncomeInfoRow; }
            set
            {
                if (selectedIncomeInfoRow != value)
                {
                    selectedIncomeInfoRow = value;
                    OnPropertyChanged("SelectedIncomeInfoRow");
                }
            }
        }

        public IncomeAndExpenditure SelectedIncomeAndExpenditure
        {
            get { return selectedIncomeAndExpenditure; }
            set
            {
                if (selectedIncomeAndExpenditure != value)
                {
                    selectedIncomeAndExpenditure = value;
                    OnPropertyChanged("SelectedIncomeAndExpenditure");
                }
            }
        }

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

        public IODetailOfDayViewModel(MainWindowViewModel parentVM)
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
                    string sql = string.Format(@"SELECT a.ContractNo,  b.SocialUnitName,a.EffectiveDate||'--'||a.ExpirateDate as RentalDate ,  c.Date, c.TimeFrom||'--'||c.TimeTo as TimeSlot , b.Amount as DepositFeesAmount, c.Amount as  RentalFeesAmount,
d.Amount as  PropertyManagementFeesAmount, e.Amount as ParkingFeesAmount, b.Amount+c.Amount+d.Amount as TotalAmount,a.Notes
from ContractInfo a 
left JOIN   DepositFeesInfo b on a.SocialUnitId=b.SocialUnitId
left JOIN   RentalFeesInfo c on a.SocialUnitId=c.SocialUnitId
left JOIN   PropertyManagementFeesInfo d  on a.SocialUnitId=d.SocialUnitId
left JOIN   ParkingFeesInfo  e    on a.SocialUnitId=e.SocialUnitId
where  SUBSTR(c.Date,1,10) = '{0}'", WhereDate.ToString("yyyy-MM-dd"));
                    DataSet ds = GlobalVariables.Smc.Select(sql, null);
                    DailyIncomeInfoTbl = ds == null ? null : ds.Tables[0];

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        //  TODO

        #endregion

        #region Internal types

        //  TODO

        #endregion
    }
}
