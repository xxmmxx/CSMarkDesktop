using CSMarkWinForms.Settings;
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
        public ResultsOverview(string AppVersion, string ContributionLevel){
            InitializeComponent();
            title.Text = ProductName;
            version.Text = AppVersion;
            contributionStatus.Text = ContributionLevel;
        }
        private void ResultsOverview_Load(object sender, EventArgs e){
            processorThreadCount.Text = "Thread Count: " + Environment.ProcessorCount.ToString();
            CSMarkLib.Result x = Results.Default.BenchmarkResult;
            singleScoreResult.Text = x.OverallSingle.ToString();
            multiScoreResult.Text = x.OverallMulti.ToString();
        }
    }
}