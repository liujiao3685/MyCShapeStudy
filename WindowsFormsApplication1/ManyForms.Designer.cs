namespace WindowsFormsApplication1
{
    partial class ManyForms
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelParent = new System.Windows.Forms.Panel();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnMontionControl = new System.Windows.Forms.Button();
            this.btnParamSet = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelParent
            // 
            this.panelParent.Location = new System.Drawing.Point(360, 34);
            this.panelParent.Name = "panelParent";
            this.panelParent.Size = new System.Drawing.Size(292, 274);
            this.panelParent.TabIndex = 0;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 381);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(342, 68);
            this.txtOutput.TabIndex = 1;
            // 
            // btnMontionControl
            // 
            this.btnMontionControl.Location = new System.Drawing.Point(438, 351);
            this.btnMontionControl.Name = "btnMontionControl";
            this.btnMontionControl.Size = new System.Drawing.Size(138, 41);
            this.btnMontionControl.TabIndex = 2;
            this.btnMontionControl.Text = "运动控制";
            this.btnMontionControl.UseVisualStyleBackColor = true;
            this.btnMontionControl.Click += new System.EventHandler(this.btnMontionControl_Click);
            // 
            // btnParamSet
            // 
            this.btnParamSet.Location = new System.Drawing.Point(438, 420);
            this.btnParamSet.Name = "btnParamSet";
            this.btnParamSet.Size = new System.Drawing.Size(138, 41);
            this.btnParamSet.TabIndex = 3;
            this.btnParamSet.Text = "参数设置";
            this.btnParamSet.UseVisualStyleBackColor = true;
            this.btnParamSet.Click += new System.EventHandler(this.btnParamSet_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(327, 296);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(319, 270);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ManyForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 475);
            this.Controls.Add(this.panelParent);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnParamSet);
            this.Controls.Add(this.btnMontionControl);
            this.Controls.Add(this.txtOutput);
            this.Name = "ManyForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManyForms";
            this.Load += new System.EventHandler(this.ManyForms_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelParent;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnMontionControl;
        private System.Windows.Forms.Button btnParamSet;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
    }
}