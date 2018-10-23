using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSMarkWinForms.Forms{
    public partial class ResultsOverview : Form{
        public ResultsOverview(string AppVersion){
            InitializeComponent();
            title.Text = ProductName;
            version.Text = AppVersion;
        }
        private void ResultsOverview_Load(object sender, EventArgs e){
            try{
                processorThreadCount.Text = "Thread Count: " + Environment.ProcessorCount.ToString();
                CSMarkLib.Result x = CSMarkWinForms.Results.Default.BenchmarkResult;
                singleScoreResult.Text = x.OverallSingle.ToString();
                multiScoreResult.Text = x.OverallMulti.ToString();
            }
            catch(Exception ex){
                throw new Exception(ex.ToString());
            } 
        }

        private void getResultsBreakdownBtn_Click(object sender, EventArgs e){
            Form resultsBreakdown = new Results.ResultsBreakdown();
            resultsBreakdown.ShowDialog();
        }
    }
}