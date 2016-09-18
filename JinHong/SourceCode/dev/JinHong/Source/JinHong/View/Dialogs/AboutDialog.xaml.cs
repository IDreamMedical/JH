using System;
using System.Windows;
using System.Diagnostics;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        #region Dependency properties
        public static readonly DependencyProperty VersionInfoProperty
            = DependencyProperty.Register("VersionInfo", typeof(FileVersionInfo), typeof(AboutDialog));
        #endregion

        #region Properties
        public FileVersionInfo VersionInfo
        {
            get { return (FileVersionInfo)GetValue(VersionInfoProperty); }
            set { SetValue(VersionInfoProperty, value); }
        }
        public string ApplicationName
        {
            get { return VersionInfo.ProductName; }
        }

        public string ApplicationVersion
        {
            get { return VersionInfo.ProductVersion; }
        }

        public string ApplicationCopyright
        {
            get { return VersionInfo.LegalCopyright; }
        }

        #endregion

        #region Constructors

        public AboutDialog()
        {
            InitializeComponent(); 
        }

        #endregion

        #region Methods

        #region Event handlers

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        #endregion

        #endregion
    }
}
