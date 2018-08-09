/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using System;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using AluminiumCoreLib.Utilities;
using AutoUpdaterDotNET;
using CSMarkLib;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.IO;
using CSMarkDesktop.Windows;
using CSMarkLib.Results;
using CSMarkDesktop.Windows.LauncherUI;
using CSMarkDesktop.Windows.WebUI;

namespace CSMarkDesktop{

    /// <summary>
    /// Interaction logic for MainWindow.xaml>
    public partial class MainWindow : Window{
        private SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(125, 244, 66));
        private SolidColorBrush myRedBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        private SolidColorBrush black = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));

        private CSMarkLib.UpdatingServices.AutoUpdater ac = new CSMarkLib.UpdatingServices.AutoUpdater();

        private Platform platform;
        private StressTestController stc;
        private BenchmarkController benchController;
        private DateTime start;
        private DispatcherTimer t;
        private bool runningStress = false;

        private string betaURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/beta.xml";
        private string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/stable.xml";
        //Supported Versions of Windows
        private Version win10v1703 = new Version(10, 0, 15063, 0);
        private Version win10v1709 = new Version(10, 0, 16299, 0);
        private Version win10v1803 = new Version(10, 0, 17134, 0);

        AluminiumCoreLib.Hardware.Processor cpu = new AluminiumCoreLib.Hardware.Processor();

        public enum DistributionPlatform{
            SteamStore,
            WinStore,
            GitRepository
        }        

        private string AppVersion;
        DistributionPlatform distribution;

        public MainWindow(){
            InitializeComponent();
           
            benchController = new BenchmarkController();
            Assembly assembly = Assembly.GetEntryAssembly();
            //Detect where the app came from.
            DetectStore();
            distribution = DistributionPlatform.WinStore;

            if ((Environment.OSVersion.Version != win10v1703) && (Environment.OSVersion.Version != win10v1709) && (Environment.OSVersion.Version != win10v1803)){
                    throw new Exception("Your OS is not supported. Please update your OS to use CSMark!");
            }
            
            if(distribution.Equals(DistributionPlatform.SteamStore) || distribution.Equals(DistributionPlatform.WinStore)){
                checkUpdatesMenuBtn.IsEnabled = false;
                checkUpdatesMenuBtn.Visibility = Visibility.Collapsed;                
                patronImage.Visibility = Visibility.Collapsed;
            }
            else{
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");
                AutoUpdater.LetUserSelectRemindLater = false;
                AutoUpdater.ReportErrors = true;
                AutoUpdater.ShowSkipButton = false;
                AutoUpdater.ShowRemindLaterButton = false;
                AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;
            }

            eligible.Content = "";           
            stc = new StressTestController();
            ApplyStressBtnColors();
            platform = new Platform();
            //Run processor detection in a new task before we need the information.       
            cpu.GetProcessorInformationAsTask();
            Thread.Sleep(300);
            Properties.Results.Default.Processor = cpu.CPU;
            Properties.Results.Default.CPUCoreCount = cpu.CoreCount;
            Properties.Results.Default.CPUThreadCount = cpu.ThreadCount;
            Properties.Results.Default.CPUClockSpeed = cpu.ClockspeedInt.ToString() + " MHz";
            Properties.Results.Default.Save();

            //Show the version number
            versionLabel.Content = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AppVersion = versionLabel.Content.ToString();

            if (Properties.Settings.Default.HideBecomeAPatronButton == true){
                patronImage.Visibility = Visibility.Collapsed;
            }

            LoadBackground();
        }

        private void AutoUpdater_ApplicationExitEvent(){
            Application.Current.Shutdown();
        }

        private void LoadBackground() {
            Brush foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));         

            if (Properties.Settings.Default.background.Equals("reallydark")) {
                Background = reallyDark;
            }
            if (Properties.Settings.Default.background.Equals("dark")) {
                Background = dark;
            }
            if (Properties.Settings.Default.background.Equals("justblack")) {
                Background = black;
            }

            if (!Properties.Settings.Default.background.Equals("dark") && !Properties.Settings.Default.background.Equals("justblack") && !Properties.Settings.Default.background.Equals("reallydark"))
            {
                Properties.Settings.Default.background = "dark";
                Properties.Settings.Default.Save();
            }

            gridColour.Background = Background;
        }
        private void DetectStore(){
            string currentDirectory = Environment.CurrentDirectory;

            if (currentDirectory.Contains("16188AluminiumTech.CSMark_20gejd9zdp9ny")){
                distribution = DistributionPlatform.WinStore;
                Title += " Windows Store Version";
            }
            else{
                distribution = DistributionPlatform.GitRepository;
            }
        }
        #region Updating Handling
        private bool CheckForUpdates(){
            Stopwatch sw = new Stopwatch();
            ac.checkForUpdate(betaURL);            
            sw.Start();
            while(sw.ElapsedMilliseconds < 3000 && ac.isCheckForUpdateCompleted()){
            }

            var installed = ac.getInstalledVersion();
            var latest = ac.getLatestVersion();

            return !Equals(installed, latest);
        }
        private void DownloadUpdates(){
            if (Properties.Settings.Default.UseBetaUpdateChannel){
                AutoUpdater.Start(betaURL);
            }
            else{
                AutoUpdater.Start(stableURL);
            }
        }
        #endregion
        #region Stress Test Handling
        private void ApplyStressBtnColors(){
            if (runningStress == false){
                stressBtn.Background = myGreenBrush;
                stressBtn.Content = "Start Stress Test";              
            }
            else{
                stressBtn.Background = myRedBrush;
                stressBtn.Content = "Stop Stress Test";             
            }
        }
        private void HandleStressTest(){
            if (runningStress == true){
                runningStress = false;
                benchBtn.IsEnabled = true;
                var stopStressTest = new Task(() => stc.StopStressTest());
                stopStressTest.Start();               
                t.Stop();
            }
            else{
                benchBtn.IsEnabled = false;
                runningStress = true;
                //Start the Stress Test as a new Task to ensure good UI performance.
                var startStressTest = new Task(() => stc.StartMultiStressTest());
                startStressTest.Start();
                //Create the dispatcher timer so we can see how long a stress test runs for.
                t = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 1), DispatcherPriority.Background,
               t_Tick, Dispatcher.CurrentDispatcher); t.IsEnabled = true;
                start = DateTime.Now;
                //Reset the stressTimer label.
                stressTimer.Content = "";
            }
        }
        #endregion
        private void StartBenchmark(){
            var benchmarkWorkTask = new Task(() => BenchmarkWork());         
            benchmarkWorkTask.Start();
            benchmarkWorkTask.Wait();       
            
            try{
                benchBtn.Dispatcher.Invoke(new Action(() => { benchBtn.IsEnabled = true; }));
                benchBtn.Dispatcher.Invoke(new Action(() => { benchBtn.Content = "Start Benchmark"; }));
                benchBtn.Dispatcher.Invoke(new Action(() => { eligible.Content = ""; }));
                benchBtn.Dispatcher.Invoke(new Action(() => { stressBtn.IsEnabled = true; }));
                benchBtn.Dispatcher.Invoke(new Action(() => { new BenchResults().ShowDialog(); }));
            }
            catch(Exception ex){
                MessageBox.Show(ex.ToString());
            }         
        }

        private async void BenchmarkWork(){
            var warmupTask = Task.Factory.StartNew(() => benchController.DoWarmup());
            warmupTask.Wait((30) * 1000);
            var task1 = Task.Factory.StartNew(() => benchController.StartSingleBenchmarkTests());
            task1.Wait((60 * 5) * 1000);
            var task2 = Task.Factory.StartNew(() => benchController.StartMultiBenchmarkTests()); 
            task2.Wait((60 * 5) * 1000);

            HashMap<BenchmarkType, Benchmark> hash = benchController.ReturnBenchmarkObjects();
            var resultSaver = new ResultSaver();
            var result = resultSaver.SaveResult(true, hash);
            Properties.Results.Default.BenchmarkResult = result;
            Properties.Results.Default.Save();
        }

        private void t_Tick(object sender, EventArgs e){
            if (runningStress){
                stressTimer.Content = Convert.ToString(DateTime.Now - start);
            }
        }
        #region Handle Button Clicks
        private void benchBtn_Click(object sender, RoutedEventArgs e){
            stressBtn.IsEnabled = false;
            benchBtn.Content = "Starting Benchmark...";
            benchBtn.IsEnabled = false;
            eligible.Content = "Starting Benchmark.... This may take a while. You may want to get a drink or some food whilst waiting.";
            eligible.Visibility = Visibility.Visible;
            var task = new Task(() => StartBenchmark());
            task.Start();          
        }
        private void stressBtn_Click(object sender, RoutedEventArgs e){            
            HandleStressTest();
            ApplyStressBtnColors();
        }

        private void OpenURLWin10(string URL){
            try{
                Window webTest = new BrowserView(URL, 720, 1280);
                webTest.ShowDialog();
            }
            catch{
                platform.OpenURLInBrowser(URL);
            }
        }

        private void patronLeftButtonDown(object sender, MouseButtonEventArgs e){
            Window webTest = new BrowserView(Properties.Settings.Default.patreonURL, 800, 600);
            webTest.ShowDialog();
        }
        private void menuExitBtn_Click(object sender, RoutedEventArgs e){
                Application.Current.Shutdown();
        }
        private void menuSettingsBtn_Click(object sender, RoutedEventArgs e){
            Window settings = new Settings(distribution);
            settings.ShowDialog();
        }
        private void menuAboutAppBtn_Click(object sender, RoutedEventArgs e){
            Window window = new AboutApp(distribution);
            window.ShowDialog();
        }
        private void main_Closing(object sender, System.ComponentModel.CancelEventArgs e){
                Application.Current.Shutdown();
        }
        private void menuAboutPCBtn_Click(object sender, RoutedEventArgs e){
            Window window = new AboutPC();
            window.ShowDialog();

        }
        private void submitBugReportBtn_Click(object sender, RoutedEventArgs e){
            try
            {
                OpenURLWin10(Properties.Settings.Default.githubURL + "/issues/new?template=bug_report.md");
            }
            catch{
                platform.OpenURLInBrowser(Properties.Settings.Default.githubURL + "/issues/new?template=bug_report.md");
            }
        }
        private void submitFeatureRequestBtn_Click(object sender, RoutedEventArgs e){
            try
            {
                OpenURLWin10(Properties.Settings.Default.githubURL + "/issues/new?template=feature_request.md");
            }
            catch
            {
                platform.OpenURLInBrowser(Properties.Settings.Default.githubURL + "/issues/new?template=feature_request.md");
            }
        }
        private void donateCSMarkMenuBtn_Click(object sender, RoutedEventArgs e){
                try
                {
                    OpenURLWin10(Properties.Settings.Default.patreonURL);
                }
                catch
                {
                    platform.OpenURLInBrowser(Properties.Settings.Default.patreonURL);
                }
            }
        private void joinDiscordMenuBtn_Click(object sender, RoutedEventArgs e){
            try
            {
                OpenURLWin10(Properties.Settings.Default.discordURL);
            }
            catch
            {
                platform.OpenURLInBrowser(Properties.Settings.Default.discordURL);
            }
        } 
        private void getSourceCodeMenuBtn_Click(object sender, RoutedEventArgs e){
            try
            {
                OpenURLWin10(Properties.Settings.Default.githubURL);
            }
            catch
            {
                platform.OpenURLInBrowser(Properties.Settings.Default.githubURL);
            }
        }
        private void viewPrivacyPolicyBtn_Click(object sender, RoutedEventArgs e){
            try
            {
                OpenURLWin10(Properties.Settings.Default.githubURL + "/blob/master/PrivacyPolicy.md");
            }
            catch
            {
                platform.OpenURLInBrowser(Properties.Settings.Default.githubURL + "/blob/master/PrivacyPolicy.md");
            }
        } 
        private void viewSourceCodeLicenseBtn_Click(object sender, RoutedEventArgs e){
            try
            {
                OpenURLWin10(Properties.Settings.Default.githubURL + "/blob/master/LICENSE");
            }
            catch
            {
                platform.OpenURLInBrowser(Properties.Settings.Default.githubURL + "/blob/master/LICENSE");
            }
        }
        private void checkUpdatesMenuBtn_Click(object sender, RoutedEventArgs e){
            var check = CheckForUpdates();

            if (check){
                DownloadUpdates();
            }
        }
        private void main_GotFocus(object sender, RoutedEventArgs e){
            LoadBackground();
        }
        private void main_MouseEnter(object sender, MouseEventArgs e){
            LoadBackground();
        }
        #endregion
    }
}