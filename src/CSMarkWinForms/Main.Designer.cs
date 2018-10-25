namespace CSMarkWinForms
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.aboutButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.translateButton = new System.Windows.Forms.Button();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.version = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.startStressTestBtn = new System.Windows.Forms.Button();
            this.startBenchmarkBtn = new System.Windows.Forms.Button();
            this.contributionStatus = new System.Windows.Forms.Label();
            this.overclockWarning = new System.Windows.Forms.Label();
            this.copyrightNotice = new System.Windows.Forms.Label();
            this.getPremiumBtn = new System.Windows.Forms.Button();
            this.expiryLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // aboutButton
            // 
            this.aboutButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.aboutButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutButton.ForeColor = System.Drawing.Color.White;
            this.aboutButton.Location = new System.Drawing.Point(893, 12);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(45, 40);
            this.aboutButton.TabIndex = 30;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = false;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // helpButton
            // 
            this.helpButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.helpButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpButton.ForeColor = System.Drawing.Color.White;
            this.helpButton.Location = new System.Drawing.Point(1005, 12);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(45, 40);
            this.helpButton.TabIndex = 28;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = false;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.settingsButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsButton.ForeColor = System.Drawing.Color.White;
            this.settingsButton.Location = new System.Drawing.Point(1061, 12);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(45, 40);
            this.settingsButton.TabIndex = 27;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = false;
            // 
            // translateButton
            // 
            this.translateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.translateButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.translateButton.ForeColor = System.Drawing.Color.White;
            this.translateButton.Location = new System.Drawing.Point(949, 12);
            this.translateButton.Name = "translateButton";
            this.translateButton.Size = new System.Drawing.Size(45, 40);
            this.translateButton.TabIndex = 24;
            this.translateButton.Text = "Translate";
            this.translateButton.UseVisualStyleBackColor = false;
            this.translateButton.Click += new System.EventHandler(this.translateButton_Click);
            // 
            // logoBox
            // 
            this.logoBox.Image = ((System.Drawing.Image)(resources.GetObject("logoBox.Image")));
            this.logoBox.Location = new System.Drawing.Point(12, 12);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(64, 66);
            this.logoBox.TabIndex = 33;
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
            this.version.Location = new System.Drawing.Point(94, 63);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(96, 25);
            this.version.TabIndex = 32;
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
            this.title.Location = new System.Drawing.Point(91, 12);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(111, 32);
            this.title.TabIndex = 31;
            this.title.Text = "Title Size";
            // 
            // startStressTestBtn
            // 
            this.startStressTestBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.startStressTestBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.startStressTestBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startStressTestBtn.ForeColor = System.Drawing.Color.White;
            this.startStressTestBtn.Location = new System.Drawing.Point(446, 426);
            this.startStressTestBtn.Name = "startStressTestBtn";
            this.startStressTestBtn.Size = new System.Drawing.Size(217, 53);
            this.startStressTestBtn.TabIndex = 35;
            this.startStressTestBtn.Text = "Start Stress Test";
            this.startStressTestBtn.UseVisualStyleBackColor = false;
            this.startStressTestBtn.Click += new System.EventHandler(this.startStressBtn_Click);
            // 
            // startBenchmarkBtn
            // 
            this.startBenchmarkBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.startBenchmarkBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.startBenchmarkBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startBenchmarkBtn.ForeColor = System.Drawing.Color.White;
            this.startBenchmarkBtn.Location = new System.Drawing.Point(446, 349);
            this.startBenchmarkBtn.Name = "startBenchmarkBtn";
            this.startBenchmarkBtn.Size = new System.Drawing.Size(217, 53);
            this.startBenchmarkBtn.TabIndex = 34;
            this.startBenchmarkBtn.Text = "Start Benchmark";
            this.startBenchmarkBtn.UseVisualStyleBackColor = false;
            this.startBenchmarkBtn.Click += new System.EventHandler(this.startBenchmarkBtn_Click);
            // 
            // contributionStatus
            // 
            this.contributionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contributionStatus.AutoSize = true;
            this.contributionStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.contributionStatus.Font = new System.Drawing.Font("Bahnschrift Condensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contributionStatus.ForeColor = System.Drawing.Color.Lime;
            this.contributionStatus.Location = new System.Drawing.Point(277, 15);
            this.contributionStatus.Name = "contributionStatus";
            this.contributionStatus.Size = new System.Drawing.Size(57, 33);
            this.contributionStatus.TabIndex = 36;
            this.contributionStatus.Text = "FREE";
            // 
            // overclockWarning
            // 
            this.overclockWarning.AutoSize = true;
            this.overclockWarning.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overclockWarning.ForeColor = System.Drawing.Color.White;
            this.overclockWarning.Location = new System.Drawing.Point(345, 504);
            this.overclockWarning.Name = "overclockWarning";
            this.overclockWarning.Size = new System.Drawing.Size(451, 51);
            this.overclockWarning.TabIndex = 37;
            this.overclockWarning.Text = "Warning: Overclocking your CPU may damage it and/or void your warranty. \r\n       " +
    "                          Do so at your own risk. \r\nFor more information, visit " +
    "your CPU manufacturer\'s website.";
            // 
            // copyrightNotice
            // 
            this.copyrightNotice.AutoSize = true;
            this.copyrightNotice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyrightNotice.ForeColor = System.Drawing.Color.White;
            this.copyrightNotice.Location = new System.Drawing.Point(458, 569);
            this.copyrightNotice.Name = "copyrightNotice";
            this.copyrightNotice.Size = new System.Drawing.Size(205, 17);
            this.copyrightNotice.TabIndex = 38;
            this.copyrightNotice.Text = "Copyright (c) AluminiumTech 2018";
            // 
            // getPremiumBtn
            // 
            this.getPremiumBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.getPremiumBtn.Font = new System.Drawing.Font("Bahnschrift Condensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getPremiumBtn.ForeColor = System.Drawing.Color.RoyalBlue;
            this.getPremiumBtn.Location = new System.Drawing.Point(379, 12);
            this.getPremiumBtn.Name = "getPremiumBtn";
            this.getPremiumBtn.Size = new System.Drawing.Size(170, 45);
            this.getPremiumBtn.TabIndex = 41;
            this.getPremiumBtn.Text = "Get Premium";
            this.getPremiumBtn.UseVisualStyleBackColor = false;
            this.getPremiumBtn.Click += new System.EventHandler(this.getPremiumBtn_Click);
            // 
            // expiryLabel
            // 
            this.expiryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expiryLabel.AutoSize = true;
            this.expiryLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.expiryLabel.Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expiryLabel.ForeColor = System.Drawing.Color.White;
            this.expiryLabel.Location = new System.Drawing.Point(278, 63);
            this.expiryLabel.Name = "expiryLabel";
            this.expiryLabel.Size = new System.Drawing.Size(78, 23);
            this.expiryLabel.TabIndex = 42;
            this.expiryLabel.Text = "Expires:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(1116, 611);
            this.Controls.Add(this.expiryLabel);
            this.Controls.Add(this.getPremiumBtn);
            this.Controls.Add(this.copyrightNotice);
            this.Controls.Add(this.overclockWarning);
            this.Controls.Add(this.contributionStatus);
            this.Controls.Add(this.startStressTestBtn);
            this.Controls.Add(this.startBenchmarkBtn);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.version);
            this.Controls.Add(this.title);
            this.Controls.Add(this.aboutButton);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.translateButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
            this.Enter += new System.EventHandler(this.Main_Enter);
            this.Resize += new System.EventHandler(this.Main_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button translateButton;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label version;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button startStressTestBtn;
        private System.Windows.Forms.Button startBenchmarkBtn;
        private System.Windows.Forms.Label contributionStatus;
        private System.Windows.Forms.Label overclockWarning;
        private System.Windows.Forms.Label copyrightNotice;
        private System.Windows.Forms.Button getPremiumBtn;
        private System.Windows.Forms.Label expiryLabel;
    }
}