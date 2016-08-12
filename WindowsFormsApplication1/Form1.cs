using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            axBIM3DViewer1.LoadFileEnd += AxBIM3DViewer1_LoadFileEnd; 
        }

        private void AxBIM3DViewer1_LoadFileEnd(object sender, AxBIM3DViewerLib._DBIM3DViewerEvents_LoadFileEndEvent e)
        {
            MessageBox.Show("asd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd=new OpenFileDialog();
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                axBIM3DViewer1.OpenFile(ofd.FileName);
            }           
        }
    }
}
