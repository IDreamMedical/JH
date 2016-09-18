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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JinHong.View
{
    /// <summary>
    /// ControlBar.xaml 的交互逻辑
    /// </summary>
    public partial class ControlBar : UserControl
    {
        #region Fields

        //WindowState _oldWindowState;

        #endregion

        #region Properties

        public Window OwnerWindow
        {
            get { return Window.GetWindow(this); }
        }

        #endregion

        #region Constructors

        public ControlBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Event handlers

        private void imageClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OwnerWindow != null)
                OwnerWindow.Close();
        }

        private void imageMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OwnerWindow != null)
            {
                //_oldWindowState = OwnerWindow.WindowState;
                OwnerWindow.WindowState = WindowState.Minimized;
            }
        }

        private void imageMaximize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OwnerWindow != null)
            {
                if (OwnerWindow.WindowState != WindowState.Maximized)
                {
                    //_oldWindowState = OwnerWindow.WindowState;
                    OwnerWindow.WindowState = WindowState.Maximized;
                }
                else
                {
                    OwnerWindow.WindowState = WindowState.Normal;
                }
            }
        }

        #endregion

        #endregion
    }
}
