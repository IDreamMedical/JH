using System;
using System.Windows;
using System.Diagnostics;

using JinHong.Model;

namespace JinHong.View.Dialogs
{
    public partial class EditWaterAndElectricityFeesDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty ModeProperty
            = DependencyProperty.Register("Mode", typeof(EditingMode), typeof(EditWaterAndElectricityFeesDialog), new PropertyMetadata(ModePropertyChanged));

        public static readonly DependencyProperty WaterAmountProperty
            = DependencyProperty.Register("WaterAmount", typeof(double?), typeof(EditWaterAndElectricityFeesDialog));

        public static readonly DependencyProperty ElectricityAmountProperty
            = DependencyProperty.Register("ElectricityAmount", typeof(double?), typeof(EditWaterAndElectricityFeesDialog));

        public static readonly DependencyProperty NotesProperty
            = DependencyProperty.Register("Notes", typeof(string), typeof(EditWaterAndElectricityFeesDialog));

        public static readonly DependencyProperty TotalAmountProperty
           = DependencyProperty.Register("TotalAmount", typeof(double?), typeof(EditWaterAndElectricityFeesDialog));
        #endregion

        #region Properties

        public EditingMode Mode
        {
            get { return (EditingMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public double? WaterAmount
        {
            get { return (double?)GetValue(WaterAmountProperty); }
            set { SetValue(WaterAmountProperty, value); }
        }

         public double? ElectricityAmount
        {
            get { return (double?)GetValue(ElectricityAmountProperty); }
            set { SetValue(ElectricityAmountProperty, value); }
        }

        public string Notes
        {
            get { return (string)GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }

        public double? TotalAmount
        {
            get { return (double?)GetValue(TotalAmountProperty); }
            set { SetValue(TotalAmountProperty, value); }
        }

        #endregion

        #region Constructors

        public EditWaterAndElectricityFeesDialog()
        {
            InitializeComponent();

            textBoxElectricityAmount.Focus();
        }

        #endregion

        #region Methods

        #region Callbacks

        private static void ModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            EditWaterAndElectricityFeesDialog @this = (EditWaterAndElectricityFeesDialog)d;
            EditingMode mode = (EditingMode)args.NewValue;
            switch (mode)
            {
                case EditingMode.Edit:
                    @this.Title = "编辑";
                    @this.textBoxWaterAmount.IsEnabled = true;
                    @this.textBoxElectricityAmount.IsEnabled = true;
                    @this.textBoxNotes.IsEnabled = true;
                    break;
                case EditingMode.View:
                    @this.Title = "查看";
                    @this.textBoxWaterAmount.IsEnabled = false;
                    @this.textBoxElectricityAmount.IsEnabled = false;
                    @this.textBoxNotes.IsEnabled = false;
                    break;
                default:
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
            Edit,
            View
        }
        #endregion
    }
}
