using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMarkWinForms.Forms.License{
    public partial class TermsAndPrivacyNotice : Form{
        public TermsAndPrivacyNotice(){
            InitializeComponent();
        }

        private void agree_CheckedChanged(object sender, EventArgs e){
            Properties.Settings.Default.hasAcceptedTermsAndPrivacy = agree.Checked;
            Properties.Settings.Default.Save();

            Application.Restart();
        }
    }
}