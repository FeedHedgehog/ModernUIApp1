namespace ModernUI.Common.Infrastructure.Export
{
    using System;
    using System.ComponentModel.Composition;
    using System.Windows;

    /// <summary>
    /// The view export attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [MetadataAttribute]
    public sealed class ViewExportAttribute : ExportAttribute, IViewRegionRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewExportAttribute"/> class.
        /// </summary>
        public ViewExportAttribute()
            : base(typeof(FrameworkElement))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewExportAttribute"/> class.
        /// </summary>
        /// <param name="viewName">
        /// The view name.
        /// </param>
        public ViewExportAttribute(string viewName)
            : base(viewName, typeof(FrameworkElement))
        {
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        public string ViewName
        {
            get { return this.ContractName; }
        }

        /// <summary>
        /// Gets or sets the region name.
        /// </summary>
        public string RegionName { get; set; }
    }
}
