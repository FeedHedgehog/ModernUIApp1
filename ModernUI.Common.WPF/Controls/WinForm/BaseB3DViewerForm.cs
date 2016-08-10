using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModernUI.Common.WPF.Controls.WinForm
{
    public partial class BaseB3DViewerForm : UserControl
    {

        public BaseB3DViewerForm()
        {
            InitializeComponent();            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            axBIM3DViewer1.AppendFile(@"‪D:\2016\package\projects\CodeRepository\EBIMWorks 0.5\Project1\测试模型\杭州地铁5号线_通惠路站.b3d");
            axBIM3DViewer1.LoadFileEnd += AxBIM3DViewer1_LoadFileEnd;
        }

        private void AxBIM3DViewer1_LoadFileEnd(object sender, AxBIM3DViewerLib._DBIM3DViewerEvents_LoadFileEndEvent e)
        {
            MessageBox.Show("aa");
        }
    }
}
