namespace CSMarkWinForms.Forms
{
    partial class ResultsOverview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultsOverview));
            this.singleOverall = new System.Windows.Forms.Label();
            this.multiOverall = new System.Windows.Forms.Label();
            this.processorThreadCount = new System.Windows.Forms.Label();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.version = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.multiScoreResult = new System.Windows.Forms.Label();
            this.singleScoreResult = new System.Windows.Forms.Label();
            this.getResultsBreakdownBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // singleOverall
            // 
            this.singleOverall.AutoSize = true;
            this.singleOverall.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleOverall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(244)))), ((int)(((byte)(66)))));
            this.singleOverall.Location = new System.Drawing.Point(24, 156);
            this.singleOverall.Name = "singleOverall";
            this.singleOverall.Size = new System.Drawing.Size(267, 32);
            this.singleOverall.TabIndex = 0;
            this.singleOverall.Text = "Single Threaded Score: ";
            // 
            // multiOverall
            // 
            this.multiOverall.AutoSize = true;
            this.multiOverall.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiOverall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(244)))), ((int)(((byte)(66)))));
            this.multiOverall.Location = new System.Drawing.Point(24, 228);
            this.multiOverall.Name = "multiOverall";
            this.multiOverall.Size = new System.Drawing.Size(257, 32);
            this.multiOverall.TabIndex = 1;
            this.multiOverall.Text = "Multi Threaded Score: ";
            // 
            // processorThreadCount
            // 
            this.processorThreadCount.AutoSize = true;
            this.processorThreadCount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processorThreadCount.Location = new System.Drawing.Point(25, 301);
            this.processorThreadCount.Name = "processorThreadCount";
            this.processorThreadCount.Size = new System.Drawing.Size(136, 25);
            this.processorThreadCount.TabIndex = 2;
            this.processorThreadCount.Text = "Thread Count: ";
            // 
            // logoBox
            // 
            this.logoBox.Image = ((System.Drawing.Image)(resources.GetObject("logoBox.Image")));
            this.logoBox.Location = new System.Drawing.Point(30, 27);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(64, 66);
            this.logoBox.TabIndex = 39;
            this.logoBox.TabStop = false;
            // 
            // version
            // 
            this.version.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.version.AutoSize = true;
            this.version.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.version.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version.ForeColor = System.Drawing.Color.White;
            this.version.Location = new System.Drawing.Point(112, 78);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(96, 25);
            this.version.TabIndex = 38;
            this.version.Text = "Label Size";
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.AutoSize = true;
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.title.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(109, 27);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(111, 32);
            this.title.TabIndex = 37;
            this.title.Text = "Title Size";
            // 
            // multiScoreResult
            // 
            this.multiScoreResult.AutoSize = true;
            this.multiScoreResult.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiScoreResult.ForeColor = System.Drawing.Color.White;
            this.multiScoreResult.Location = new System.Drawing.Point(297, 228);
            this.multiScoreResult.Name = "multiScoreResult";
            this.multiScoreResult.Size = new System.Drawing.Size(257, 32);
            this.multiScoreResult.TabIndex = 42;
            this.multiScoreResult.Text = "Multi Threaded Score: ";
            // 
            // singleScoreResult
            // 
            this.singleScoreResult.AutoSize = true;
            this.singleScoreResult.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleScoreResult.ForeColor = System.Drawing.Color.White;
            this.singleScoreResult.Location = new System.Drawing.Point(297, 156);
            this.singleScoreResult.Name = "singleScoreResult";
            this.singleScoreResult.Size = new System.Drawing.Size(267, 32);
            this.singleScoreResult.TabIndex = 41;
            this.singleScoreResult.Text = "Single Threaded Score: ";
            // 
            // getResultsBreakdownBtn
            // 
            this.getResultsBreakdownBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.getResultsBreakdownBtn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getResultsBreakdownBtn.ForeColor = System.Drawing.Color.White;
            this.getResultsBreakdownBtn.Location = new System.Drawing.Point(259, 346);
            this.getResultsBreakdownBtn.Name = "getResultsBreakdownBtn";
            this.getResultsBreakdownBtn.Size = new System.Drawing.Size(252, 45);
            this.getResultsBreakdownBtn.TabIndex = 66;
            this.getResultsBreakdownBtn.Text = "See Results Breakdown";
            this.getResultsBreakdownBtn.UseVisualStyleBackColor = false;
            this.getResultsBreakdownBtn.Click += new System.EventHandler(this.getResultsBreakdownBtn_Click);
            // 
            // ResultsOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(816, 403);
            this.Controls.Add(this.getResultsBreakdownBtn);
            this.Controls.Add(this.multiScoreResult);
            this.Controls.Add(this.singleScoreResult);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.version);
            this.Controls.Add(this.title);
            this.Controls.Add(this.processorThreadCount);
            this.Controls.Add(this.multiOverall);
            this.Controls.Add(this.singleOverall);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ResultsOverview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSMarkDesktop Benchmark Results Overview";
            this.Load += new System.EventHandler(this.ResultsOverview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label singleOverall;
        private System.Windows.Forms.Label multiOverall;
        private System.Windows.Forms.Label processorThreadCount;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label version;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label multiScoreResult;
        private System.Windows.Forms.Label singleScoreResult;
        private System.Windows.Forms.Button getResultsBreakdownBtn;
    }
}