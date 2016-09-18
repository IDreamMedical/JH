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

namespace JinHong.View.Dialogs
{
    public partial class FireFightingEquipmentInfoEditDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register
            ("Mode", typeof(EditingMode), typeof(FireFightingEquipmentInfoEditDialog), new PropertyMetadata(ModePropertyChanged));

        public static readonly DependencyProperty FireFightingEquipmentInfoProperty = DependencyProperty.Register
            ("FireFightingEquipmentInfo", typeof(FireFightingEquipmentInfo), typeof(FireFightingEquipmentInfoEditDialog));

        #endregion

        #region Properties

        public EditingMode Mode
        {
            get { return (EditingMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public FireFightingEquipmentInfo FireFightingEquipmentInfo
        {
            get { return (FireFightingEquipmentInfo)GetValue(FireFightingEquipmentInfoProperty); }
            set { SetValue(FireFightingEquipmentInfoProperty, value); }
        }

        #endregion

        #region Constructors

        public FireFightingEquipmentInfoEditDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Callbacks

        private static void ModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            FireFightingEquipmentInfoEditDialog @this = (FireFightingEquipmentInfoEditDialog)d;

            EditingMode mode = (EditingMode)args.NewValue;
            switch (mode)
            {
                case EditingMode.Add:
                    @this.Title = "新增消防资料";
                    @this.textBoxBuildingId.IsEnabled = true;
                    //  TODO
                    break;
                case EditingMode.Modify:
                    @this.Title = "编辑消防资料";
                    @this.textBoxBuildingId.IsEnabled = false;
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

        #region Internal types

        public enum EditingMode
        {
            Add,
            Modify
        }

        #endregion
    }
}
