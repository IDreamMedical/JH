using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows;
using System.Threading;

namespace UniGuy.Windows
{
    /// <summary>
    /// SplashScreen管理器
    /// </summary>
    /// <remarks>
    /// 对于SplashScreen:
    /// 1.如果程序有登陆窗口,a)我们先载入SplashScreen然后载入部分内容,可以在这个过程中显示登录窗口;
    /// b)也可以在载入完全完成后在主窗口显示之前载入登录窗口;
    /// c)也可以不显示SplashScreen先载入登录必须要使用的验证模块后显示登录窗口,登录成功后再显示SplashScreen载入其他模块;
    /// 2.本类的使用方法:
    /// a)WPF程序手工Main方法启动;
    /// b)a中Main方法中实例化MainWindow;
    /// c)a中Main方法中调用本类的Show(SetSplashScreenDelegate setSplashScreenHandler[可以设置好DataContext], Window mainWindow)方法
    /// d)进行耗时过程,这个过程中可以动态设置splashScreen的DataContext(不同线程操作)
    /// e)App.Run(mainWindow)
    /// 注:本类的Show方法会在mainWindow载入后自动关闭splashWindow
    /// </remarks>
    public class SplashScreenManager
    {
        /// <summary>
        /// 设置SplashScreen的委托类型
        /// </summary>
        /// <returns></returns>
        /// <remarks>SplashScreen的</remarks>
        public delegate Window SetSplashScreenDelegate();

        private static Window splashScreen;

        private static readonly object mutex = new object();

        private static Window SetSplashScreen(SetSplashScreenDelegate setSplashScreenHandler)
        {
            if (setSplashScreenHandler != null)
                return setSplashScreenHandler();
            return null;
        }

        /// <summary>
        /// 显示SplashScreen
        /// </summary>
        /// <param name="setSplashScreenHandler">生成SplashScreen的方法</param>
        /// <param name="mainWindow">主窗口</param>
        public static void Show(SetSplashScreenDelegate setSplashScreenHandler, Window mainWindow)
        {
            lock (mutex)
            {
                // 跑SplashScreen的线程,一气呵成
                new Thread(() =>
                {
                    Dispatcher.CurrentDispatcher.BeginInvoke((Action)delegate()
                    {
                        //  根据外部方法创建splashScreen实例(之所以用委托是这个实例必须有另外的这个线程创建)
                        splashScreen = SetSplashScreen(setSplashScreenHandler);

                        if (splashScreen != null)
                        {
                            //  splashScreen的一些属性设置,这些属性是splahScreen应该有的,外部方法不应该设置这些属性,设置了也会在这里被覆盖
                            splashScreen.Topmost = true;
                            splashScreen.WindowState = WindowState.Normal;
                            splashScreen.ShowInTaskbar = false;
                            splashScreen.WindowStyle = WindowStyle.None;
                            splashScreen.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            splashScreen.Cursor = System.Windows.Input.Cursors.AppStarting;

                            //  传入的第二个参数用来通知载入完成
                            if (mainWindow != null)
                                mainWindow.Loaded += new RoutedEventHandler(delegate(object sender, RoutedEventArgs e) { Close(); });
                            //  显示splashScreen
                            splashScreen.Show();
                        }
                    });

                    Dispatcher.Run();
                })
                {
                    ApartmentState = ApartmentState.STA,
                    IsBackground = true
                }.Start();
            }
        }

        public static void Show(SetSplashScreenDelegate setSplashScreenHandler)
        {
            Show(setSplashScreenHandler, null);
        }

        /// <summary>
        /// 如果Show方法的mainWindow为null
        /// 则需要在完成耗时操作后手动调用本方法
        /// </summary>
        /// <remarks>
        /// 如果MainWindow的实例化本身比较耗时,或者初始化工作在MainWindow的实例化过程中完成,
        /// 则可以先调用Show方法,然后实例化MainWindow,并在实例化完成后手动调用本方法;
        /// 当然一般来说,初始化过程是在主窗口载入之前处理比较好
        /// </remarks>
        public static void Close()
        {
            if (splashScreen != null)
            {
                if (splashScreen.Dispatcher.CheckAccess())
                    splashScreen.Close();
                else
                    splashScreen.Dispatcher.Invoke((Action)(() => splashScreen.Close()));
                splashScreen = null;
            }
        }
    }
}
