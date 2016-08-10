namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axBIM3DViewer1 = new AxBIM3DViewerLib.AxBIM3DViewer();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axBIM3DViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axBIM3DViewer1
            // 
            this.axBIM3DViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axBIM3DViewer1.Enabled = true;
            this.axBIM3DViewer1.Location = new System.Drawing.Point(0, 0);
            this.axBIM3DViewer1.Name = "axBIM3DViewer1";
            this.axBIM3DViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axBIM3DViewer1.OcxState")));
            this.axBIM3DViewer1.Size = new System.Drawing.Size(514, 409);
            this.axBIM3DViewer1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(329, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 409);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.axBIM3DViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axBIM3DViewer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxBIM3DViewerLib.AxBIM3DViewer axBIM3DViewer1;
        private System.Windows.Forms.Button button1;
    }
}

