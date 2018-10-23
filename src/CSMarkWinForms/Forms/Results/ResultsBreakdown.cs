using CSMarkLib;
using CSMarkLib.BenchmarkManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMarkWinForms.Forms.Results{
    public partial class ResultsBreakdown : Form{

        public ResultsBreakdown(){
            InitializeComponent();

            SetupManager setup = new SetupManager(true);
            if(setup.DetermineDistributionPlatform().Equals(DistributionPlatform.WinStore){
                saveResultBtn.Enabled = false;
                saveResultBtn.Visible = false;
            }
        }


            pythagorasMulti.Text = x.PythagorasMulti.ToString();
            pythagorasSingle.Text = x.PythagorasSingle.ToString();

            arithmeticMulti.Text = x.ArithmeticSumNMulti.ToString();
            arithmeticSingle.Text = x.ArithmeticSumNSingle.ToString();

            geometricMulti.Text = x.GeometricSumNMulti.ToString();
            geometricSingle.Text = x.GeometricSumNSingle.ToString();

            compoundInterestMulti.Text = x.CompoundInterestMulti.ToString();
            compoundInterestSingle.Text = x.CompoundInterestSingle.ToString();

            nearestPrimeMulti.Text = x.NearestPrimeMulti.ToString();
            nearestPrimeSingle.Text = x.NearestPrimeSingle.ToString();

            changeReturnMulti.Text = x.ChangeReturnMulti.ToString();
            changeReturnSingle.Text = x.ChangeReturnSingle.ToString();

            rbssLiteMulti.Text = x.RbssLiteMulti.ToString();
            rbssLiteSingle.Text = x.RbssLiteSingle.ToString();
        }

        private void saveResultBtn_Click(object sender, EventArgs e){
            var btc = new BenchmarkController();
            btc.SaveToTextFile("0.32.1.0", CSMarkWinForms.Results.Default.BenchmarkResult);

            MessageBox.Show("Your results text file has been saved at: " + Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "results", "File Saved");
        }
    }
}
