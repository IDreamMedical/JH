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

using JinHong.ViewModel;
using JinHong.View.Dialogs;
using UniGuy.Entity;

namespace JinHong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(MainWindowViewModel), typeof(MainWindow), new PropertyMetadata(null, OnViewModelPropertyChanged));

        #endregion

        #region Fields

        //  历史访问相关
        readonly List<object> _history = new List<object>();
        int _currentIndex = -1;

        #endregion

        #region Properties

        public MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        #endregion

        #region Constructors

        public MainWindow()
        {
            //  登陆
            GlobalVariables.Smc.ServiceClientExceptionOccurred += (o, e) => MessageBox.Show(e.Exception.ToString());
            LoginDialog ld = new LoginDialog();
            bool enableSystemUser = GlobalVariables.GetConfigurationSetting<bool>("login.systemLogin"); //  是否允许直接系统账户登陆, 在App.exe.config中, 主要用于测试, 发布后去掉该配置
            if (GlobalVariables.GetConfigurationSetting<bool>("login.debugLogin")) //  是否自动填写系统用户名和密码
            {
                ld.Username = "sys";
                ld.SetPassword("JinHong");
            }
            ld.LoginFunc = new Func<string, string, LoginDialog.LoginResult>((n, p) =>
            {
                User user = null;
                bool success = false;
                string message = null;

                if (enableSystemUser && n == "sys")
                {
                    user = new User(n, "JinHong");
                    success = user.Verify(p);
                    if (!success)
                        message = "密码错误";
                }
                else
                {
                    user = GlobalVariables.Smc.Load<User>(n);
                    if (user == null)
                        message = "用户名不存在";
                    else
                    {
                        success = user.Verify(p);
                        if (!success)
                            message = "密码错误";
                    }
                }
                if (!success)
                    return new LoginDialog.LoginResult(success, message);

                GlobalVariables.AppStatusInfo.User = user;
                return LoginDialog.LoginResult.SuccessResult;
            });
            //  登陆成功
            if (!ld.ShowDialog().GetValueOrDefault())
            {
                this.Close();
            }

            ViewModel = new MainWindowViewModel();
            InitializeComponent();

            //  其他初始化...
            statusBar.ViewModel = new StatusBarViewModel(ViewModel);

            //  TODO
        }

        #endregion

        #region Methods

        #region Private

        private void AddToHistory(object item)
        {
            if (_currentIndex == -1 || _history[_currentIndex] != item)
            {
                _currentIndex++;
                while (_history.Count > _currentIndex)
                    _history.RemoveAt(_currentIndex);
                _history.Add(item);
            }
        }

        #endregion

        #region Overrides

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.Key == Key.System && e.SystemKey == Key.F10)
                moduleSelector.ShowModules();
        }

        #endregion

        #region Callbacks

        static void OnViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            MainWindow @this = (MainWindow)d;
            MainWindowViewModel viewModel = args.NewValue as MainWindowViewModel;

            if (viewModel != null)
                viewModel.CurrentPageChanged += (o, a) => @this.AddToHistory(viewModel.CurrentPageViewModel);

            //  TODO
        }

        #endregion

        #region Command handlers

        #region Goto previous document tab
        public void GotoPrevious_CanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = _currentIndex > 0;
        }
        public void GotoPrevious_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            _currentIndex--;
            ViewModel.CurrentPageViewModel = _history[_currentIndex] as AbstractPageViewModel;
        }
        #endregion

        #region Goto next document tab

        public void GotoNext_CanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = _currentIndex < _history.Count - 1;
        }

        public void GotoNext_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            _currentIndex++;
            ViewModel.CurrentPageViewModel = _history[_currentIndex] as AbstractPageViewModel;
        }

        #endregion

        private void HelpAbout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void HelpAbout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            new AboutDialog { Owner = this }.ShowDialog();
        }

        #endregion

        #region Event handlers

        private void gridTop_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.Source != moduleSelector)
                this.DragMove();
        }

        #endregion
        

        #endregion

        
    }
}
