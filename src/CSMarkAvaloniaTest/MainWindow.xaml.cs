/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
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
        SolidColorBrush myPurpleBrush = new SolidColorBrush(Color.FromRgb(179, 66, 244));
        SolidColorBrush myPinkBrush = new SolidColorBrush(Color.FromRgb(244, 66, 241));

        SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));
        SolidColorBrush blueDark = new SolidColorBrush(Color.FromRgb(43, 76, 119));
        SolidColorBrush blueGray = new SolidColorBrush(Color.FromRgb(73, 121, 183));
        SolidColorBrush lightBlueGray = new SolidColorBrush(Color.FromRgb(144, 158, 175));

        StressTestController stc;
        DateTime start;
        DispatcherTimer t;

        private bool runningStress = false;

        protected string betaURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/beta.xml";
        protected string stableURL = "https://raw.githubusercontent.com/CSMarkBenchmark/CSMarkDesktop/master/channels/wpf/stable.xml";

        public MainWindow()
        {
            InitializeComponent();

            Background = dark;

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

        private void LoadBackground()
        {
            /*
            if (Settings.Default.background.Equals("reallydark"))
            {
                gridColour.Background = reallyDark;
            }
            else if (Settings.Default.background.Equals("dark"))
            {
                gridColour.Background = dark;
            }
            else if (Settings.Default.background.Equals("bluedark"))
            {
                gridColour.Background = blueDark;
            }
            else if (Settings.Default.background.Equals("bluegray"))
            {
                gridColour.Background = blueGray;
            }
            else if (Settings.Default.background.Equals("lightbluegray"))
            {
                gridColour.Background = lightBlueGray;
            }
            else
            {
                gridColour.Background = dark;
            }
            */

           
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
