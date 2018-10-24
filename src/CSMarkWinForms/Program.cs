using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMarkWinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Properties.Settings.Default.hasAcceptedTermsAndPrivacy){
                Application.Run(new Main());
            }
            else{
                Application.Run(new Forms.License.TermsAndPrivacyNotice());
            }
            
        }
    }
}