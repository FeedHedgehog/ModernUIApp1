using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.WPF.Delegate
{
    /// <summary>
    /// 公共的委托事件
    /// </summary>
    public class DelegateClass
    {
        /// <summary>
        /// 传递3DViewer实例对象
        /// </summary>
        /// <param name="viewer"></param>
        public delegate void BIM3DViewerLoadFileEnd(object viewer);

        public static event BIM3DViewerLoadFileEnd BIM3DViewerLoadFileEndEvent;

        public static void OnBIM3DViewerLoadFileEndEvent(object viewer)
        {
            BIM3DViewerLoadFileEndEvent?.Invoke(viewer);
        }
    }
}
