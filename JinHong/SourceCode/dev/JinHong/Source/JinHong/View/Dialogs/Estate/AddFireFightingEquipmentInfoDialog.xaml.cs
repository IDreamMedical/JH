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

using UniGuy.Core.Extensions;
using JinHong.Model;
using System.Data;
using System.ComponentModel;

namespace JinHong.View.Dialogs
{
    public partial class AddFireFightingEquipmentInfoDialog : Window, INotifyPropertyChanged
    {
        #region Dependency properties

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register
            ("Mode", typeof(EditingMode), typeof(AddFireFightingEquipmentInfoDialog), new PropertyMetadata(ModePropertyChanged));

        public static readonly DependencyProperty FireFightingEquipmentInfoProperty = DependencyProperty.Register
            ("FireFightingEquipmentInfo", typeof(FireFightingEquipmentInfo), typeof(AddFireFightingEquipmentInfoDialog));

        #endregion


        #region Fields

        private DataTable _availableBuildingTbl;


        #endregion

        #region Properties

        public EditingMode Mode
        {
            get { return (EditingMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public DataTable AvailableBuildingTbl
        {
            get { return _availableBuildingTbl; }
            set
            {
                if (_availableBuildingTbl != value)
                {
                    _availableBuildingTbl = value;
                    OnPropertyChanged("AvailableBuildingTbl");
                }

            }
        }


        public FireFightingEquipmentInfo FireFightingEquipmentInfo
        {
            get { return (FireFightingEquipmentInfo)GetValue(FireFightingEquipmentInfoProperty); }
            set { SetValue(FireFightingEquipmentInfoProperty, value); }
        }

        #endregion

        #region Constructors

        public AddFireFightingEquipmentInfoDialog()
        {
            InitializeComponent();
            AvailableBuildingTbl = BuildingInfoHelper.GetBuildings();

        }

        #endregion

        #region Methods

        #region Callbacks

        private static void ModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            AddFireFightingEquipmentInfoDialog @this = (AddFireFightingEquipmentInfoDialog)d;

            EditingMode mode = (EditingMode)args.NewValue;
            switch (mode)
            {
                case EditingMode.Add:
                    @this.Title = "新增消防资料";
                    //  TODO
                    break;
                case EditingMode.Modify:
                    @this.Title = "编辑消防资料";
                    //  TODO
                    break;
                default:
                    //  TODO
                    break;
            }
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

        #region Interface
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        #endregion


        #region Internal types

        public enum EditingMode
        {
            Add,
            Modify
        }

        #endregion

        private void comboBoxBuildingId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
