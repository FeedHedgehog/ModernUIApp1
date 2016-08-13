using AxBIM3DViewerLib;
using ModernUI.Common.WPF.Delegate;
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

namespace ModernUI.Common.WPF.Controls
{
    /// <summary>
    /// B3DViewerRegion.xaml 的交互逻辑
    /// </summary>
    public partial class B3DViewerRegion : UserControl
    {
        private AxBIM3DViewer _bimViewer;
        public B3DViewerRegion()
        {
            DelegateClass.BIM3DViewerLoadFileEndEvent += DelegateClass_BIM3DViewerLoadFileEndEvent;
            InitializeComponent();
        }

        private void DelegateClass_BIM3DViewerLoadFileEndEvent(object viewer)
        {
            _bimViewer = viewer as AxBIM3DViewer;
        }

    }
}
