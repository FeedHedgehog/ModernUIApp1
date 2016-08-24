using Microsoft.Practices.ServiceLocation;
using ModernUI.Common.Utility.ExceptionEx;
using ModernUI.Common.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ModerUI.Startup.Loader
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
     /// BootstrapperType类型
     /// </summary>
        public Type BootstrapperType { get; set; }

        /// <summary>
        /// 获取最初始的异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns>最初始的异常</returns>
        public static Exception GetOriginException(Exception exception)
        {
            if (exception.InnerException == null)
            {
                return exception;
            }

            return GetOriginException(exception.InnerException);
        }


        private DispatcherTimer inputCheckTimer = null;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            var bootstrapper = Activator.CreateInstance(this.BootstrapperType) as ModerUIBootstrapper;
            bootstrapper.Run();

            //this.thinkMoreConfiguration = ServiceLocator.Current.GetInstance<ThinkMoreConfiguration>();
            //this.broadcast = ServiceLocator.Current.GetInstance<IBroadcast>();
            //this.userService = ServiceLocator.Current.GetInstance<IUserService>();
            //this.subjectPagesViewModel = ServiceLocator.Current.GetInstance<SubjectPagesViewModel>();
            //this.statusBar = ServiceLocator.Current.GetInstance<IStatusBar>();
            //IEventAggregator eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            //eventAggregator.GetEvent<SettingUpdatedEvent>().Subscribe(this.SettingUpdatedOnEvent);

            this.inputCheckTimer = new DispatcherTimer();
            this.inputCheckTimer.Tick += mousPositionTimer_Tick;
            this.inputCheckTimer.Interval = new TimeSpan(0, 0, 2);
            this.inputCheckTimer.Start();
        }

        private void mousPositionTimer_Tick(object sender, EventArgs e)
        {
            
        }


        /// <summary>
        /// 捕获当前应用程序域的异常
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //应用程序即将终止
            if (e.IsTerminating)
            {
                if (e.ExceptionObject is Exception)
                {
                    var exception = e.ExceptionObject as Exception;
                    Debug.WriteLine("{0}\r\n{1}", exception.Message, exception.StackTrace);
                    this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)(() => { MessageDialog.Show(string.Format("{0}\r\n{1}", exception.Message, exception.StackTrace)); }));
                }
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 处理应用程序异常
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception != null && e.Exception is MsgException)
            {
                MessageDialog.Show(e.Exception.Message);
            }
            else
            {
                var exception = GetOriginException(e.Exception);
                //ThinkMore.Startup.Loader.Log.ThinkMoreLogger.Instance.LoggerAdapter.Log(string.Format("{0}\r\n{1}", exception.Message, exception.StackTrace), Microsoft.Practices.Prism.Logging.Category.Exception, Microsoft.Practices.Prism.Logging.Priority.High);
                Debug.WriteLine("{0}\r\n{1}", exception.Message, exception.StackTrace);
            }
            //MessageBox.Show(exception.StackTrace, exception.Message);
            e.Handled = true;
        }    
    }
}
