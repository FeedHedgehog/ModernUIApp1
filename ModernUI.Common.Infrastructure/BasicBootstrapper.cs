namespace ModernUI.Common.Infrastructure
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.Threading.Tasks;
    using System.Windows;

    using Microsoft.Practices.Prism.MefExtensions;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.ServiceLocation;

    using ModernUI.Common.Infrastructure.Export;
    using ModernUI.Common.Infrastructure.Interfaces;

    /// <summary>
    /// 基本自举类
    /// </summary>
    public class BasicBootstrapper : MefBootstrapper
    {
        #region Overrides

        /// <summary>
        /// 确认MEF导入目录
        /// </summary>
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            //添加具体自举类所在的程序集,一般情况下,其和Shell位于同一程序集
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(this.GetType().Assembly));
        }

        /// <summary>
        /// 确认默认行为
        /// </summary>
        /// <returns>默认行为工厂</returns>
        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }

        /// <summary>
        /// 创建Shell
        /// </summary>
        /// <returns>创建好的Shell</returns>
        protected override DependencyObject CreateShell()
        {
            var shell = Container.GetExportedValueOrDefault<IShell>();
            if (shell is Window)
            {
                return shell as Window;
            }

            throw new Exception("请确保存在IShell接口的Window,并且正确的导出");
        }

        /// <summary>
        /// 初始化Shell
        /// </summary>
        protected override void InitializeShell()
        {
            //base.InitializeShell(); //注释掉该句,否则会创建两个Shell,2015-11-13

            //查找Splash
            var splash = Container.GetExportedValueOrDefault<ISplash>();
            if ((splash != null) && splash.EnableSplash)
            {
                if (!(splash is Window))
                {
                    throw new Exception("请确保存在实现IShell接口的Window");
                }

                var splashWindow = splash as Window;
                splashWindow.Show();
                Task.Factory.StartNew(
                    () =>
                    {
                        var preloads = ServiceLocator.Current.GetAllInstances<IPreload>();

                        //进行预加载
                        foreach (var preload in preloads)
                        {
                            if (splash.UpdateSplashInfo != null)
                            {
                                splashWindow.Dispatcher.Invoke(
                                    new Action(() => splash.UpdateSplashInfo(preload.PreloadInfo)));
                            }
                            preload.Preload();
                        }

                        //展示主窗口
                        var mainWindow = this.Shell as Window;
                        if (mainWindow != null)
                        {
                            mainWindow.Dispatcher.Invoke(
                                new Action(
                                    () =>
                                    {
                                        Application.Current.MainWindow = mainWindow;
                                        mainWindow.Activated += (s, e) => splashWindow.Dispatcher.Invoke(new Action(splashWindow.Close)); ;
                                        mainWindow.Activated += this.MainWindowActivated;
                                        mainWindow.Show();
                                    }));
                        }
                    });
            }
            else
            {
                Application.Current.MainWindow = this.Shell as Window;
                var mainWindow = Application.Current.MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.Dispatcher.Invoke(new Action(
                        () =>
                        {
                            mainWindow.Activated += this.MainWindowActivated;
                            mainWindow.Show();
                        }));
                }
            }
        }

        /// <summary>
        /// 主窗口激活的响应
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void MainWindowActivated(object sender, EventArgs e)
        {
            var startPage = Container.GetExportedValueOrDefault<IStartPage>();
            if ((startPage != null) && startPage.EnableStartPage && (startPage is Window))
            {
                startPage.EnableStartPage = false;
                var startWindow = startPage as Window;
                startWindow.Owner = Application.Current.MainWindow;
                startWindow.ShowDialog();
            }
        }

        #endregion Overrides
    }
}