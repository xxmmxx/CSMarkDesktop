namespace CSMarkWinForms.Forms.Upgrade
{
    partial class PrimeOverview
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
            this.getPrimeBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // getPrimeBtn
            // 
            this.getPrimeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.getPrimeBtn.Font = new System.Drawing.Font("Bahnschrift Condensed", 20.25F);
            this.getPrimeBtn.ForeColor = System.Drawing.Color.RoyalBlue;
            this.getPrimeBtn.Location = new System.Drawing.Point(282, 274);
            this.getPrimeBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.getPrimeBtn.Name = "getPrimeBtn";
            this.getPrimeBtn.Size = new System.Drawing.Size(198, 59);
            this.getPrimeBtn.TabIndex = 42;
            this.getPrimeBtn.Text = "Get Prime";
            this.getPrimeBtn.UseVisualStyleBackColor = false;
            this.getPrimeBtn.Click += new System.EventHandler(this.getPrimeBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(728, 150);
            this.label1.TabIndex = 43;
            this.label1.Text = "Support CSMark\'s ongoing development with CSMark Prime and get a cool\r\n PRIME bad" +
    "ge in the app.\r\n\r\nCSMark Prime is completely optional and contains all the funct" +
    "ionality of the \r\nfree version.";
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
            this.Controls.Add(this.getPrimeBtn);
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

        private System.Windows.Forms.Button getPrimeBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label priceLabel;
    }
}