namespace ModernUI.Common.Infrastructure.Export
{
    using System;
    using System.ComponentModel.Composition;
    using System.Windows;

    using Microsoft.Practices.Prism.Regions;

    /// <summary>
    /// 自动定位导出视图的行为
    /// </summary>
    /// <remarks>
    /// 该类在BootStrapper的ConfigureDefaultRegionBehaviors中注册,在每个Region关联的Adapter初始化时调用
    /// </remarks>
    [Export(typeof(AutoPopulateExportedViewsBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPopulateExportedViewsBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {
        /// <summary>
        /// Gets or sets the registered views.
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        public Lazy<FrameworkElement, IViewRegionRegistration>[] RegisteredViews { get; set; }

        /// <summary>
        /// when MEF finshed import
        /// </summary>
        public void OnImportsSatisfied()
        {
            this.AddRegisteredViews();
        }

        /// <summary>
        /// when attached
        /// </summary>
        protected override void OnAttach()
        {
            this.AddRegisteredViews();
        }

         /// <summary>
        /// Auto register region.The add registered views.
        /// </summary>
        private void AddRegisteredViews()
        {
            if (this.Region != null)
            {
                foreach (var viewEntry in this.RegisteredViews)
                {
                    if (viewEntry.Metadata.RegionName == this.Region.Name)
                    {
                        var view = viewEntry.Value;

                        if (!this.Region.Views.Contains(view))
                        {
                            this.Region.Add(view);
                        }
                    }
                }
            }
        }
    }
}
