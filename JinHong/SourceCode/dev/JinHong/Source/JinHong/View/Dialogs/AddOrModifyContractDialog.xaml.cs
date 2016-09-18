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
    public partial class AddOrModifyContractDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register
            ("Mode", typeof(EditingMode), typeof(AddOrModifyContractDialog), new PropertyMetadata(ModePropertyChanged));

        public static readonly DependencyProperty ContractNameProperty = DependencyProperty.Register
            ("ContractName", typeof(string), typeof(AddOrModifyContractDialog), new PropertyMetadata(TestNamePropertyChanged));

        public static readonly DependencyProperty ContractDescriptionProperty = DependencyProperty.Register
            ("ContractDescription", typeof(string), typeof(AddOrModifyContractDialog));

        #endregion

        #region Properties

        public EditingMode Mode
        {
            get { return (EditingMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public string ContractName
        {
            get { return (string)GetValue(ContractNameProperty); }
            set { SetValue(ContractNameProperty, value); }
        }

        public string ContractDescription
        {
            get { return (string)GetValue(ContractDescriptionProperty); }
            set { SetValue(ContractDescriptionProperty, value); }
        }

        #endregion

        #region Constructors

        public AddOrModifyContractDialog()
        {
            InitializeComponent();

            textBoxContractName.SelectAll();
            textBoxContractName.Focus();
        }

        #endregion

        #region Methods

        #region General

        #endregion

        #region Callbacks

        private static void ModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            AddOrModifyContractDialog @this = (AddOrModifyContractDialog)d;

            EditingMode mode = (EditingMode)args.NewValue;
            switch (mode)
            {
                case EditingMode.Add:
                    @this.Title = "新增合同";
                    @this.textBoxContractName.IsEnabled = true;
                    //  TODO
                    break;
                case EditingMode.Modify:
                    @this.Title = "修改合同";
                    @this.textBoxContractName.IsEnabled = false;
                    //  TODO
                    break;
                default:
                    //  TODO
                    break;
            }
        }

        private static void TestNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            AddOrModifyContractDialog @this = (AddOrModifyContractDialog)d;

            string contractName = (string)args.NewValue;
            if (string.IsNullOrWhiteSpace(contractName))
            {
                @this.buttonOK.IsEnabled = false;
                return;
            }

            @this.buttonOK.IsEnabled = PathHelper.IsValidFileName(contractName);
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
