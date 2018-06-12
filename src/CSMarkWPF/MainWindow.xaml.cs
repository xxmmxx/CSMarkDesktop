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
using CSMarkReduxWPF.Properties;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.IO;

namespace CSMarkReduxWPF{

    /// </summ    /// <summary>
    /// Interaction logic for MainWindow.xaml>
    public partial class MainWindow : Window{
        SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(125, 244, 66));
        SolidColorBrush myRedBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        SolidColorBrush myPurpleBrush = new SolidColorBrush(Color.FromRgb(179, 66, 244));
        SolidColorBrush myPinkBrush = new SolidColorBrush(Color.FromRgb(244, 66, 241));

        SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));
        SolidColorBrush blueDark = new SolidColorBrush(Color.FromRgb(43, 76, 119));
        SolidColorBrush blueGray = new SolidColorBrush(Color.FromRgb(73, 121, 183));
        SolidColorBrush lightBlueGray = new SolidColorBrush(Color.FromRgb(144, 158, 175));

        CSMarkLib.UpdatingServices.AutoUpdater ac = new CSMarkLib.UpdatingServices.AutoUpdater();

        LinearGradientBrush orangeGradient = new LinearGradientBrush(Color.FromRgb(0, 0, 0), Color.FromRgb(249, 67, 12),45.0);
        Platform platform;
        StressTestController stc;
        DateTime start;
        DispatcherTimer t;

        private bool runningStress = false;

        protected string betaURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/beta.xml";
        protected string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/stable.xml";

        public MainWindow(){
            InitializeComponent();

            Assembly assembly = Assembly.GetEntryAssembly();
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");
            AutoUpdater.LetUserSelectRemindLater = false;
            AutoUpdater.ReportErrors = true;
            AutoUpdater.ShowSkipButton = false;
            AutoUpdater.ShowRemindLaterButton = false;   

            LoadBackground();
            stc = new StressTestController();
            ApplyStressBtnColors();
            platform = new Platform();

            //Show the version number
            versionLabel.Content = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            DetectBenchmarkEligibility();           
        }

        private void LoadBackground() {
            if (Settings.Default.background.Equals("reallydark")) {
                gridColour.Background = reallyDark;
            }
            else if (Settings.Default.background.Equals("dark")) {
                gridColour.Background = dark;
            }
            else if (Settings.Default.background.Equals("bluedark")) {
                gridColour.Background = blueDark;
            }
            else if (Settings.Default.background.Equals("bluegray")) {
                gridColour.Background = blueGray;
            }
            else if (Settings.Default.background.Equals("lightbluegray")){
                gridColour.Background = lightBlueGray;
            }
            else{
                gridColour.Background = dark;
            }
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
                AutoUpdater.Start(betaURL);
        }

        private void DetectBenchmarkEligibility(){
            Platform platform = new Platform();
           var os = platform.ReturnPlatform();

            if (os.Equals("Windows 10")){
                benchBtn.Content = "Start Benchmark";
                benchBtn.IsEnabled = true;
                eligible.Content = "This device is eligible to run the Benchmark."; 
            }
            else if (os.Equals("Windows 7") && os.Equals("Windows 8") && os.Equals("Windows 8.1")){
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
                t = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 50), DispatcherPriority.Background,
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
        private void benchBtn_Click(object sender, RoutedEventArgs e){
            if (benchmarkCLICheck()){
                executeCLIApp();
            }
            else
            {
                MessageBox.Show("We were unable to start the CSMarkCoreBenchmarkApp. Please ensure you have a valid CSMarkCoreBenchmarkApp folder in the current app directory before trying again.", "Failed to Start CSMarkCoreBenchmarkApp");
            }
        }
        private void stressBtn_Click(object sender, RoutedEventArgs e){            
            HandleStressTest();
            ApplyStressBtnColors();
        }
        private void checkBetaUpdateBtn_Click(object sender, RoutedEventArgs e){
           var check = checkForUpdates();

            if (check){
                downloadUpdates();
            }
            // If updates aren't available, don't bother trying to use AutoUpdater.
        }
        private void patronLeftButtonDown(object sender, MouseButtonEventArgs e){
            platform.OpenURLInBrowser("https://www.patreon.com/csmark");
        }
    }
}