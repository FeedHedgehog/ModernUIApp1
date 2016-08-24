using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModerUI.Startup.Loader.Startup
{
    /// <summary>
    /// 使用vb的方式处理单例传入命令行参数（运行时、启动时）
    /// </summary>
    public class SingleInstanceApplicationWrapper : WindowsFormsApplicationBase
    {
        /// <summary>
        /// 是否单实例
        /// </summary>
        public SingleInstanceApplicationWrapper()
        {
            this.IsSingleInstance = false;
        }

        /// <summary>
        /// BootstrapperType类型
        /// </summary>
        public Type BootstrapperType { get; set; }

        /// <summary>
        /// 第一次启动
        /// </summary>
        /// <param name="e">startup eventargs</param>
        /// <returns>如果完成返回状态</returns>
        protected override bool OnStartup(StartupEventArgs e)
        {
            App app = new App();
            app.BootstrapperType = this.BootstrapperType;
            app.InitializeComponent();
            //if (e.CommandLine != null)
            //    app.args = e.CommandLine.ToArray();
            app.Run();
            return false;
        }

        /// <summary>
        /// 单实例应用程序后续启动时运行
        /// </summary>
        /// <param name="e">后续启动传入的参数</param>
        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs e)
        {
            try
            {
                base.OnStartupNextInstance(e);
                //if (e.CommandLine != null && e.CommandLine.Count > 0)
                //{
                //    if (DataModel.Instance.IInfoWindow != null)
                //        App.HandleAppArgs(e.CommandLine.ToArray(), DataModel.Instance.IInfoWindow);
                //}
            }
            catch (Exception ex)
            {
                //AppClient.Show(App.GetLastException(ex).Message);
                //if (ex is MsgException)
                //    return;
                string spitstr = "\r\n<------------单实例后续接收消息异常---------------->\r\n";
                string exceptionStr = string.Format("{0}{1}{2}{3}{4}", ex.Message, spitstr, ex.StackTrace, spitstr, ex.Source);
                //ThinkMoreLogger.Instance.LoggerAdapter.Log(ex.Message, Category.Exception, Priority.High);
            }
        }
    }
}
