using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 定义通用的CRUD 命令的基类
    /// </summary>
    public class BaseViewModel : AbstractPageViewModel
    {
        public BaseViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {


        }

        #region Field
        protected readonly object _syncRoot = new object();

        private DataTable _sourceTbl;

        private string _whereName;

        private string _moduleName;



        private bool _isCanExecute;

        private ICommand _refreshCommand;

        private ICommand _addNewCommand;
        private ICommand _removeCommand;
        private ICommand _exportToExcelCommand;
        private ICommand _exportToPdfCommand;
        private ICommand _editCommand;
        private ICommand _selectRowCommand;
        private ICommand _btnOKCommand;
        private ICommand _btnCancelCommand;
        private ICommand _selectItemChangedCommand;





        #endregion

        #region Prop

        public string ModuleName
        {
            get { return _moduleName; }
            set
            {
                if (_moduleName != value)
                {
                    _moduleName = value;
                    OnPropertyChanged("ModuleName");
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

        public DataTable SourceTbl
        {
            get { return _sourceTbl; }
            set
            {
                if (_sourceTbl != value)
                {
                    _sourceTbl = value;

                    OnPropertyChanged("SourceTbl");

                }

            }

        }

        public ICommand RefreshCommand
        {
            get { return _refreshCommand; }
            set
            {
                if (_refreshCommand != value)
                {
                    _refreshCommand = value;
                    OnPropertyChanged("RefreshCommand");
                }
            }
        }

        public ICommand AddNewCommand
        {
            get { return _addNewCommand; }
            set
            {
                if (_addNewCommand != value)
                {
                    _addNewCommand = value;
                    OnPropertyChanged("AddNewCommand");
                }
            }
        }

        public ICommand RemoveCommand
        {
            get { return _removeCommand; }

            set
            {
                if (_removeCommand != value)
                {
                    _removeCommand = value;
                    OnPropertyChanged("RemoveCommand");
                }
            }
        }

        public ICommand ExportToExcelCommand
        {
            get { return _exportToExcelCommand; }
            set
            {
                if (_exportToExcelCommand != value)
                {
                    _exportToExcelCommand = value;
                    OnPropertyChanged("ExportToExcelCommand");

                }
            }
        }

        public ICommand ExportToPdfCommand
        {
            get { return _exportToPdfCommand; }
            set
            {
                if (_exportToPdfCommand != value)
                {
                    _exportToPdfCommand = value;
                    OnPropertyChanged("ExportToPdfCommand");
                }
            }
        }


        public ICommand EditCommand
        {
            get { return _editCommand; }
            set
            {
                if (_editCommand != value)
                {
                    _editCommand = value;
                    OnPropertyChanged("EditCommand");
                }
            }
        }

        public ICommand SelectRowCommand
        {
            get { return _selectRowCommand; }
            set
            {
                if (_selectRowCommand != value)
                {
                    _selectRowCommand = value;
                    OnPropertyChanged("SelectRowCommand");
                }
            }
        }
        public ICommand BtnOKCommand
        {
            get { return _btnOKCommand; }
            set
            {

                if (_btnOKCommand != value)
                {
                    _btnOKCommand = value;
                    OnPropertyChanged("BtnOKCommand");
                }
            }
        }
        public ICommand BtnCancelCommand
        {
            get { return _btnCancelCommand; }
            set
            {
                if (_btnCancelCommand != value)
                {
                    _btnCancelCommand = value;
                    OnPropertyChanged("BtnCancelCommand");
                }
            }
        }


        public ICommand SelectItemChangedCommand
        {
            get { return _selectItemChangedCommand; }
            set
            {
                if (_selectItemChangedCommand != value)
                {
                    _selectItemChangedCommand = value;
                    OnPropertyChanged("SelectItemChangedCommand");
                }
            }
        }

        public virtual bool IsCanExecute
        {

            get { return _isCanExecute; }
            set
            {
                if (_isCanExecute != value)
                {
                    _isCanExecute = value;
                    OnPropertyChanged("IsCanExecute");
                    CanExecute();
                }
            }
        }




        #endregion

        #region Methods
        public virtual void ExportToExcel(DataTable sourceTbl, string moduleName)
        {

            GlobalVariables.ExportHelper.ExportToExcel(sourceTbl, moduleName);
        }

        public virtual void ExportToPdf(DataTable sourceTbl, string moduleName)
        {
            GlobalVariables.ExportHelper.ExportToPdf(sourceTbl, moduleName);
        }


        public virtual void Save()
        {

        }

        public virtual void Edit()
        {

        }

        public virtual void Del()
        {

        }

        public virtual void Cancel() { }


        public virtual bool CanExecute()
        {
            return false;
        }




        #endregion




    }
}
