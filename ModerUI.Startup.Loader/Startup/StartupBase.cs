using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModerUI.Startup.Loader.Startup
{
    /// <summary>
    /// 主程序启动
    /// </summary>
    public class StartupBase
    {
        [STAThread]
        public static void Run<T>(string[] args) where T : ModerUIBootstrapper, new()
        {
            //接收启动程序时和启动后的参数
            try
            {
                //args = SendData.ConvertInfo(args);
                SingleInstanceApplicationWrapper wra = new SingleInstanceApplicationWrapper();
                wra.BootstrapperType = typeof(T);
                wra.Run(args);
            }
            catch (Exception ex)
            {
                string spitstr = "\r\n<------------程序启动异常---------------->\r\n";
                string exceptionStr = string.Format("{0}{1}{2}{3}{4}", ex.Message, spitstr, ex.StackTrace, spitstr, ex.Source);
                //ThinkMoreLogger.Instance.LoggerAdapter.Log(exceptionStr, Category.Exception, Priority.High);
            }
        }
    }
}
