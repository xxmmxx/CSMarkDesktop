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
using System.Windows.Shapes;
using System.Windows.Threading;
using AluminiumCoreLib.Utilities;
using AutoUpdaterDotNET;
using CSMarkLib;
using CSMarkReduxWPF.Properties;
using System.Diagnostics;

namespace CSMarkReduxWPF{

    /// </summ    /// <summary>
    /// Interaction logic for MainWindow.xamlary>
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

        LinearGradientBrush orangeGradient = new LinearGradientBrush(Color.FromRgb(0, 0, 0), Color.FromRgb(249, 67, 12),45.0);

        StressTestController stc;

        DispatcherTimer t;
        DateTime start;

        private bool runningStress = false;

        protected string betaURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/beta.xml";
        protected string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/stable.xml";

        public MainWindow(){
            InitializeComponent();
            LoadBackground();
            stc = new StressTestController();
            ApplyStressBtnColors();         

            //Disable Benchmark Button since it currently does nothing.
            benchBtn.IsEnabled = false;

            //Show the version number
            versionLabel.Content = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            DetectBenchmarkEligibility();

            //Check for updates automatically on startup.
            AutoUpdater.ShowSkipButton = false;
            AutoUpdater.Start(betaURL);
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
                gridColour.Background = orangeGradient;
            }
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

        private void t_Tick(object sender, EventArgs e){
            stressTimer.Content = Convert.ToString(DateTime.Now - start);
        }
        private void benchBtn_Click(object sender, RoutedEventArgs e){
            
        }
        private void stressBtn_Click(object sender, RoutedEventArgs e){            
            HandleStressTest();
            ApplyStressBtnColors();
        }
        private void checkBetaUpdateBtn_Click(object sender, RoutedEventArgs e){
                AutoUpdater.ShowSkipButton = false;
                AutoUpdater.Start(betaURL);
        }
    }
}