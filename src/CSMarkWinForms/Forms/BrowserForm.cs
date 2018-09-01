using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMarkWinForms.Forms
{
    public partial class BrowserForm : Form
    {
        public BrowserForm(string WindowTitle, string url)
        {
            InitializeComponent();
            Text = WindowTitle;
            Uri uri = new Uri(url);

            try
            {
                web.Navigate(uri);
            }
            catch
            {

            }
        }

        private void BrowserForm_Load(object sender, EventArgs e)
        {

        }
    }
}
