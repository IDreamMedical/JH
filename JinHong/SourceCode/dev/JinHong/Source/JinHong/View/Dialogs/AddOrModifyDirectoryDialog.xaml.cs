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
using System.Windows.Shapes;

using System.IO;
using UniGuy.Core.Helpers;

namespace JinHong.View.Dialogs
{
    public partial class AddOrModifyDirectoryDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register
            ("Mode", typeof(EditingMode), typeof(AddOrModifyDirectoryDialog), new PropertyMetadata(ModePropertyChanged));

        public static readonly DependencyProperty DirectoryNameProperty = DependencyProperty.Register
            ("DirectoryName", typeof(string), typeof(AddOrModifyDirectoryDialog), new PropertyMetadata(CategoryNamePropertyChanged));

        #endregion

        #region Properties

        public EditingMode Mode
        {
            get { return (EditingMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public string DirectoryName
        {
            get { return (string)GetValue(DirectoryNameProperty); }
            set { SetValue(DirectoryNameProperty, value); }
        }

        #endregion

        #region Constructors

        public AddOrModifyDirectoryDialog()
        {
            InitializeComponent();

            textBoxDirectoryName.SelectAll();
            textBoxDirectoryName.Focus();
        }

        #endregion

        #region Methods

        #region General

        #endregion

        #region Callbacks

        private static void ModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            AddOrModifyDirectoryDialog @this = (AddOrModifyDirectoryDialog)d;

            EditingMode mode = (EditingMode)args.NewValue;
            switch (mode)
            {
                case EditingMode.Add:
                    @this.Title = "新增目录";
                    @this.textBoxDirectoryName.IsEnabled = true;
                    //  TODO
                    break;
                case EditingMode.Modify:
                    @this.Title = "修改目录";
                    @this.textBoxDirectoryName.IsEnabled = false;
                    //  TODO
                    break;
                default:
                    //  TODO
                    break;
            }
        }

        private static void CategoryNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            AddOrModifyDirectoryDialog @this = (AddOrModifyDirectoryDialog)d;

            string categoryName = (string)args.NewValue;
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                @this.buttonOK.IsEnabled = false;
                return;
            }

            @this.buttonOK.IsEnabled = PathHelper.IsValidFileName(categoryName);
        }
        #endregion

        #region Event handlers

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #endregion

        #region Internal types
        public enum EditingMode
        {
            Add,
            Modify
        }
        #endregion
    }

    
}
