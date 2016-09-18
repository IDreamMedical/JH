using System;
using System.Windows;
using System.Diagnostics;

using JinHong.Model;

namespace JinHong.View.Dialogs
{
    public partial class EditRentalAndDepositFeesDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty ModeProperty
            = DependencyProperty.Register("Mode", typeof(EditingMode), typeof(EditRentalAndDepositFeesDialog), new PropertyMetadata(ModePropertyChanged));

        public static readonly DependencyProperty RentalAmountProperty
            = DependencyProperty.Register("RentalAmount", typeof(double?), typeof(EditRentalAndDepositFeesDialog));

        public static readonly DependencyProperty IsDepositNeededProperty
            = DependencyProperty.Register("IsDepositNeeded", typeof(bool), typeof(EditRentalAndDepositFeesDialog));

        public static readonly DependencyProperty DepositAmountProperty
            = DependencyProperty.Register("DepositAmount", typeof(double?), typeof(EditRentalAndDepositFeesDialog));

        public static readonly DependencyProperty NotesProperty
            = DependencyProperty.Register("Notes", typeof(string), typeof(EditRentalAndDepositFeesDialog));

        #endregion

        #region Properties

        public EditingMode Mode
        {
            get { return (EditingMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public double? RentalAmount
        {
            get { return (double?)GetValue(RentalAmountProperty); }
            set { SetValue(RentalAmountProperty, value); }
        }

        public bool IsDepositNeeded
        {
            get { return (bool)GetValue(IsDepositNeededProperty); }
            set { SetValue(IsDepositNeededProperty, value); }
        }

         public double? DepositAmount
        {
            get { return (double?)GetValue(DepositAmountProperty); }
            set { SetValue(DepositAmountProperty, value); }
        }

        public string Notes
        {
            get { return (string)GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }

        #endregion

        #region Constructors

        public EditRentalAndDepositFeesDialog()
        {
            InitializeComponent();

            textBoxRentalAmount.Focus();
        }

        #endregion

        #region Methods

        #region Callbacks

        private static void ModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            EditRentalAndDepositFeesDialog @this = (EditRentalAndDepositFeesDialog)d;
            EditingMode mode = (EditingMode)args.NewValue;
            switch (mode)
            {
                case EditingMode.Edit:
                    @this.Title = "编辑";
                    @this.textBoxRentalAmount.IsEnabled = true;
                    //@this.textBoxDepositAmount.IsEnabled = true;
                    @this.textBoxNotes.IsEnabled = true;
                    break;
                case EditingMode.View:
                    @this.Title = "浏览";
                    @this.textBoxRentalAmount.IsEnabled = false;
                   // @this.textBoxDepositAmount.IsEnabled = false;
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
