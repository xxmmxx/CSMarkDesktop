using AluminiumCoreLib.Utilities;
using AutoUpdaterDotNET;
using CSMarkLib;
using CSMarkLib.BenchmarkManagement;
using CSMarkWinForms.Forms;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

/// Using Namespaces to enable Subscriptions
using Windows.Services.Store;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
///
using CSMarkWinForms.UWP.Patronage;

using CSMark.Desktop.Common;

namespace CSMarkWinForms{
    public partial class Main : Form{

        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }

        BenchmarkController btc;
        StressTestController stc = new StressTestController();

        private bool runningStressTest = false;
        private DateTime start;
        private Timer t;
        private Platform platform;

        private IAPManagement management;

        private SetupManager setup = new SetupManager(true);
        private BenchmarkManager benchmark = new BenchmarkManager();
        ContributorLevel level = ContributorLevel.Free;
        bool UseBetaChannel = false;

        public Main(){
            InitializeComponent();

            Assembly assembly = Assembly.GetEntryAssembly();
            platform = new Platform();
            btc = new BenchmarkController();
            getPremiumBtn.Visible = true;

            if (!setup.OSCompatibilityCheck()){
                throw new Exception("Your are running an old version of Windows which CSMark Doesn't support. Please update to Windows 10 Version 1709 or newer.");
            }           

            if (setup.DetermineDistributionPlatform().Equals(DistributionPlatform.GitRepository)){
                setup.CheckForUpdate(UseBetaChannel);
            }
            management = new IAPManagement();
            management.IsAPremiumUser();    
            DetermineContributorLevel();
        }

        #region Handle Stress Test and Benchmarks
        private void HandleStressTest(){
            if (runningStressTest != true){
                startBenchmarkBtn.Enabled = false;
                runningStressTest = true;                 
                //Create the dispatcher timer so we can see how long a stress test runs for.
                t = new Timer();
                t.Tick += TimerTickEvent;
                start = DateTime.Now;
                //Reset the stressTimer label.
                //    stressTimer.Content = "";
                startStressTestBtn.BackColor = Color.Red;
                startStressTestBtn.Text = "Stop Stress Test";             
            }
            else{
                runningStressTest = false;
                startBenchmarkBtn.Enabled = true;
                startStressTestBtn.BackColor = Color.Green;
                startStressTestBtn.Text = "Start Stress Test";
            }

            benchmark.HandleStressTest(runningStressTest);
        }
        private void StartBenchmark(){
            try{
               Results.Default.BenchmarkResult = benchmark.StartBenchmark();

                startBenchmarkBtn.Invoke(new Action(() => { startBenchmarkBtn.Enabled = true; }));
                startBenchmarkBtn.Invoke(new Action(() => { startBenchmarkBtn.Text = "Start Benchmark"; }));
                startBenchmarkBtn.Invoke(new Action(() => { startStressTestBtn.Enabled = true; }));

                ResultsOverview ro = new ResultsOverview(version.Text);

                Results.Default.Save();

                Task t1 = new Task(() => ro.ShowDialog());
                t1.Start();
            }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
            }
        }

        private void TimerTickEvent(object sender, EventArgs e){
            if (runningStressTest){
                //stressTimer.Content = Convert.ToString(DateTime.Now - start);
            }
        }
        #endregion
        #region Utility Misc stuff
        /// <summary>
        /// Determine what level of contribution the current user is at.
        /// </summary>
        private ContributorLevel DetermineContributorLevel(){
            if (management.IsActiveIAP()){
                level = ContributorLevel.StorePremium;
                expiryLabel.Text = management.GetIAPExpirationDate();
            }           

            if (level.Equals(ContributorLevel.Free)){
                contributionStatus.Text = "FREE";
                contributionStatus.ForeColor = Color.Lime;
                getPremiumBtn.Visible = true;
                expiryLabel.Text = "Expires: Never";
            }
            else if (level.Equals(ContributorLevel.PatronPremium) || level.Equals(ContributorLevel.StorePremium)){
                contributionStatus.Text = "PREMIUM EDITION";
                contributionStatus.ForeColor = Color.Goldenrod;
                getPremiumBtn.Visible = false;
                expiryLabel.Text = "Expires: " + management.GetIAPExpirationDate();
            }
            else if (level.Equals(ContributorLevel.PatronPro)){
                contributionStatus.Text = "PRO";
                contributionStatus.ForeColor = Color.OrangeRed;
                getPremiumBtn.Visible = false;
            }
            else if (level.Equals(ContributorLevel.PatronSponsor)){
                contributionStatus.Text = "SPONSOR";
                contributionStatus.ForeColor = Color.Goldenrod;
                ///
                getPremiumBtn.Visible = false;
            }

            //temporarily disable expiry date
            expiryLabel.Visible = false;

            return level;
        }

        private void DoScaling()
        {
            title.Font = new Font("Segoe UI", 18);
            version.Font = new Font("Segoe UI", 18);

       //     settingsButton.Font = new Font("Segoe MDL2 Assets", 20);
        //    translateButton.Font = new Font("Segoe MDL2 Assets", 20);
            helpButton.Font = new Font("Segoe MDL2 Assets", 20);
            aboutButton.Font = new Font("Segoe MDL2 Assets", 20);

            aboutButton.Text = "\xE115";
            settingsButton.Text = "\xE115";
            //closeButton.Text = "\xE10A";
            //feedbackButton.Text = "\xE170";
            helpButton.Text = "\xE897";
            aboutButton.Text = "\xE946";
            translateButton.Text = "\xE775";
        }

        private void PositionIcons()
        {
         //       settingsButton.Location = new Point(Width - 70, Height - (Height - 5));
                aboutButton.Location = new Point(Width - 70, Height - (Height - 5));
                helpButton.Location = new Point(Width - 120, Height - (Height - 5));
          //      translateButton.Location = new Point(Width - 170, Height - (Height - 5));
                PositionButtons();
            translateButton.Hide();
            helpButton.Hide();
        }

        private void PositionButtons(){
           startBenchmarkBtn.Location = new Point((Width / 2) - 100, Height / 2);
            startStressTestBtn.Location = new Point((Width / 2) - 100, (Height / 2) + 80);
            overclockWarning.Location = new Point((Width / 2) - 200, (Height / 2) + 160);
            copyrightNotice.Location = new Point((Width / 2) - 150, (Height / 2) + 240);

            //      MessageBox.Show(Width.ToString());
            //    MessageBox.Show(Height.ToString());
        }
        
        #endregion
        private void Main_Load(object sender, EventArgs e){
            Text = ProductName;
            title.Text = ProductName;
            PositionIcons();
            DoScaling(); 
            DetermineContributorLevel();
            setup.DetermineDistributionPlatform();
            settingsButton.Visible = false;      
        }
        #region Button click handling
        private void aboutButton_Click(object sender, EventArgs e){
            Form about = new About(version.Text);
            about.ShowDialog();
        }
        private void helpButton_Click(object sender, EventArgs e){

        }
        private void translateButton_Click(object sender, EventArgs e){

        }
        private void Main_ResizeEnd(object sender, EventArgs e){
            PositionIcons();
        }
        private void Main_Resize(object sender, EventArgs e){
            PositionIcons();
        }
        private void startBenchmarkBtn_Click(object sender, EventArgs e){
            startStressTestBtn.Enabled = false;
            startBenchmarkBtn.Text = "Starting Benchmark...";
            startBenchmarkBtn.Enabled = false;
            StartBenchmark();
            // var task = new Task(() => StartBenchmark());
           // task.Start();
        }
        private void startStressBtn_Click(object sender, EventArgs e){
            HandleStressTest();
        }
        #endregion

        private void getPremiumBtn_Click(object sender, EventArgs e){
            Form Premiumform = new Forms.Upgrade.PremiumOverview();
            Premiumform.ShowDialog();
        }
        private void Main_Enter(object sender, EventArgs e)
        {

        }
    }
}