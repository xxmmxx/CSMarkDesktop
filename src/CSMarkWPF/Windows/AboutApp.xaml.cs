/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using AluminiumCoreLib.Utilities;
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
using System.Windows.Shapes;
using static CSMarkDesktop.MainWindow;

namespace CSMarkDesktop.Windows{
    /// <summary>
    /// Interaction logic for AboutApp.xaml
    /// </summary>
    public partial class AboutApp : Window{
        private SolidColorBrush black = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));

        private Platform platform;
    
        public AboutApp(DistributionPlatform distribution){
            InitializeComponent();
            LoadBackground();

            //Show the version number
            versionLabel.Content += "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (distribution == DistributionPlatform.WinStore){
                versionLabel.Content += " Windows Store Version";
            }

            platform = new Platform();
        }

        public void LoadBackground(){
            if (Properties.Settings.Default.background.Equals("reallydark")){
                gridColour.Background = reallyDark;
            }
            else if (Properties.Settings.Default.background.Equals("dark")){
                gridColour.Background = dark;
            }
            else if (Properties.Settings.Default.background.Equals("justblack")){
                gridColour.Background = black;
            }
            else{
                gridColour.Background = dark;
            }

            Background = gridColour.Background;
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            title.Foreground = Foreground;
            infoLabel.Foreground = Foreground;
            versionLabel.Foreground = Foreground;
            thirdpartylicensenotice.Background = Background;
            thirdpartylicensenotice.Foreground = Foreground;
        }
        private void patronImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e){
            platform.OpenURLInBrowser(Properties.Settings.Default.patreonURL);
        }
    }
}