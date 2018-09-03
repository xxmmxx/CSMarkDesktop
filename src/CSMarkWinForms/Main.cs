using AluminiumCoreLib.Utilities;
using AutoUpdaterDotNET;
using CSMarkLib;
using CSMarkLib.BenchmarkManagement;
using CSMarkWinForms.Forms;
using CSMarkWinForms.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

/// Using Namespaces to enable Subscriptions
using Windows.Services.Store;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
///
using CSMarkWinForms.Patronage;

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

        //private string betaURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/beta.xml";
        private string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/stable.xml";
        //Supported Versions of Windows
        private Version win10v1703 = new Version(10, 0, 15063, 0);
        private Version win10v1709 = new Version(10, 0, 16299, 0);
        private Version win10v1803 = new Version(10, 0, 17134, 0);

        private bool runningStressTest = false;
        private DateTime start;
        private Timer t;
        private Platform platform;

        private enum DistributionPlatform{
            SteamStore,
            WinStore,
            GitRepository
        }

        ContributorLevel level = ContributorLevel.Free;

        SubscriptionWrapper subWrapper = new SubscriptionWrapper();

        public Main(){
            InitializeComponent();
            Assembly assembly = Assembly.GetEntryAssembly();
            platform = new Platform();
            btc = new BenchmarkController();

            if (DetermineDistributionPlatform().Equals(DistributionPlatform.GitRepository)){
                AutoUpdater.Start(stableURL);
            }
            //Check for WinStore sub         
            subWrapper.CheckIfUserHasSubscriptionAsync();

            level = subWrapper.GetContributorLevel();

            DetermineContributorLevel();           
        }

        #region Handle Stress Test and Benchmarks
        private void HandleStressTest(){
            if (runningStressTest != true){
                startBenchmarkBtn.Enabled = false;
                runningStressTest = true;
                //Start the Stress Test as a new Task to ensure good UI performance.
                var startStressTest = new Task(() => stc.StartMultiStressTest());
                startStressTest.Start();
                //Create the dispatcher timer so we can see how long a stress test runs for.
                t = new Timer();
                t.Tick += TimerTickEvent;
                start = DateTime.Now;
                //Reset the stressTimer label.
                //    stressTimer.Content = "";
            }
            else{
                runningStressTest = false;
                startBenchmarkBtn.Enabled = true;
                stc.StopStressTest();
                //  var stopStressTest = new Task(() => stc.StopStressTest());
                //    stopStressTest.Start();
                t.Stop();
            }

            ApplyStresTestButtonColors();
        }
        private void ApplyStresTestButtonColors(){
            if (runningStressTest == false)
            {
                startStressTestBtn.BackColor = Color.Green;
                startStressTestBtn.Text = "Start Stress Test";
            }
            else{
                startStressTestBtn.BackColor = Color.Red;
                startStressTestBtn.Text = "Stop Stress Test";
            }
        }
        private void StartBenchmark(){
            var benchmarkWorkTask = new Task(() => BenchmarkWork());
            benchmarkWorkTask.Start();
            benchmarkWorkTask.Wait();

            try{
                startBenchmarkBtn.Invoke(new Action(() => { startBenchmarkBtn.Enabled = true; }));
                startBenchmarkBtn.Invoke(new Action(() => { startBenchmarkBtn.Text = "Start Benchmark"; }));
                startBenchmarkBtn.Invoke(new Action(() => { startStressTestBtn.Enabled = true; }));
                startBenchmarkBtn.Invoke(new Action(() => { new ResultsOverview(version.Text, contributionStatus.Text).ShowDialog(); }));
            }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
            }
        }
        private async void BenchmarkWork(){
       //     var warmupTask = Task.Factory.StartNew(() => btc.DoWarmup());
         //   warmupTask.Wait((60) * 1000);
            var task1 = Task.Factory.StartNew(() => btc.StartSingleBenchmarkTests());
            task1.Wait((120 * 5) * 1000);
            var task2 = Task.Factory.StartNew(() => btc.StartMultiBenchmarkTests());
            task2.Wait((120 * 5) * 1000);

            /*    HashMap<BenchmarkType, CSMarkLib.BenchmarkLib.Benchmark> hash = btc.ReturnBenchmarkObjects();
                var resultSaver = new ResultSaver();
                var result = resultSaver.SaveResult(true, hash);
                */

            Results.Default.BenchmarkResult = btc.SaveResult(true, true);
        //    Results.Default.BenchmarkResult = result;
            Results.Default.Save();
        }
        private void TimerTickEvent(object sender, EventArgs e){
            if (runningStressTest){
                //stressTimer.Content = Convert.ToString(DateTime.Now - start);
            }
        }
        #endregion
        #region Handle WinStore Subs
       

   
        
        #endregion
        #region Utility Misc stuff

        /// <summary>
        /// Determine what distribution platform CSMark has come from.
        /// </summary>
        private DistributionPlatform DetermineDistributionPlatform(){
            DistributionPlatform distribution = DistributionPlatform.GitRepository;

            string currentDirectory = Environment.CurrentDirectory;

            if (currentDirectory.Contains("16188AluminiumTech.CSMark_20gejd9zdp9ny")){
                distribution = DistributionPlatform.WinStore;
                Text += " Windows Store Version";
            }
            else{
                distribution = DistributionPlatform.GitRepository;
                getPrimeBtn.Enabled = false;
                getPrimeBtn.Visible = false;
            }
            //Store Testing: 
            getPrimeBtn.Enabled = true;
            getPrimeBtn.Visible = true;

            return distribution;
        }
        /// <summary>
        /// Determine what level of contribution the current user is at.
        /// </summary>
        private ContributorLevel DetermineContributorLevel(){ 
            if (level.Equals(ContributorLevel.Free)){
                contributionStatus.Text = "FREE";
                contributionStatus.ForeColor = Color.Lime;
                getPrimeBtn.Visible = true;
            }
            else if (level.Equals(ContributorLevel.PatronPrime)){
                contributionStatus.Text = "PRIME";
                contributionStatus.ForeColor = Color.RoyalBlue;
                //  getPrimeBtn.Text = "Get PRO";
                //getPrimeBtn.ForeColor = Color.OrangeRed;
                getPrimeBtn.Visible = false;
            }
            else if (level.Equals(ContributorLevel.PatronPro)){
                contributionStatus.Text = "PRO";
                contributionStatus.ForeColor = Color.OrangeRed;
                ///
                getPrimeBtn.Visible = false;
            }
            else if (level.Equals(ContributorLevel.PatronSponsor)){
                contributionStatus.Text = "SPONSOR";
                contributionStatus.ForeColor = Color.Goldenrod;
                ///
                getPrimeBtn.Visible = false;
            }
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

        private string FormatDateVersion()
        {
            string major = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
            string minor = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            string build = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
            string revision = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();

            version.Text = version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString() + "." + revision.ToString();

            string existingString = "";
            string newString = "0";

            //Fix the date version format for the minor version
            if (minor.Length.Equals(1))
            {
                existingString = minor;
                newString = newString + existingString;
                minor = newString;
            }

            ///Smart auto correcting version syste,.
             if (build.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString();
            }
            else if (revision.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString();
            }
            else if (!revision.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString() + "." + revision.ToString();
            }

            if (major.Equals("0")){
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString();
            }

            return version.Text;
        }

        private string FormatFriendlyVersion()
        {
            string major = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
            string minor = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            string build = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
            string revision = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();

            ///Smart auto correcting version syste,.
            if (build.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString();
            }
            else if (revision.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString();
            }
            else if (!revision.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString() + "." + revision.ToString();
            }

            if (major.Equals("0"))
            {
                version.Text = major.ToString() + "." + minor.ToString() + "." + build.ToString();
            }

            return version.Text;
        }
        #endregion
        private void Main_Load(object sender, EventArgs e){
            Text = ProductName;
            title.Text = ProductName;
            PositionIcons();
            DoScaling(); 
            FormatFriendlyVersion();
            DetermineContributorLevel();
            DetermineDistributionPlatform();
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
            var task = new Task(() => StartBenchmark());
            task.Start();
        }
        private void startStressBtn_Click(object sender, EventArgs e){
            HandleStressTest();
        }
        #endregion

        private void getPrimeBtn_Click(object sender, EventArgs e){
            try
            {
                subWrapper.SetupSubscriptionInfoAsync();
                subWrapper.PurchaseAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void Main_Enter(object sender, EventArgs e)
        {
            if (subWrapper.GetContributorLevel() == ContributorLevel.StorePrime)
            {
                getPrimeBtn.Enabled = false;
                getPrimeBtn.Visible = false;
                DetermineContributorLevel();
            }
        }
    }
}