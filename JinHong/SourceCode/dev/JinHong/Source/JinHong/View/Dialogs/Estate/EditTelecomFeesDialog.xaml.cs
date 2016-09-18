﻿using System;
using System.Windows;
using System.Diagnostics;

using JinHong.Model;

namespace JinHong.View.Dialogs
{
    public partial class EditTelecomFeesDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty ModeProperty
            = DependencyProperty.Register("Mode", typeof(EditingMode), typeof(EditTelecomFeesDialog), new PropertyMetadata(ModePropertyChanged));

        public static readonly DependencyProperty AmountProperty
            = DependencyProperty.Register("Amount", typeof(double?), typeof(EditTelecomFeesDialog));

        public static readonly DependencyProperty NotesProperty
            = DependencyProperty.Register("Notes", typeof(string), typeof(EditTelecomFeesDialog));

        #endregion

        #region Properties

        public EditingMode Mode
        {
            get { return (EditingMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public double? Amount
        {
            get { return (double?)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        public string Notes
        {
            get { return (string)GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }

        #endregion

        #region Constructors

        public EditTelecomFeesDialog()
        {
            InitializeComponent();

            textBoxAmount.Focus();
        }

        #endregion

        #region Methods

        #region Callbacks

        private static void ModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            EditTelecomFeesDialog @this = (EditTelecomFeesDialog)d;
            EditingMode mode = (EditingMode)args.NewValue;
            switch (mode)
            {
                case EditingMode.Edit:
                    @this.Title = "编辑";
                    @this.textBoxAmount.IsEnabled = true;
                    @this.textBoxNotes.IsEnabled = true;
                    break;
                case EditingMode.View:
                    @this.Title = "查看";
                    @this.textBoxAmount.IsEnabled = false;
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
