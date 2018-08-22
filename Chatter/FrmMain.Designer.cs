namespace 碎碎念
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnStart = new System.Windows.Forms.Button();
            this.txtText = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sddChapter = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.sslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnLastChapter = new System.Windows.Forms.Button();
            this.btnNextChapter = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStart.Location = new System.Drawing.Point(111, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(40, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "朗读";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtText
            // 
            this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtText.BackColor = System.Drawing.SystemColors.Window;
            this.txtText.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtText.Location = new System.Drawing.Point(0, 0);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.ReadOnly = true;
            this.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtText.Size = new System.Drawing.Size(384, 189);
            this.txtText.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sddChapter,
            this.tsProgress,
            this.sslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 188);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(384, 23);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sddChapter
            // 
            this.sddChapter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sddChapter.Image = ((System.Drawing.Image)(resources.GetObject("sddChapter.Image")));
            this.sddChapter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sddChapter.Name = "sddChapter";
            this.sddChapter.ShowDropDownArrow = false;
            this.sddChapter.Size = new System.Drawing.Size(20, 21);
            this.sddChapter.Text = "toolStripDropDownButton1";
            this.sddChapter.Click += new System.EventHandler(this.sddChapter_Click);
            // 
            // tsProgress
            // 
            this.tsProgress.Name = "tsProgress";
            this.tsProgress.Size = new System.Drawing.Size(100, 17);
            // 
            // sslStatus
            // 
            this.sslStatus.BackColor = System.Drawing.SystemColors.Control;
            this.sslStatus.Name = "sslStatus";
            this.sslStatus.Size = new System.Drawing.Size(0, 18);
            // 
            // btnLastChapter
            // 
            this.btnLastChapter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLastChapter.Location = new System.Drawing.Point(3, 3);
            this.btnLastChapter.Name = "btnLastChapter";
            this.btnLastChapter.Size = new System.Drawing.Size(50, 23);
            this.btnLastChapter.TabIndex = 3;
            this.btnLastChapter.Text = "上一章";
            this.btnLastChapter.UseVisualStyleBackColor = true;
            this.btnLastChapter.Click += new System.EventHandler(this.btnLastChapter_Click);
            // 
            // btnNextChapter
            // 
            this.btnNextChapter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNextChapter.Location = new System.Drawing.Point(209, 3);
            this.btnNextChapter.Name = "btnNextChapter";
            this.btnNextChapter.Size = new System.Drawing.Size(50, 23);
            this.btnNextChapter.TabIndex = 3;
            this.btnNextChapter.Text = "下一章";
            this.btnNextChapter.UseVisualStyleBackColor = true;
            this.btnNextChapter.Click += new System.EventHandler(this.btnNextChapter_Click);
            // 
            // btnLast
            // 
            this.btnLast.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLast.Location = new System.Drawing.Point(57, 3);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(50, 23);
            this.btnLast.TabIndex = 4;
            this.btnLast.Text = "上一页";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNext.Location = new System.Drawing.Point(155, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Controls.Add(this.btnLastChapter);
            this.panel1.Controls.Add(this.btnNextChapter);
            this.panel1.Location = new System.Drawing.Point(51, 155);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 30);
            this.panel1.TabIndex = 6;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "碎碎念";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sslStatus;
        private System.Windows.Forms.ToolStripProgressBar tsProgress;
        private System.Windows.Forms.ToolStripDropDownButton sddChapter;
        private System.Windows.Forms.Button btnLastChapter;
        private System.Windows.Forms.Button btnNextChapter;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Panel panel1;
    }
}