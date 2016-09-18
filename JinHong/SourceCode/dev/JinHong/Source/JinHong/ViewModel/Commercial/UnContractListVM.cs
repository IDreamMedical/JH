using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using JinHong.Model;
using System.Data;
using System.Threading.Tasks;
using UniGuy.Core.Extensions;


namespace JinHong.ViewModel
{
    public class UnContractListVM : AbstractPageViewModel
    {
        #region Events

        #endregion

        #region Fields
        readonly object _syncRoot = new object();
        const string REMOTE_CONTRACT_DIR = "Contracts";

        /// <summary>
        ///    未签合同Tbl
        /// </summary>
        private DataTable unContractListTbl;

        /// <summary>
        /// 选中待签合同的
        /// </summary>
        private ContractInfo selectedContractInfo;
        private DataRow selectedRow;
        private string _whereName;
        private string _contractNO;

      

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置合同信息表格
        /// </summary>

        public DataTable UnContractListTbl
        {
            get { return unContractListTbl; }
            set
            {
                if (unContractListTbl != value)
                {
                    unContractListTbl = value;
                    OnPropertyChanged("UnContractListTbl");
                }
            }
        }
        public ContractInfo SelectedContractInfo
        {
            get { return selectedContractInfo; }
            set
            {

                if (selectedContractInfo != value)
                {
                    selectedContractInfo = value;
                    OnPropertyChanged("SelectedContractInfo");
                }

            }
        }


        public string ContractNO
        {
            get { return _contractNO; }
            set
            {
                if (_contractNO != value)
                {
                    _contractNO = value;
                    OnPropertyChanged("ContractNO");
                } 
              }
        }



        public DataRow SelectedRow
        {
            get { return selectedRow; }
            set
            {
                if (selectedRow != value)
                {
                    selectedRow = value;
                    OnPropertyChanged("SelectedRow");
                }
            }
        }

        public string WhereName
        {
            get { return _whereName; }
            set
            {
                if (_whereName != value)
                {
                    _whereName = value;
                    OnPropertyChanged("WhereName");
                }
            }
        }

        #endregion

        #region Constructors

        public UnContractListVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion



        #region Methods

        public void Query(Action actCompleted)
        {

            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    var ds = GlobalVariables.Smc.Select(@"select 	a.SocialUnitId, a.SocialUnitName, a.Amount, 
				                                                    a.IsPay,  a.PreDate ,b.SocialUnitLeader,
				                                                    b.TelNo,b.LicenceDate ,b.Status from DepositFeesInfo   a 
				                                                    INNER join  SocialUnitInfo b on   a.SocialUnitId=b.Id
				                                                    WHERE  b.Status=0 
                                                                    and a.SocialUnitId NOT in (select SocialUnitId from ContractInfo)  ", null);
                    UnContractListTbl = ds == null ? null : ds.Tables[0];
                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        public void Query(string query, Action actCompleted)
        {

            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    string sqlStr = string.Format(@"select 	a.SocialUnitId, a.SocialUnitName, a.Amount, 
				                                                    a.IsPay,  a.PreDate ,b.SocialUnitLeader,
				                                                    b.TelNo,b.LicenceDate ,b.Status from DepositFeesInfo   a 
				                                                    INNER join  SocialUnitInfo b on   a.SocialUnitId=b.Id
				                                                    WHERE  b.Status=0  and  a.SocialUnitName like '%{0}%'", query);
                    var ds = GlobalVariables.Smc.Select(sqlStr, null);
                    UnContractListTbl = ds == null ? null : ds.Tables[0];
                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        public void Save(ContractInfo contractInfo, ObservableCollection<ContractDetail> contractDetails, string fileName)
        {
            if (contractInfo == null)
            {
                return;
            }
            contractInfo.LeaseType = 0;
            if (GlobalVariables.Smc.Insert<ContractInfo>(contractInfo))
            {
                RentalFeesInfo rfi = new RentalFeesInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    Amount = 0,
                    SocialUnitId = contractInfo.SocialUnitId,
                    SocialUnitName = contractInfo.SocialUnitName,
                    TimeFrom = contractInfo.EffectiveDate.Value,
                    TimeTo = contractInfo.EffectiveDate.Value.AddMonths(3).AddDays(-1)
                };
                PropertyManagementFeesInfo pmfi = new PropertyManagementFeesInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    Amount = 0,
                    SocialUnitId = contractInfo.SocialUnitId,
                    SocialUnitName = contractInfo.SocialUnitName,
                    TimeFrom = contractInfo.EffectiveDate.Value,
                    TimeTo = contractInfo.EffectiveDate.Value.AddMonths(3).AddDays(-1)

                };
                foreach (var item in contractDetails)
                {
                    GlobalVariables.Smc.Insert<ContractDetail>(item);
                    GlobalVariables.Smc.NonQuery(string.Format("UPDATE RoomInfo  set Status=1  where  id='{0}'", item.RoomId));
                    rfi.Amount = rfi.Amount + item.MonthRentalFee * 3;
                    pmfi.Amount = pmfi.Amount + item.MonthPropManageFee * 3;

                }
                GlobalVariables.Smc.Insert<PropertyManagementFeesInfo>(pmfi);
                GlobalVariables.Smc.Insert<RentalFeesInfo>(rfi);
                ContactRecords cr = new ContactRecords()
                {
                    Id = Guid.NewGuid().ToString(),
                    ContractId = contractInfo.Id,
                    Status = "0",
                    FileName = fileName
                };
                GlobalVariables.Smc.Insert<ContactRecords>(cr);
                GlobalVariables.Smc.UploadFile(contractInfo.SocialUnitName + ".doc", cr.FileName);
            }


        }




        /// <summary>
        /// 获取租赁明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetContractDetailTbl()
        {
            string contractId = SelectedContractInfo.Id;
            string sql = string.Format(
        @" select a.Id,a.RoomId,a.BuildingId,a.Area,a.Notes,a.DayPropManageFee,
          a.MonthPropManageFee,a.DayRentalFee,a.MonthRentalFee 
          from   ContractDetail a   INNER join   ContractInfo b   on   a.ContractId  =b.Id  where  b.Id='{0}'   ", contractId);
            DataSet dsTemp = GlobalVariables.Smc.Select(sql);
            return dsTemp == null ? null : dsTemp.Tables[0];
        }


        public ContractInfo ToModel(DataRow dr)
        {
            selectedContractInfo = new ContractInfo()
            {
                Id = Guid.NewGuid().ToString(),
                SocialUnitName = dr["SocialUnitName"] + "",
                SocialUnitId = dr["SocialUnitId"] + "",
                DepositFee = Convert.ToDouble(dr["Amount"]),
                LicenceDate = dr["LicenceDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["LicenceDate"]),
                CustomerTel = dr["TelNo"] == DBNull.Value ? "0" : dr["TelNo"] + "",
                PreDate=dr["PreDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["PreDate"])
            };

            return selectedContractInfo;
        }

        #endregion



    }
}
