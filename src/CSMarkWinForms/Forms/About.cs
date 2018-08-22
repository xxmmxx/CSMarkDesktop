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
    public partial class About : Form
    {
        public About(string AppVersion)
        {
            InitializeComponent();
            version.Text = "App Version: " + AppVersion;
        }

        private void About_Load(object sender, EventArgs e)
        {

        }
    }
}
