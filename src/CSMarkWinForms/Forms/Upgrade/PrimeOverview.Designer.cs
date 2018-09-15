namespace CSMarkWinForms.Forms.Upgrade
{
    partial class PremiumOverview
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
            this.getPremiumBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // getPremiumBtn
            // 
            this.getPremiumBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.getPremiumBtn.Font = new System.Drawing.Font("Bahnschrift Condensed", 20.25F);
            this.getPremiumBtn.ForeColor = System.Drawing.Color.RoyalBlue;
            this.getPremiumBtn.Location = new System.Drawing.Point(282, 274);
            this.getPremiumBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.getPremiumBtn.Name = "getPremiumBtn";
            this.getPremiumBtn.Size = new System.Drawing.Size(198, 59);
            this.getPremiumBtn.TabIndex = 42;
            this.getPremiumBtn.Text = "Get Premium";
            this.getPremiumBtn.UseVisualStyleBackColor = false;
            this.getPremiumBtn.Click += new System.EventHandler(this.getPrimeBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(758, 150);
            this.label1.TabIndex = 43;
            this.label1.Text = "Support CSMark\'s ongoing development with CSMark Premium and get a cool\r\n PREMIUM" +
    " badge in the app.\r\n\r\nCSMark Premium is completely optional and contains all the" +
    " functionality of the \r\nfree version.";
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceLabel.ForeColor = System.Drawing.Color.White;
            this.priceLabel.Location = new System.Drawing.Point(205, 202);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(232, 21);
            this.priceLabel.TabIndex = 44;
            this.priceLabel.Text = "Support CSMark starting at just ";
            // 
            // PrimeOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(779, 339);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.getPremiumBtn);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PrimeOverview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Get CSMark Prime";
            this.Load += new System.EventHandler(this.PrimeOverview_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getPremiumBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label priceLabel;
    }
}