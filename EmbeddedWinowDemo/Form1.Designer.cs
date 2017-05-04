namespace EmbeddedWinowDemo
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
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.embeddedPanel1 = new EmbeddedWinow.EmbeddedPanel();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(59, 18);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 2;
            // 
            // embeddedPanel1
            // 
            this.embeddedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.embeddedPanel1.ExePath = "F:\\dpjia.penguin.work\\Dpjia.WoodenCustomization\\test demo\\EmbeddedWinow\\WindowChi" +
    "ld\\bin\\Debug\\WindowChild.exe";
            this.embeddedPanel1.Location = new System.Drawing.Point(0, 243);
            this.embeddedPanel1.Name = "embeddedPanel1";
            this.embeddedPanel1.Size = new System.Drawing.Size(310, 227);
            this.embeddedPanel1.TabIndex = 3;
            this.embeddedPanel1.StartCompleteEvent += new EmbeddedWinow.ExeStartCompleteEventHandle(this.embeddedPanel1_StartCompleteEvent);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 470);
            this.Controls.Add(this.embeddedPanel1);
            this.Controls.Add(this.monthCalendar1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private EmbeddedWinow.EmbeddedPanel embeddedPanel1;
    }
}

