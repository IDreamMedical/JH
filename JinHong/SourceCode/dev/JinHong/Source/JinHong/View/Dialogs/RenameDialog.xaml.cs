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
    public partial class RenameDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register
            ("TargetName", typeof(string), typeof(RenameDialog), new PropertyMetadata(CategoryNamePropertyChanged));

        #endregion

        #region Properties

        public string TargetName
        {
            get { return (string)GetValue(TargetNameProperty); }
            set { SetValue(TargetNameProperty, value); }
        }

        #endregion

        #region Constructors

        public RenameDialog()
        {
            InitializeComponent();

            textBoxTargetName.SelectAll();
            textBoxTargetName.Focus();
        }

        #endregion

        #region Methods

        #region General

        #endregion

        #region Callbacks

        private static void CategoryNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            RenameDialog @this = (RenameDialog)d;

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
    }
}
