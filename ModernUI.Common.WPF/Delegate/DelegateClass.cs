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
        public delegate void BIM3DViewerLoadFileBegin(object viewer);

        public static event BIM3DViewerLoadFileBegin BIM3DViewerLoadFileBeginEvent;

        public static void OnBIM3DViewerLoadFileBeginEvent(object viewer)
        {
            BIM3DViewerLoadFileBeginEvent?.Invoke(viewer);
        }
    }
}
