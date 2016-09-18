using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using UniGuy.Commands;
using JinHong.View.Dialogs;
using UniGuy.Corek;
using JinHong.Helper;
using JinHong.ServiceContract;
using JinHong.Services;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;


namespace JinHong.ViewModel
{
    /// <summary>
    /// RentalParkingViewModel
    /// 业务逻辑就是：根据租赁月份生成停车位费用信息
    /// </summary>
    public class RentalParkingViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// 本地路径
        /// </summary>
        private const string LOCAL_IMG_DIR = @"bin\\debug\\";

        private static readonly Lazy<IParkingLotRentalService> lazy = new Lazy<IParkingLotRentalService>(() => new ParkingLotRentalService());

        /// <summary>
        /// 选中的租赁信息
        /// </summary>
        private ParkingLotRentalInfo _selectedParkingLotRental;

        /// <summary>
        /// 本地图片路径
        /// </summary>

        private string _localImgPath;

        /// <summary>
        /// 远程路径
        /// </summary>
        private string _remoteImgPath;


        /// <summary>
        /// 改变图标
        /// </summary>
        private ICommand _changeImgCommand;

        #endregion

        #region Properties
        public static IParkingLotRentalService Service { get { return lazy.Value; } }


        /// <summary>
        /// 获得或者设置当前选中的车位信息
        /// </summary>
        public ParkingLotRentalInfo SelectedParkingLotRental
        {
            get { return _selectedParkingLotRental; }
            set
            {
                if (_selectedParkingLotRental != value)
                {
                    _selectedParkingLotRental = value;
                    OnPropertyChanged("SelectedParkingLotRental");
                }
            }
        }
        /// <summary>
        /// 本地图片路径
        /// </summary>
        public string LocalImgPath
        {
            get { return _localImgPath; }
            set
            {
                if (_localImgPath != value)
                {
                    _localImgPath = value;
                    OnPropertyChanged("LocalHostImgPath");
                }
            }
        }

        public string RemoteImgPath
        {
            get { return _remoteImgPath; }
            set
            {
                if (_remoteImgPath != value)
                {
                    _remoteImgPath = value;
                    OnPropertyChanged("RemoteImgPath");
                }
            }
        }

        /// <summary>
        /// 更改图标
        /// </summary>
        public ICommand ChangeImgCommand
        {
            get { return _changeImgCommand; }
            set
            {
                if (_changeImgCommand != value)
                {
                    _changeImgCommand = value;
                    OnPropertyChanged("ChangeImgCommand");
                }
            }
        }

        #endregion

        #region Constructors

        public RentalParkingViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion

        #region Methods

        #region Public Methods

        public void Initialize()
        {
            this.ModuleName = "车位租赁表";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
            this.ChangeImgCommand = new DelegateCommand(OnChangeImgCommand);
            OnRefreshCommand();



        }



        private void OnExportToPdfCommand()
        {
            base.ExportToPdf(SourceTbl, ModuleName);
        }

        private void OnExportToExcelCommand()
        {
            base.ExportToExcel(SourceTbl, ModuleName);
        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelParkingLotRental(this.SelectedParkingLotRental.Id))
            {
                MessageBox.Show("删除成功！", "系统提示");
            }
            else
            {
                MessageBox.Show("删除失败！", "系统提示");
            }
        }

        private void OnAddNewCommand()
        {
            var dlg = new AddParkingRentalDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.ParkingRental = new ParkingLotRentalInfo { Id = Guid.NewGuid().ToString() };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();
        }

        private void OnEditCommand()
        {
            var dlg = new AddParkingRentalDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.ParkingRental = this.SelectedParkingLotRental;
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();
        }

        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));

        }



        private void Query(string name, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        SourceTbl = Service.GetAllParkingRentals();
                    }
                    else
                    {
                        SourceTbl = Service.GetParkingRentalByWhereStr(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }


        private void OnChangeImgCommand()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Jpg files (*.jpg)|*.jpg|Gif files (*.gif)|Png files (*.png)|*.jpg|*.jpg|All files (*.*)|*.*", //过滤文件类型
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,  //设定初始目录
                ShowReadOnly = true
            };
            if (ofd.ShowDialog().GetValueOrDefault())
            {
                this.UploadImage(ofd.FileName);
            }
        }

        #endregion


        public void SetLocalImagePath()
        {
            Task.Factory.StartNew(() =>
            {
                string sql = string.Format("SELECT Value FROM ConfigItem WHERE KKey='{0}'", "model.estate.车位图示.remoteImageName");
                RemoteImgPath = GlobalVariables.Smc.Scalar<string>(sql, null);
                string localImagePath = Path.Combine(LOCAL_IMG_DIR, new FileInfo(RemoteImgPath).Name);

                if (File.Exists(localImagePath))
                {
                    //  对比hash码
                    string sql1 = string.Format("SELECT Value FROM ConfigItem WHERE KKey='{0}'", "model.estate.车位图示.hash64");
                    string hash1 = GlobalVariables.Smc.Scalar<string>(sql1, null);
                    string hash2;
                    using (MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider())
                    {
                        //using (FileStream fs = new FileStream(localImagePath, FileMode.Open))
                        hash2 = Convert.ToBase64String(hashProvider.ComputeHash(File.ReadAllBytes(localImagePath)));
                    }
                    if (hash1 == hash2)
                        LocalImgPath = localImagePath;
                    else
                        RefreshImage(RemoteImgPath, localImagePath);
                }
                else
                    RefreshImage(RemoteImgPath, localImagePath);
            });
        }

        public void UploadImage(string localImagePath)
        {
            Task.Factory.StartNew(() =>
            {
                //  扩展名修改
                int index1 = RemoteImgPath.LastIndexOf('.');
                int index2 = localImagePath.LastIndexOf('.');
                RemoteImgPath = RemoteImgPath.Substring(0, index1) + localImagePath.Substring(index2);
                string sql = string.Format("UPDATE ConfigItem SET Value='{1}' WHERE KKey='{0}'", "model.estate.车位图示.remoteImageName", RemoteImgPath);
                GlobalVariables.Smc.NonQuery(sql);
                GlobalVariables.Smc.UploadFile(RemoteImgPath, localImagePath);

                //  写hash码
                string hash64;
                using (MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider())
                {
                    using (FileStream fs = new FileStream(localImagePath, FileMode.Open))
                        hash64 = Convert.ToBase64String(hashProvider.ComputeHash(fs));
                }
                sql = string.Format("SELECT count(*) FROM ConfigItem WHERE KKey='{0}'", "model.estate.车位图示.hash64");
                if (GlobalVariables.Smc.Scalar<int>(sql) > 0)
                    sql = string.Format("UPDATE ConfigItem SET Value='{1}' WHERE KKey='{0}'", "model.estate.车位图示.hash64", hash64);
                else
                    sql = string.Format("INSERT INTO ConfigItem (KKey, Value) VALUES('{0}', '{1}')", "model.estate.车位图示.hash64", hash64);
                GlobalVariables.Smc.NonQuery(sql, null);

                LocalImgPath = localImagePath;
            });
        }

        public void RefreshImage(string remoteImagePath, string localImagePath)
        {
            QueryImage(remoteImagePath, localImagePath, null);
        }

        /// <summary>
        /// 查询图示
        /// </summary>
        /// <param name="actCompleted"></param>
        public void QueryImage(string remoteImagePath, string localImagePath, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    Task.Factory.StartNew(() =>
                    {
                        // 如果服务端的图片和本地不同,则查询并下载到LocalImagePath; 否则跳过
                        GlobalVariables.Smc.DownloadFile(remoteImagePath, localImagePath);
                        LocalImgPath = localImagePath;
                    });
                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }


        #endregion
    }
}
