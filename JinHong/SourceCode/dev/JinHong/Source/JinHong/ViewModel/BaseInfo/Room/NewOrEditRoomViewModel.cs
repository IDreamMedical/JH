using JinHong.Model;
using JinHong.Services;
using Microsoft.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using UniGuy.Commands;
using UniGuy.Corek;
using JinHong.Extensions;
namespace JinHong.ViewModel
{

    /// <summary>
    /// 新建或者编辑Room
    /// </summary>
    public class NewOrEditRoomViewModel : NewOrEditViewModelBase
    {

        #region private Field

        private ObservableCollection<BuildingInfo> _buildings;
        private RoomInfo _room;

        private static readonly Lazy<RoomService> lazy = new Lazy<RoomService>(() => new RoomService());

        #endregion

        #region Public Prop
        public ObservableCollection<BuildingInfo> Buildings
        {
            get { return _buildings; }
            private set
            {
                if (_buildings != value)
                {
                    _buildings = value;
                    OnPropertyChanged("Buildings");
                }
            }
        }
        public RoomInfo Room
        {
            get { return _room; }
            set
            {
                if (_room != value)
                {
                    _room = value;
                    OnPropertyChanged("Room");
                }
            }
        }
        public static RoomService Service { get { return lazy.Value; } }

        #endregion

        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            InitializeBuildings();
            this.BtnOKCommand = new DelegateCommand(CreateOrEditRoom);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
        }
        #endregion

        #region Private Method

        private void CreateOrEditRoom()
        {
            var result = false;
            if (IsExist())
            {
                MessageBox.Show("该室号已存在！", "系统提示");
                return;
            }
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateRoom(Room);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateRoom(Room);

                    break;
                default:
                    break;
            }
            if (result)
            {
                MessageBox.Show("保存成功！", "系统提示");
            }
            else
            {
                MessageBox.Show("保存失败！", "系统提示");
            }

        }

      
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        private bool IsExist()
        {
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    return Service.HasRoom(string.Empty, Room.BuildingId, Room.Name);
                case OperateModeEnum.Edit:
                    return Service.HasRoom(Room.Id, Room.BuildingId, Room.Name);
                default:
                    return false;
            }

        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitializeBuildings()
        {
            Buildings = new ObservableCollection<BuildingInfo>();
            var dt = new BuildingService().GetAllBuildings();
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Buildings.Add(item.BuildEntity<BuildingInfo>());
                }
            }

        }
        #endregion

    }
}
