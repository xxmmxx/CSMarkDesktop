using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
using AutoUpdaterDotNET;
using CSMarkLib;

namespace CSMarkReduxWPF{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(125, 244, 66));
        SolidColorBrush myRedBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        StressTestController stc;

        DispatcherTimer t;
        DateTime start;

        private bool runningStress = false;

        protected string betaURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/beta.xml";
        protected string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/stable.xml";

        public MainWindow(){
            InitializeComponent();
            stc = new StressTestController();
            ApplyStressBtnColors();         

            //Disable Benchmark Button since it currently does nothing.
            benchBtn.IsEnabled = false;
            //Hide unfinished stress testing timed stuff.
            stressTimeBox.IsEnabled = false;
            stressTimeBox.Visibility = Visibility.Hidden;
            stressDurationLabel.IsEnabled = false;
            stressDurationLabel.Visibility = Visibility.Hidden;

            //Show the version number
            versionLabel.Content = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void ApplyStressBtnColors(){
            if (runningStress == false)
            {
                stressBtn.Background = myGreenBrush;
                stressBtn.Content = "Start Stress Test";       
            }
            else
            {
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

        private void t_Tick(object sender, EventArgs e)
        {
            stressTimer.Content = Convert.ToString(DateTime.Now - start);
        }
        private void benchBtn_Click(object sender, RoutedEventArgs e){
            
        }
        private void stressBtn_Click(object sender, RoutedEventArgs e){            
            HandleStressTest();
            ApplyStressBtnColors();
        }

        private void checkBetaUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            AutoUpdater.ShowSkipButton = false;
            AutoUpdater.Start(betaURL);
        }
    }
}
