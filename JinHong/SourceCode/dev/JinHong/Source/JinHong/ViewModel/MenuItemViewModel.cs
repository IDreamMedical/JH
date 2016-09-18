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

namespace JinHong.ViewModel
{
    public class MenuItemViewModel : ViewModelBase
    {
        #region Fields

        //  菜单名称, 用于显示, 转换为图标等等
        private string name;
        //  需要的时候永远显示为ToolTip
        private string description;

        // 表示IsEnabled所需的权限名称, null表示不需要权限
        private string isEnabledPrivilegeName;
        //  表示可见所需要的权限名称, null表示不需要权限
        private string visibilityPrivilegeName;

        private MainMenuViewModel root;
        private MenuItemViewModel parent;

        private ObservableCollection<MenuItemViewModel> items;
        private MenuItemViewModel selectedItem;

        #endregion

        #region Properties

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

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public MainMenuViewModel Root
        {
            get { return root; }
            internal set
            {
                if (root != value)
                {
                    root = value;
                    OnPropertyChanged("Root");
                }
            }
        }

        public MenuItemViewModel Parent
        {
            get { return parent; }
            private set
            {
                if (parent != value)
                {
                    parent = value;
                    OnPropertyChanged("Parent");
                }
            }
        }

        public ObservableCollection<MenuItemViewModel> Items
        {
            get { return items; }
            private set
            {
                if (items != value)
                {
                    items = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        public MenuItemViewModel SelectedItem
        {

            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public string IsEnabledPrivilegeName
        {
            get { return isEnabledPrivilegeName; }
            set
            {
                if (isEnabledPrivilegeName != value)
                {
                    isEnabledPrivilegeName = value;
                    OnPropertyChanged("IsEnabledPrivilegeName");
                }
            }
        }

        public string VisibilityPrivilegeName
        {
            get { return visibilityPrivilegeName; }
            set
            {
                if (visibilityPrivilegeName != value)
                {
                    visibilityPrivilegeName = value;
                    OnPropertyChanged("VisibilityPrivilegeName");
                }
            }
        }

        #endregion

        #region Constructors

        public MenuItemViewModel(MenuItemViewModel parentVM = null)
        {
            this.Parent = parentVM;
        }

        public MenuItemViewModel(string name, string description = null, MenuItemViewModel parentVM = null):this(parentVM)
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Methods

        public void AddItem(MenuItemViewModel item)
        {
            if (Items == null)
                Items = new ObservableCollection<MenuItemViewModel>();
            item.Parent = this;
            item.Root = this.Root;
            Items.Add(item);
        }

        public MenuItemViewModel AddItem(string name, string description = null)
        {
            if (Items == null)
                Items = new ObservableCollection<MenuItemViewModel>();
            MenuItemViewModel item = new MenuItemViewModel(name, description, this);
            item.Root = this.Root;
            Items.Add(item);
            return item;
        }

        #endregion
    }
}
