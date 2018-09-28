using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CSMarkWinForms.Patronage;
using CSMarkWinForms.UWP.Patronage;
using Windows.Services.Store;

namespace CSMarkWinForms.Forms.Upgrade{
    public partial class PremiumOverview : Form{
        IAPManagement management;

        public PremiumOverview(){
            InitializeComponent();
            management = new IAPManagement();
        }
        private void getPrimeBtn_Click(object sender, EventArgs e){
        
        }

        private void PrimeOverview_Load(object sender, EventArgs e)
        {
        }
    }
}