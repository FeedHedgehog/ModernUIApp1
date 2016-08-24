using ModernUI.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace ModerUI.Startup.Loader
{
    public class ModerUIBootstrapper: BasicBootstrapper
    {        
        /// <summary>
        /// 确认MEF导入目录
        /// </summary>
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            // 添加ThinkMore.Common.Infrastructure程序集
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(BasicBootstrapper).Assembly));

            // 添加ThinkMore.Startup.Loader程序集,即当前程序集
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModerUIBootstrapper).Assembly));

            // 添加ThinkMore.Basic.Common程序集
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ICardService).Assembly));

            //添加主题目录
            DirectoryCatalog catalog = new DirectoryCatalog("Themes");
            this.AggregateCatalog.Catalogs.Add(catalog);

            //添加协同程序集
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(System.Reflection.Assembly.LoadFrom("ThinkMore.Cooperation.dll")));
        }
    }
}
