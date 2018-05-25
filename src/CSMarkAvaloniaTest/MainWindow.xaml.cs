using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using CSMarkLib;
using System;

namespace CSMarkAvaloniaTest
{
    public class MainWindow : Window
    {
       SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(125, 244, 66));
        SolidColorBrush myRedBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
    
        StressTestController stc;

        DispatcherTimer t;
        DateTime start;

       private bool runningStress = false;

        protected string betaURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/beta.xml";
        protected string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/stable.xml";

        public MainWindow()
        {
            InitializeComponent();
            stc = new StressTestController();
            ApplyStressBtnColors();

            //Show the version number
        //    versionLabel.Content = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void ApplyStressBtnColors()
        {

        }

        private void HandleStressTest()
        {

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
