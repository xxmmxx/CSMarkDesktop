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

namespace CSMarkDesktop{

    /// <summary>
    /// Interaction logic for MainWindow.xaml>
    public partial class MainWindow : Window{
        private SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(125, 244, 66));
        private SolidColorBrush myRedBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        private SolidColorBrush myPurpleBrush = new SolidColorBrush(Color.FromRgb(179, 66, 244));
        private SolidColorBrush myPinkBrush = new SolidColorBrush(Color.FromRgb(244, 66, 241));

        private SolidColorBrush black = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));
        private SolidColorBrush blueDark = new SolidColorBrush(Color.FromRgb(43, 76, 119));
        private SolidColorBrush blueGray = new SolidColorBrush(Color.FromRgb(80, 148, 237));
        private SolidColorBrush blurple = new SolidColorBrush(Color.FromRgb(114, 137, 218));

        private CSMarkLib.UpdatingServices.AutoUpdater ac = new CSMarkLib.UpdatingServices.AutoUpdater();

        private Platform platform;
        private StressTestController stc;
        private DateTime start;
        private DispatcherTimer t;

        private bool runningStress = false;

        private string betaURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/beta.xml";
        private string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/stable.xml";

        //Supported Versions of Windows
        private Version win10v1607 = new Version(10, 0, 14393, 0);
        private Version win10v1703 = new Version(10, 0, 15063, 0);
        private Version win10v1709 = new Version(10, 0, 16299, 0);
        private Version win10v1803 = new Version(10, 0, 17134, 0);
        //We don't support these versions of Windows.
        private Version win7_0 = new Version(6,1, 7600,0);
        private Version win7_1 = new Version(6,1, 7601,0);
        private Version win8_0 = new Version(6,2,9200, 0);
        private Version win8_1 = new Version(6,3,9200 ,0);
        private Version win8_1_update = new Version(6, 3, 9600, 0);
        private Version win10v1507 = new Version(10, 0, 10240, 0);
        private Version win10v1511 = new Version(10, 0, 10586, 0);

        private Brush foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private Brush background;
        public enum DistributionPlatform{
            SteamStore,
            WinStore,
            GitRepository
        }

        private string AppVersion;
        
        DistributionPlatform distribution = DistributionPlatform.GitRepository;

        public MainWindow(){
            InitializeComponent();
            Assembly assembly = Assembly.GetEntryAssembly();
           
            if(distribution.Equals(DistributionPlatform.SteamStore) || distribution.Equals(DistributionPlatform.WinStore)){
                checkUpdatesMenuBtn.IsEnabled = false;
                checkUpdatesMenuBtn.Visibility = Visibility.Collapsed;                
                menuRestartAppBtn.IsEnabled = false;
                menuRestartAppBtn.Visibility = Visibility.Collapsed;
                patronImage.Visibility = Visibility.Collapsed;
            }
            else{
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");
                AutoUpdater.LetUserSelectRemindLater = false;
                AutoUpdater.ReportErrors = true;
                AutoUpdater.ShowSkipButton = false;
                AutoUpdater.ShowRemindLaterButton = false;
            }

            LoadBackground();
            stc = new StressTestController();
            ApplyStressBtnColors();
            platform = new Platform();

            //Show the version number
            versionLabel.Content = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AppVersion = versionLabel.Content.ToString();

            if (!benchmarkCLICheck() && (!distribution.Equals(DistributionPlatform.SteamStore) && !distribution.Equals(DistributionPlatform.WinStore))){
                benchBtn.IsEnabled = false;
                eligible.Content = "Download the CSMarkCoreBenchmarkApp zip file and extract it in the CSMarkDesktop app folder to run the benchmark";
            }

            DetectBenchmarkEligibility();

            if (Properties.Settings.Default.HideBecomeAPatronButton == true){
                patronImage.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadBackground(){
            if (Properties.Settings.Default.background.Equals("reallydark")) {
                gridColour.Background = reallyDark;
            }
            else if (Properties.Settings.Default.background.Equals("dark")) {
                gridColour.Background = dark;
            }
            else if (Properties.Settings.Default.background.Equals("bluedark")) {
                gridColour.Background = blueDark;
            }
            else if (Properties.Settings.Default.background.Equals("bluegray")) {
                gridColour.Background = blueGray;
            }
            else if (Properties.Settings.Default.background.Equals("blurple")){
                gridColour.Background = blurple;
            }
            else if (Properties.Settings.Default.background.Equals("justblack")){
                gridColour.Background = black;
            }
            else{
                gridColour.Background = dark;
            }

            background = gridColour.Background;
            var thickness = new Thickness(0);

            dockPanel.Background = background;  

            menuBar.Background = background;
            menuBar.Foreground = foreground;
            menuBar.BorderThickness = thickness;
            menuBar.BorderBrush = background;

            helpMenu.Background = background;
            helpMenu.Foreground = foreground;
            helpMenu.BorderThickness = thickness;
            helpMenu.BorderBrush = background;

            fileMenu.Background = background;
            fileMenu.Foreground = foreground;
            fileMenu.BorderThickness = thickness;
            fileMenu.BorderBrush = background;

            aboutMenu.Background = background;
            aboutMenu.Foreground = foreground;
            aboutMenu.BorderThickness = thickness;
            aboutMenu.BorderBrush = background;

            supportMenu.Background = background;
            supportMenu.Foreground = foreground;
            supportMenu.BorderThickness = thickness;
            supportMenu.BorderBrush = background;

            menuSettingsBtn.Foreground = foreground;
            menuSettingsBtn.Background = background;
            menuSettingsBtn.BorderThickness = thickness;
            menuSettingsBtn.BorderBrush = background;

            menuAboutAppBtn.Background = background;
            menuAboutAppBtn.Foreground = foreground;
            menuAboutAppBtn.BorderThickness = thickness;
            menuAboutAppBtn.BorderBrush = background;

            menuAboutPCBtn.Background = background;
            menuAboutPCBtn.Foreground = foreground;
            menuAboutPCBtn.BorderThickness = thickness;
            menuAboutPCBtn.BorderBrush = background;

            menuExitBtn.Foreground = foreground;
            menuExitBtn.Background = background;
            menuExitBtn.BorderThickness = thickness;
            menuExitBtn.BorderBrush = background;

            menuRestartAppBtn.Background = background;
            menuRestartAppBtn.Foreground = foreground;
            menuRestartAppBtn.BorderThickness = thickness;
            menuRestartAppBtn.BorderBrush = background;

            submitBugReportBtn.Background = background;
            submitBugReportBtn.Foreground = foreground;
            submitBugReportBtn.BorderThickness = thickness;
            submitBugReportBtn.BorderBrush = background;

            submitFeatureRequestBtn.Background = background;
            submitFeatureRequestBtn.Foreground = foreground;
            submitFeatureRequestBtn.BorderThickness = thickness;
            submitFeatureRequestBtn.BorderBrush = background;

            joinDiscordMenuBtn.Background = background;
            joinDiscordMenuBtn.Foreground = foreground;
            joinDiscordMenuBtn.BorderThickness = thickness;
            joinDiscordMenuBtn.BorderBrush = background;

            getSourceCodeMenuBtn.Background = background;
            getSourceCodeMenuBtn.Foreground = foreground;
            getSourceCodeMenuBtn.BorderThickness = thickness;
            getSourceCodeMenuBtn.BorderBrush = background;

            donateCSMarkMenuBtn.Background = background;
            donateCSMarkMenuBtn.Foreground = foreground;
            donateCSMarkMenuBtn.BorderThickness = thickness;
            donateCSMarkMenuBtn.BorderBrush = background;

            viewPrivacyPolicyBtn.Background = background;
            viewPrivacyPolicyBtn.Foreground = foreground;
            viewPrivacyPolicyBtn.BorderThickness = thickness;
            viewPrivacyPolicyBtn.BorderBrush = background;

            viewSourceCodeLicenseBtn.Background = background;
            viewSourceCodeLicenseBtn.Foreground = foreground;
            viewSourceCodeLicenseBtn.BorderThickness = thickness;
            viewSourceCodeLicenseBtn.BorderBrush = background;

            checkUpdatesMenuBtn.Background = background;
            checkUpdatesMenuBtn.Foreground = foreground;
            checkUpdatesMenuBtn.BorderThickness = thickness;
            checkUpdatesMenuBtn.BorderBrush = background;
        }

        private bool checkForUpdates(){
            Stopwatch sw = new Stopwatch();
            ac.checkForUpdate(betaURL);            
            sw.Start();
            while(sw.ElapsedMilliseconds < 3000 && ac.isCheckForUpdateCompleted()){
            }

            var installed = ac.getInstalledVersion();
            var latest = ac.getLatestVersion();

            return !Equals(installed, latest);
        }

        private void downloadUpdates(){
            if (Properties.Settings.Default.UseBetaUpdateChannel){
                AutoUpdater.Start(betaURL);
            }
            else{
                AutoUpdater.Start(stableURL);
            }
        }

        private void DetectBenchmarkEligibility(){
            Platform platform = new Platform();
           var os = platform.ReturnPlatform();

            if (os.Equals("Windows 10") && ((Environment.OSVersion.Version == win10v1607) || (Environment.OSVersion.Version == win10v1703) || (Environment.OSVersion.Version == win10v1709) || (Environment.OSVersion.Version == win10v1803))){
                benchBtn.Content = "Start Benchmark";
                benchBtn.IsEnabled = true;
                eligible.Visibility = Visibility.Hidden;
            }
            else if (os.Equals("Windows 7") || os.Equals("Windows 8") || os.Equals("Windows 8.1")){
                benchBtn.Content = "Unable to run Benchmark";
                benchBtn.IsEnabled = false;
                eligible.Content = "Upgrade to Windows 10 to run the benchmark.";
            }
        }

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
                stc.StopStressTest();
                t.Stop();
            }
            else{
                runningStress = true;
                stc.StartMultiStressTest();
                t = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 1), DispatcherPriority.Background,
               t_Tick, Dispatcher.CurrentDispatcher); t.IsEnabled = true;
                start = DateTime.Now;
                stressTimer.Content = "";
            }
        }

        private bool benchmarkCLICheck(){
            string coreBenchmarkDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Win10-x64" + Path.DirectorySeparatorChar;
            return Directory.Exists(coreBenchmarkDirectory);
        }
        private void executeCLIApp(){
            string currentDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Win10-x64" + Path.DirectorySeparatorChar + "publish" + Path.DirectorySeparatorChar + "CSMarkCoreBenchmarkApp";
            try{
                Platform platform = new Platform();
                platform.RunProcess(currentDirectory);
            }
            catch(Exception ex){
                MessageBox.Show(ex.ToString(), "Exception Occured");
            }

            MessageBox.Show("After the benchmark is completed, the result text file will be saved in the CSMarkCoreBenchmarkApp folder.", "Result Saving Reminder");
        }

        private void t_Tick(object sender, EventArgs e){
            stressTimer.Content = Convert.ToString(DateTime.Now - start);
        }
        #region Handle Button Clicks
        private void benchBtn_Click(object sender, RoutedEventArgs e){
            benchBtn.Content = "Starting Benchmark...";
            benchBtn.IsEnabled = false;
            eligible.Content = "Starting Benchmark.... During this benchmark, your system may become unresponsive for a short peroid of time.";
            eligible.Visibility = Visibility.Visible;
            new LaunchBenchmark(AppVersion, background, foreground);
            benchBtn.Content = "Start Benchmark";
            benchBtn.IsEnabled = true;
            eligible.Content = "";
        }
        private void stressBtn_Click(object sender, RoutedEventArgs e){            
            HandleStressTest();
            ApplyStressBtnColors();
        }
        private void patronLeftButtonDown(object sender, MouseButtonEventArgs e){
            platform.OpenURLInBrowser("https://www.patreon.com/csmark");
        }
        private void menuExitBtn_Click(object sender, RoutedEventArgs e){
                //Close the app if the menu button 
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
            //By default, exit the app if the user hits the X button.
            if (Properties.Settings.Default.exitButtonShouldQuitApp){
                //Close the app if the menu button 
                Application.Current.Shutdown();
            }
            //Else minimize the app.
            else{
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
        }
        private void menuAboutPCBtn_Click(object sender, RoutedEventArgs e){
            Window window = new AboutPC();
            window.ShowDialog();
        }
        private void menuRestartAppBtn_Click(object sender, RoutedEventArgs e){
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        private void submitBugReportBtn_Click(object sender, RoutedEventArgs e){
            platform.OpenURLInBrowser("https://github.com/CSMarkBenchmark/CSMarkDesktop/issues/new?template=bug_report.md");
        }
        private void submitFeatureRequestBtn_Click(object sender, RoutedEventArgs e){
            platform.OpenURLInBrowser("https://github.com/CSMarkBenchmark/CSMarkDesktop/issues/new?template=feature_request.md");
        }
        private void donateCSMarkMenuBtn_Click(object sender, RoutedEventArgs e){
            platform.OpenURLInBrowser("https://www.patreon.com/CSMark/");
        }
        private void joinDiscordMenuBtn_Click(object sender, RoutedEventArgs e){
            platform.OpenURLInBrowser("https://discord.gg/mUHAqUr");
        } 
        private void getSourceCodeMenuBtn_Click(object sender, RoutedEventArgs e){
            platform.OpenURLInBrowser("https://github.com/CSMarkBenchmark/CSMarkDesktop/");
        }
        private void viewPrivacyPolicyBtn_Click(object sender, RoutedEventArgs e){
            platform.OpenURLInBrowser("https://github.com/CSMarkBenchmark/CSMarkDesktop/blob/master/PrivacyPolicy.md");
        } 
        private void viewSourceCodeLicenseBtn_Click(object sender, RoutedEventArgs e){
            platform.OpenURLInBrowser("https://github.com/CSMarkBenchmark/CSMarkDesktop/blob/master/LICENSE");
        }
        private void checkUpdatesMenuBtn_Click(object sender, RoutedEventArgs e){
            var check = checkForUpdates();

            if (check){
                downloadUpdates();
            }
        }
        #endregion
        private void main_GotFocus(object sender, RoutedEventArgs e){
            LoadBackground();
            if (Properties.Settings.Default.HideBecomeAPatronButton == true)
            {
                patronImage.Visibility = Visibility.Collapsed;
            }
            else
            {
                patronImage.Visibility = Visibility.Visible;
            }
        }
        private void main_MouseEnter(object sender, MouseEventArgs e){
            LoadBackground();
            if (Properties.Settings.Default.HideBecomeAPatronButton == true)
            {
                patronImage.Visibility = Visibility.Collapsed;
            }
            else
            {
                patronImage.Visibility = Visibility.Visible;
            }
        }
    }
}