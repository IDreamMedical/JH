using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.IO;
using UniGuy.Core.Helpers;
using UniGuy.Core.Extensions;
using UniGuy.Windows;
using JinHong.Controller;

namespace JinHong
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region member variables
        bool m_handledFatalException = false;
        #endregion

        #region Methods

        #region Overrides
        protected override void OnStartup(StartupEventArgs e)
        {
           // RoleController t = new RoleController(null);



            base.OnStartup(e);

            GlobalVariables.Ls.LogInfo("Application startup...");

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            //  载入应用程序配置
            if (ApplicationSettings.ApplicationSettingsFileInfo.Exists)
                GlobalVariables.AppSettings = SerializationHelper.XmlDeserialize<ApplicationSettings>(ApplicationSettings.ApplicationSettingsFileInfo.FullName);
            else
                GlobalVariables.AppSettings = new ApplicationSettings();

            //  TODO
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            GlobalVariables.Ls.LogInfo("Application exit!");

            //  保存应用程序配置
            SerializationHelper.XmlSerialize<ApplicationSettings>(ApplicationSettings.ApplicationSettingsFileInfo.FullName, GlobalVariables.AppSettings);
            //  TODO
        }
        #endregion

        #region Event handlers

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        void HandleCorruptedStateException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                lock (this)
                {
                    if (m_handledFatalException || !e.IsTerminating)
                        return;
                    else
                        m_handledFatalException = true;
                }

                Exception ex = e.ExceptionObject as Exception;
                if (ex != null)
                {
                    string path = System.IO.Path.GetTempFileName();
                    System.IO.File.WriteAllText(path, ex.GetFullMessage());

                    MessageBox.Show(
                        "An unrecoverable error has occurred and the application must terminate. " +
                        string.Format("A crash dump has been saved to file {0}. Please send this file to Grass Valley for analysis.", path),
                        "GV STRATUS API Test App Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
            finally
            {
                Environment.Exit(-1);
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHelper2.HandleFatalException(e.ExceptionObject as Exception);
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            lock (this)
            {
                if (m_handledFatalException)
                    return;
                else
                    m_handledFatalException = true;
            }

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "JinHong\\FatalException.log");
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            System.IO.File.AppendAllText(path, e.Exception.GetFullMessage());

            Environment.Exit(-1);
            e.Handled = true;
        }

        #endregion

        #endregion
    }
}
