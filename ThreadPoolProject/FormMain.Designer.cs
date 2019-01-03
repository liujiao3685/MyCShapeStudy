namespace ThreadPoolProject
{
    partial class FormMain
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnJian = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnJia = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.txtXLocation = new System.Windows.Forms.TextBox();
            this.txtWLocation = new System.Windows.Forms.TextBox();
            this.txtZLocation = new System.Windows.Forms.TextBox();
            this.txtYLocation = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnJian
            // 
            this.btnJian.Location = new System.Drawing.Point(168, 193);
            this.btnJian.Margin = new System.Windows.Forms.Padding(5);
            this.btnJian.Name = "btnJian";
            this.btnJian.Size = new System.Drawing.Size(51, 46);
            this.btnJian.TabIndex = 0;
            this.btnJian.Text = "-";
            this.btnJian.UseVisualStyleBackColor = true;
            this.btnJian.Click += new System.EventHandler(this.btnMove_Click);
            this.btnJian.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJian_MouseDown);
            this.btnJian.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJian_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 206);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnJia
            // 
            this.btnJia.Location = new System.Drawing.Point(43, 193);
            this.btnJia.Margin = new System.Windows.Forms.Padding(5);
            this.btnJia.Name = "btnJia";
            this.btnJia.Size = new System.Drawing.Size(51, 46);
            this.btnJia.TabIndex = 2;
            this.btnJia.Text = "+";
            this.btnJia.UseVisualStyleBackColor = true;
            this.btnJia.Click += new System.EventHandler(this.btnMove_Click);
            this.btnJia.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJia_MouseDown);
            this.btnJia.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJia_MouseUp);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // txtXLocation
            // 
            this.txtXLocation.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtXLocation.Location = new System.Drawing.Point(60, 27);
            this.txtXLocation.Name = "txtXLocation";
            this.txtXLocation.ReadOnly = true;
            this.txtXLocation.Size = new System.Drawing.Size(148, 25);
            this.txtXLocation.TabIndex = 5;
            this.txtXLocation.Text = "0";
            this.txtXLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWLocation
            // 
            this.txtWLocation.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtWLocation.Location = new System.Drawing.Point(60, 129);
            this.txtWLocation.Name = "txtWLocation";
            this.txtWLocation.ReadOnly = true;
            this.txtWLocation.Size = new System.Drawing.Size(148, 25);
            this.txtWLocation.TabIndex = 8;
            this.txtWLocation.Text = "0";
            this.txtWLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtZLocation
            // 
            this.txtZLocation.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtZLocation.Location = new System.Drawing.Point(60, 95);
            this.txtZLocation.Name = "txtZLocation";
            this.txtZLocation.ReadOnly = true;
            this.txtZLocation.Size = new System.Drawing.Size(148, 25);
            this.txtZLocation.TabIndex = 7;
            this.txtZLocation.Text = "0";
            this.txtZLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtYLocation
            // 
            this.txtYLocation.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtYLocation.Location = new System.Drawing.Point(60, 61);
            this.txtYLocation.Name = "txtYLocation";
            this.txtYLocation.ReadOnly = true;
            this.txtYLocation.Size = new System.Drawing.Size(148, 25);
            this.txtYLocation.TabIndex = 6;
            this.txtYLocation.Text = "0";
            this.txtYLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 280);
            this.Controls.Add(this.txtXLocation);
            this.Controls.Add(this.txtWLocation);
            this.Controls.Add(this.txtZLocation);
            this.Controls.Add(this.txtYLocation);
            this.Controls.Add(this.btnJia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnJian);
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnJian;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnJia;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.TextBox txtXLocation;
        private System.Windows.Forms.TextBox txtWLocation;
        private System.Windows.Forms.TextBox txtZLocation;
        private System.Windows.Forms.TextBox txtYLocation;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

