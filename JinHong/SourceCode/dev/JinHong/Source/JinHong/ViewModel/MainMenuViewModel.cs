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
    public class MainMenuViewModel : ViewModelBase
    {
        #region Fields

        private MainWindowViewModel parentVM;

        private ObservableCollection<MenuItemViewModel> items;
        private MenuItemViewModel selectedItem;

        #endregion

        #region Properties

        public MainWindowViewModel ParentVM
        {
            get { return parentVM; }
            protected set
            {
                if (parentVM != value)
                {
                    parentVM = value;
                    OnPropertyChanged("ParentVM");
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
                    OnPropertyChanged("DescendentItems");
                }
            }
        }

        /// <summary>
        /// 二代菜单模块
        /// </summary>
        public IEnumerable<MenuItemViewModel> DescendentItems
        {
            get
            {
                if (Items != null)
                    foreach (var item in Items)
                        if (item.Items != null)
                            foreach (var level2 in item.Items)
                                yield return level2;
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

        #endregion

        #region Constructors

        public MainMenuViewModel(MainWindowViewModel parentVM)
        {
            this.ParentVM = parentVM;
        }

        #endregion

        #region Methods

        public void AddItem(MenuItemViewModel item)
        {
            if (Items == null)
                Items = new ObservableCollection<MenuItemViewModel>();
            item.Root = this;
            Items.Add(item);
        }

        public MenuItemViewModel AddItem(string name, string description = null)
        {
            if (Items == null)
                Items = new ObservableCollection<MenuItemViewModel>();
            MenuItemViewModel item = new MenuItemViewModel(name, description, null);
            item.Root = this;
            Items.Add(item);
            return item;
        }

        #endregion

        //  TODO

    }
}
