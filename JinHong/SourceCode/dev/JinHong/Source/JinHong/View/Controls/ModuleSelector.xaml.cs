using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JinHong.ViewModel;
using JinHong.Windows.Data;
using System.Diagnostics;

namespace JinHong.View.Controls
{
    /// <summary>
    /// Interaction logic for ModuleSelector.xaml
    /// </summary>
    public partial class ModuleSelector : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(MainMenuViewModel), typeof(ModuleSelector));

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            "Mode", typeof(ViewMode), typeof(ModuleSelector));

        #endregion

        #region Fields

        MenuItemNameToAbbreviationConverter _convMenuItemNameToAbbreviation = new MenuItemNameToAbbreviationConverter();
        CollectionViewSource cvsModules = null;

        #endregion

        #region Properties

        public MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public ViewMode Mode
        {
            get { return (ViewMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        #endregion

        #region Constructors

        public ModuleSelector()
        {
            InitializeComponent();

            cvsModules = (CollectionViewSource)this.FindResource("cvsModules");
        }

        #endregion

        #region Methods

        #region Private

        private void InvokeMenuItem(MenuItemViewModel vm)
        {
            Debug.Assert(vm != null);

            if (vm != null)
            {
                if (vm.Parent != null)
                    vm.Root.SelectedItem = vm.Parent;
                vm.Root.ParentVM.VisitPage(vm);
            }
            popupModules.IsOpen = false;
        }

        #endregion

        #region Public

        public void ShowModules()
        {
            if (popupModules.IsOpen = !popupModules.IsOpen)
                textBoxSearch.Focus();
        }

        #endregion

        #region Overrides

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.Key == Key.Down)
                listBoxModules.SelectedIndex = (listBoxModules.SelectedIndex + 1) % listBoxModules.Items.Count;
            else if (e.Key == Key.Up)
                listBoxModules.SelectedIndex = (listBoxModules.SelectedIndex - 1 + listBoxModules.Items.Count) % listBoxModules.Items.Count;
            else if (e.Key == Key.Enter)
                InvokeMenuItem(listBoxModules.SelectedValue as MenuItemViewModel);

        }

        #endregion

        #region Event handlers

        private void imageListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mode = ViewMode.ListView;
        }

        private void imageTileView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mode = ViewMode.TileView;
        }

        private void listBoxModules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (e.AddedItems.Count > 0)
            {
                MenuItemVM menuItemViewModel = e.AddedItems[0] as MenuItemVM;
                if (menuItemViewModel != null)
                {
                    if (menuItemViewModel.Parent != null)
                        menuItemViewModel.Root.SelectedItem = menuItemViewModel.Parent;
                    menuItemViewModel.Root.ParentVM.VisitPage(menuItemViewModel);
                }
                popupModules.IsOpen = false;
            }*/
        }

        private void Module_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MenuItemViewModel menuItemViewModel = ((ListBoxItem)sender).DataContext as MenuItemViewModel;

            InvokeMenuItem(menuItemViewModel);
        }

        private void buttonShow_Click(object sender, RoutedEventArgs e)
        {
            if (popupModules.IsOpen = !popupModules.IsOpen)
                textBoxSearch.Focus();
        }

        private void popupModules_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!buttonShow.IsMouseOver)
                popupModules.IsOpen = false;
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cvsModules.View != null)
                cvsModules.View.Refresh();
        }

        private bool CanAccept(MenuItemViewModel item, string filterText)
        {
            if (item == null)
                return false;

            if (string.IsNullOrEmpty(filterText))
                return true;

            string itemName = (string)_convMenuItemNameToAbbreviation.Convert(item.Name, typeof(string), null, System.Globalization.CultureInfo.CurrentUICulture);
            if (string.IsNullOrEmpty(itemName))
                return false;
            if (item.Name.Contains(filterText))
                return true;
            if (UniGuy.Core.Utility.ChineseCharactorUtil.ConvertToPinyin(itemName, false).ToLower().Contains(filterText))
                return true;
            if (UniGuy.Core.Utility.ChineseCharactorUtil.ConvertToPinyin(itemName, true).ToLower().Contains(filterText))
                return true;
            return false;
        }
        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            MenuItemViewModel item = e.Item as MenuItemViewModel;
            string filterText = textBoxSearch.Text.Trim();
            e.Accepted = CanAccept(item, filterText);
        }

        private void imageSearch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (cvsModules.View != null)
                cvsModules.View.Refresh();
        }

        private void imageClearSearch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBoxSearch.Text = string.Empty;
        }

        #endregion

        #endregion

        #region Internal types

        public enum ViewMode
        {
            ListView = 0,
            TileView
        }

        #endregion
    }
}
