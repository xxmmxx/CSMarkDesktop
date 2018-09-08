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
using Windows.Services.Store;

namespace CSMarkWinForms.Forms.Upgrade{
    public partial class PrimeOverview : Form{
        SubscriptionWrapper subWrapper = new SubscriptionWrapper();

        public PrimeOverview(){
            InitializeComponent();
        }
        private void getPrimeBtn_Click(object sender, EventArgs e){
            subWrapper.SetupSubscriptionInfoAsync();
            subWrapper.PurchaseAsync();
        }

        private void PrimeOverview_Load(object sender, EventArgs e)
        {
        }
    }
}