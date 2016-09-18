using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

using UniGuy.Windows.ViewModels;
using UniGuy.Core.Message;
using UniGuy.Core.Helpers;
using UniGuy.Core.Cache;

namespace JinHong.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Events

        public event EventHandler CurrentPageChanged;

        public void OnCurrentPageChanged()
        {
            if (CurrentPageChanged != null)
                CurrentPageChanged(this, EventArgs.Empty);
        }

        #endregion

        #region Fields

        public static readonly MessageManager MessageManager = new MessageManager();

        private FileVersionInfo versionInfo;

        private MainMenuViewModel mainMenuViewModel;

        //  当前页面
        private AbstractPageViewModel currentPageViewModel;
        readonly WeakCache<AbstractPageViewModel> _wcPage = new WeakCache<AbstractPageViewModel>();

        #endregion

        #region Properties

        public FileVersionInfo VersionInfo
        {
            get { return versionInfo; }
            set
            {
                if (versionInfo != value)
                {
                    versionInfo = value;
                    OnPropertyChanged("VersionInfo");
                }
            }
        }

        public MainMenuViewModel MainMenuViewModel
        {
            get { return mainMenuViewModel; }
            set
            {
                if (mainMenuViewModel != value)
                {
                    mainMenuViewModel = value;
                    OnPropertyChanged("MainMenuViewModel");
                }
            }
        }

        public AbstractPageViewModel CurrentPageViewModel
        {
            get { return currentPageViewModel; }
            set
            {
                if (currentPageViewModel != value)
                {
                    currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                    OnCurrentPageChanged();
                }
            }
        }

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {

            //  初始化工作

            //  VersionInfo
            string pathAndFileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            VersionInfo = FileVersionInfo.GetVersionInfo(pathAndFileName);

            //  StartPage
            CurrentPageViewModel = new StartPageViewModel(this);

            //  菜单项
            MainMenuViewModel = new MainMenuViewModel(this);
            MenuItemViewModel menuItemLevel1 = null;
            MenuItemViewModel menuItemLevel2 = null;
            //  财务模块
            menuItemLevel1 = new MenuItemViewModel("财务模块") { IsEnabledPrivilegeName = "财务模块_访问" };
            MainMenuViewModel.AddItem(menuItemLevel1);
            menuItemLevel1.AddItem("收入日报表");
            menuItemLevel1.AddItem("未交的房租_押金明细表");
            menuItemLevel1.AddItem("已交的房租_押金明细表");
            menuItemLevel1.AddItem("未转的物业管理费明细表");
            menuItemLevel1.AddItem("已转的物业管理费明细表");
            menuItemLevel1.AddItem("月度收支明细表");
            menuItemLevel1.AddItem("半年度收支明细表");
            menuItemLevel1.AddItem("年度收支明细表");
            menuItemLevel1.AddItem("月度收支汇总表");

            //  物业模块
            menuItemLevel1 = new MenuItemViewModel("物业模块") { IsEnabledPrivilegeName = "物业模块_访问" };
            MainMenuViewModel.AddItem(menuItemLevel1);
            menuItemLevel2 = menuItemLevel1.AddItem("人事管理");
            menuItemLevel2.AddItem("任职全厂职工名单");
            menuItemLevel2.AddItem("退休职工名单");
            menuItemLevel2 = menuItemLevel1.AddItem("消防管理");
            menuItemLevel2.AddItem("消防资料");
            menuItemLevel2 = menuItemLevel1.AddItem("物业管理");
            menuItemLevel2.AddItem("租赁单位水电费一览表");
            menuItemLevel2.AddItem("水电费总厂开票一览表");
            menuItemLevel2.AddItem("停车费统计表");
            menuItemLevel2.AddItem("钥匙管理_空调设备");
            menuItemLevel2.AddItem("电信电话_电信收费清单");
            menuItemLevel2.AddItem("维修记录");
            menuItemLevel2.AddItem("各楼层租赁单位情况表");
            menuItemLevel2.AddItem("收缴停车费明细表");
            menuItemLevel2.AddItem("平面图");
            menuItemLevel2.AddItem("应收停车费汇总表");
            menuItemLevel2.AddItem("租赁单位安全管理协议表");
            menuItemLevel2.AddItem("租赁户入住表");
            menuItemLevel2.AddItem("租赁户退租表");
            menuItemLevel2.AddItem("监控探头");
            menuItemLevel2.AddItem("车位信息管理表");
            menuItemLevel2.AddItem("抄电表数");
            menuItemLevel2.AddItem("水电费收费清单");

            //  招商模块
            menuItemLevel1 = new MenuItemViewModel("招商模块") { IsEnabledPrivilegeName = "招商模块_访问" };
            MainMenuViewModel.AddItem(menuItemLevel1);
            menuItemLevel1.AddItem("楼宇区域位置图示");
            menuItemLevel1.AddItem("租赁户企业基本情况");
            menuItemLevel2 = menuItemLevel1.AddItem("租金_物业费缴纳情况记录管理业务");
            menuItemLevel2.AddItem("押金");
            menuItemLevel2.AddItem("租金");
            menuItemLevel2.AddItem("物业费");
            menuItemLevel2.AddItem("停车费");
            menuItemLevel1.AddItem("租赁期限变更_续租_退租一览业务");
            menuItemLevel2 = menuItemLevel1.AddItem("退租流程展示表格");
            menuItemLevel2.AddItem("入租");
            menuItemLevel2.AddItem("退租");
            menuItemLevel1.AddItem("仓库租赁情况");
            menuItemLevel2 = menuItemLevel1.AddItem("合同管理");
            menuItemLevel2.AddItem("未签合同一览表");
            menuItemLevel2.AddItem("已签合同一览表");

            //  基础信息管理模块
            menuItemLevel1 = new MenuItemViewModel("系统管理模块") { IsEnabledPrivilegeName = "系统管理模块_访问" };
            MainMenuViewModel.AddItem(menuItemLevel1);
            menuItemLevel2 = menuItemLevel1.AddItem("基础信息管理");
            menuItemLevel2.AddItem("楼宇管理");
            menuItemLevel2.AddItem("角色管理");
            menuItemLevel2.AddItem("用户管理");
            menuItemLevel2.AddItem("车位管理");
            menuItemLevel2.AddItem("单元管理");
            menuItemLevel2.AddItem("仓库管理");
            menuItemLevel2.AddItem("权限管理");
            menuItemLevel2.AddItem("商户管理");
            menuItemLevel2.AddItem("空调管理");
            menuItemLevel2.AddItem("收费项目管理");
            menuItemLevel2.AddItem("角色用户对照管理");
            menuItemLevel2.AddItem("模块管理");



        }

        #endregion

        #region Methods

        /// <summary>
        /// 获得具体页面的ViewModel, 首先从缓存取, 取不到在通过委托生成
        /// </summary>
        /// <param name="key"></param>
        /// <param name="funcGetPageViewModel"></param>
        /// <returns></returns>
        private AbstractPageViewModel GetPageViewModel(object key, Func<AbstractPageViewModel> funcGetPageViewModel)
        {
            AbstractPageViewModel temp = null;
            if (!_wcPage.TryGetData(key, out temp) && funcGetPageViewModel != null)
                temp = funcGetPageViewModel();
            return temp;
        }

        public void VisitPage(MenuItemViewModel menuItemViewModel, params object[] parameters)
        {
            VisitPage(menuItemViewModel.Name, parameters);
        }

        /// <summary>
        /// 请求打开某个页面
        /// </summary>
        /// <param name="menuItemViewModel"></param>
        public void VisitPage(string pageName, params object[] parameters)
        {
            AbstractPageViewModel pageViewModel = null;

            switch (pageName)
            {
                //  财务
                case "收入日报表":
                    pageViewModel = GetPageViewModel(pageName, () => new IODetailOfDayViewModel(this));
                    break;
                case "未交的房租_押金明细表":
                    pageViewModel = GetPageViewModel(pageName, () => new RentalFeeViewModel(this));
                    break;
                case "已交的房租_押金明细表":
                    pageViewModel = GetPageViewModel(pageName, () => new RentalFeeViewModel(this));
                    break;
                case "未转的物业管理费明细表":
                    pageViewModel = GetPageViewModel(pageName, () => new ManagementFeeViewModel(this));
                    break;
                case "已转的物业管理费明细表":
                    pageViewModel = GetPageViewModel(pageName, () => new ManagementFeeViewModel(this));
                    break;
                case "月度收支明细表":
                    pageViewModel = GetPageViewModel(pageName, () => new IODetailOfMonthViewModel(this));
                    break;
                case "半年度收支明细表":
                    pageViewModel = GetPageViewModel(pageName, () => new IODetailOf6MonthViewModel(this));
                    break;
                case "年度收支明细表":
                    pageViewModel = GetPageViewModel(pageName, () => new IODetailOfYearViewModel(this));
                    break;
                case "月度收支汇总表":
                    pageViewModel = GetPageViewModel(pageName, () => new IOSummaryOfMonthViewModel(this));
                    break;

                // 物业
                case "任职全厂职工名单":
                    pageViewModel = GetPageViewModel(pageName, () => new EmployeeViewModel(this));
                    break;
                case "退休职工名单":
                    pageViewModel = GetPageViewModel(pageName, () => new EmployeeViewModel(this));
                    break;
                case "消防资料":
                    pageViewModel = GetPageViewModel(pageName, () => new FireViewModel(this));
                    break;
                case "租赁单位水电费一览表":
                    pageViewModel = GetPageViewModel(pageName, () => new 租赁单位水电费一览表VM(this));
                    break;
                case "水电费总厂开票一览表":
                    pageViewModel = GetPageViewModel(pageName, () => new PowerFeeResultViewModel(this));
                    break;
                case "停车费统计表":
                    pageViewModel = GetPageViewModel(pageName, () => new ParkingFeeSummaryViewModel(this));
                    break;
                case "钥匙管理_空调设备":
                    pageViewModel = GetPageViewModel(pageName, () => new InstallAirViewModel(this));
                    break;
                case "电信电话_电信收费清单":
                    pageViewModel = GetPageViewModel(pageName, () => new BroadBandViewModel(this));
                    break;
                case "维修记录":
                    pageViewModel = GetPageViewModel(pageName, () => new RepairViewModel(this));
                    break;
                case "各楼层租赁单位情况表":
                    pageViewModel = GetPageViewModel(pageName, () => new RentanlDetailViewModel(this));
                    break;
                case "收缴停车费明细表":
                    pageViewModel = GetPageViewModel(pageName, () => new ParkingFeeDetailOfCheckInViewModel(this));
                    break;
                case "平面图":
                    pageViewModel = GetPageViewModel(pageName, () => new ParkingLotGraphViewModel(this));
                    break;
                case "应收停车费汇总表":
                    pageViewModel = GetPageViewModel(pageName, () => new ParkingFeeDetailViewModel(this));
                    break;
                case "租赁单位安全管理协议表":
                    pageViewModel = GetPageViewModel(pageName, () => new SecurityAgreementViewModel(this));
                    break;
                case "租赁户入住表":
                    pageViewModel = GetPageViewModel(pageName, () => new CheckInViewModel(this));
                    break;
                case "租赁户退租表":
                    pageViewModel = GetPageViewModel(pageName, () => new CheckInViewModel(this));
                    break;
                case "监控探头":
                    pageViewModel = GetPageViewModel(pageName, () => new ProbeViewModel(this));
                    break;
                case "车位信息管理表":
                    pageViewModel = GetPageViewModel(pageName, () => new RentalParkingViewModel(this));
                    break;
                case "抄电表数":
                    pageViewModel = GetPageViewModel(pageName, () => new 抄电表数VM(this));
                    break;
                case "水电费收费清单":
                    pageViewModel = GetPageViewModel(pageName, () => new PowerFeeDetailViewModel(this));
                    break;

                //  招商
                case "楼宇区域位置图示":
                    pageViewModel = GetPageViewModel(pageName, () => new ParkGraphViewModel(this));
                    break;
                case "租赁户企业基本情况":
                    pageViewModel = GetPageViewModel(pageName, () => new SocialUnitIndexViewModel(this));
                    break;
                //case "租金_物业费缴纳情况记录管理业务":
                //    pageViewModel = GetPageViewModel(pageName, () => new 租金_物业费缴纳情况记录管理业务VM(this));
                //    break;
                case "押金":
                    pageViewModel = GetPageViewModel(pageName, () => new DepositFeeViewModel(this));
                    break;
                case "租金":
                    pageViewModel = GetPageViewModel(pageName, () => new RentalFeeViewModel(this));
                    break;
                case "物业费":
                    pageViewModel = GetPageViewModel(pageName, () => new ManagementFeeViewModel(this));
                    break;
                case "停车费":
                    pageViewModel = GetPageViewModel(pageName, () => new ParkingFeeViewModel(this));
                    break;
                case "租赁期限变更_续租_退租一览业务":
                    pageViewModel = GetPageViewModel(pageName, () => new RentalLogViewModel(this));
                    break;
                //case "退租流程展示表格":
                //    pageViewModel = GetPageViewModel(pageName, () => new 退租流程展示表格VM(this));
                //    break;
                case "入租":
                    pageViewModel = GetPageViewModel(pageName, () => new InRentaledVM(this));
                    break;
                case "退租":
                    pageViewModel = GetPageViewModel(pageName, () => new ReverseRentalVM(this));
                    break;
                case "仓库租赁情况":
                    pageViewModel = GetPageViewModel(pageName, () => new RentalWareHouseVM(this));
                    break;
                //case "合同管理":
                //    pageViewModel = GetPageViewModel(pageName, () =>
                //    {
                //        合同管理VM vm = new 合同管理VM(this);
                //        if (parameters != null && parameters.Length > 0)
                //        {
                //            vm.ViewContractFile(parameters[0] as string);
                //        }
                //        return vm;
                //    });
                //    break;

                case "未签合同一览表":
                    pageViewModel = GetPageViewModel(pageName, () => new UnContractListVM(this));
                    break;
                case "已签合同一览表":
                    pageViewModel = GetPageViewModel(pageName, () =>
                    {
                        ContractVM vm = new ContractVM(this);
                        if (parameters != null && parameters.Length > 0)
                        {
                            // vm.ViewContractFile(parameters[0] as string);
                        }
                        return vm;
                    });
                    break;

                // 系统管理
                case "楼宇管理":
                    pageViewModel = GetPageViewModel(pageName, () => new BuildingInfoVM(this));
                    break;
                case "角色管理":
                    pageViewModel = GetPageViewModel(pageName, () => new RoleInfoVM(this));
                    break;
                case "用户管理":
                    pageViewModel = GetPageViewModel(pageName, () => new UserInfoVM(this));
                    break;
                case "单元管理":
                    pageViewModel = GetPageViewModel(pageName, () => new RoomInfoVM(this));
                    break;
                case "仓库管理":
                    pageViewModel = GetPageViewModel(pageName, () => new WareHouseInfoVM(this));
                    break;
                case "权限管理":
                    pageViewModel = GetPageViewModel(pageName, () => new PrivilegeVM(this));
                    break;
                case "商户管理":
                    pageViewModel = GetPageViewModel(pageName, () => new SocialUnitInfoVM(this));
                    break;
                case "车位管理":
                    pageViewModel = GetPageViewModel(pageName, () => new ParkingLotInfoVM(this));
                    break;
                case "空调管理":
                    pageViewModel = GetPageViewModel(pageName, () => new AirConditionerViewModel(this));
                    break;
                case "收费项目管理":
                    pageViewModel = GetPageViewModel(pageName, () => new FeeItemVM(this));
                    break;
                case "角色用户对照管理":
                    pageViewModel = GetPageViewModel(pageName, () => new RoleOfUserManagementVM(this));
                    break;
                case "模块管理":
                    pageViewModel = GetPageViewModel(pageName, () => new ModuleViewModel(this));
                    break;

                default:
                    break;
            }

            if (pageViewModel != null)
                CurrentPageViewModel = pageViewModel;
        }



        private void InitMainMenuViewModel(string userId, MainMenuViewModel mainMenu)
        {
            var mAccountViewModel = new AccountViewModel(userId);
            if (mAccountViewModel.Modules.Count == 0) return;
            var mLevel1Menus = mAccountViewModel.Modules.Where(m => m.PId == "0").ToList();
            if (null != mLevel1Menus && mLevel1Menus.Count > 0)
            {
                foreach (var item in mLevel1Menus)
                {
                    var mMenuItemLevel1 = new MenuItemViewModel(item.Name, item.Description);
                    var mLevel2Menus = mAccountViewModel.Modules.Where(m => m.PId == item.Id).ToList();
                    if (null != mLevel2Menus && mLevel2Menus.Count > 0)
                    {
                        foreach (var itemLevel2 in mLevel2Menus)
                        {
                            mMenuItemLevel1.AddItem(new MenuItemViewModel(itemLevel2.Name, itemLevel2.Description));
                        }
                    }
                    mainMenu.AddItem(mMenuItemLevel1);
                }
            }
        }
    }
    #endregion

    //  TODO... 内容列表,etc; 应用程序信息, 登录信息等
}

