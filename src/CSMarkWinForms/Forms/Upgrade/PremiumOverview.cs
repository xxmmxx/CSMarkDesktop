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

        private void PrimeOverview_Load(object sender, EventArgs e){
            management.IsAPremiumUser();
       //     var premium12price = management.GetPremium12monthsprice().FormattedBasePrice + " " + management.GetPremium12monthsprice().CurrencyCode;
          //  var premium6price = management.GetPremium6monthsprice();
         //   var premium3price = management.GetPremium12monthsprice();


            //      getpremium12monthsBtn.Text += 
        }
        private void getpremium12monthsBtn_Click(object sender, EventArgs e){
            management.Purchase12MonthPremium();
        }
        private void getpremium6monthsBtn_Click(object sender, EventArgs e){
            management.Purchase6MonthPremium();
        }
        private void getpremium3monthsBtn_Click(object sender, EventArgs e){           
            management.Purchase3MonthPremium();
        }

        private void button1_Click(object sender, EventArgs e){
            Close();
        }
    }
}