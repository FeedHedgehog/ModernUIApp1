using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModernUI.Common.WPF.Delegate;

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
            //DelegateClass.OnBIM3DViewerLoadFileEndEvent(axBIM3DViewer1);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {                
                axBIM3DViewer1.OpenFile(ofd.FileName);
            }
        }
    }
}
