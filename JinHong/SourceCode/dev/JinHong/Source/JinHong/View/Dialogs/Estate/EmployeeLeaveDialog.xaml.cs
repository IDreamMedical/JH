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

using JinHong.Model;

namespace JinHong.View.Dialogs
{
    public partial class EmployeeLeaveDialog : Window
    {
        #region  Dependency properties

        public static readonly DependencyProperty EmployeeNameProperty = DependencyProperty.Register(
            "EmployeeName", typeof(string), typeof(EmployeeLeaveDialog));

        public static readonly DependencyProperty LeaveDateProperty = DependencyProperty.Register(
            "LeaveDate", typeof(DateTime), typeof(EmployeeLeaveDialog));

        public static readonly DependencyProperty LeaveReasonCodeProperty = DependencyProperty.Register(
            "LeaveReasonCode", typeof(string), typeof(EmployeeLeaveDialog));

        #endregion

        #region Properties

        public string EmployeeName
        {
            get { return (string)GetValue(EmployeeNameProperty); }
            set { SetValue(EmployeeNameProperty, value); }
        }

        public DateTime LeaveDate
        {
            get { return (DateTime)GetValue(LeaveDateProperty); }
            set { SetValue(LeaveDateProperty, value); }
        }

        public string LeaveReasonCode
        {
            get { return (string)GetValue(LeaveReasonCodeProperty); }
            set { SetValue(LeaveReasonCodeProperty, value); }
        }

        #endregion

        #region Constructors

        public EmployeeLeaveDialog()
        {
            LeaveDate = DateTime.Today;
            LeaveReasonCode = "2";

            InitializeComponent();
        }

        #endregion

        #region Methods

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
