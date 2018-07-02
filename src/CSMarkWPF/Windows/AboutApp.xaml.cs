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
        private SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(125, 244, 66));
        private SolidColorBrush myRedBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        private SolidColorBrush myPurpleBrush = new SolidColorBrush(Color.FromRgb(179, 66, 244));
        private SolidColorBrush myPinkBrush = new SolidColorBrush(Color.FromRgb(244, 66, 241));

        private SolidColorBrush black = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));
        private SolidColorBrush blueDark = new SolidColorBrush(Color.FromRgb(43, 76, 119));
        private SolidColorBrush blueGray = new SolidColorBrush(Color.FromRgb(73, 121, 183));
        private SolidColorBrush blurple = new SolidColorBrush(Color.FromRgb(114, 137, 218));

        private Platform platform;
    
        public AboutApp(DistributionPlatform distribution){
            InitializeComponent();
            LoadBackground();

            //Show the version number
            versionLabel.Content += "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            platform = new Platform();

            if(distribution.Equals(DistributionPlatform.SteamStore) || distribution.Equals(DistributionPlatform.WinStore)){
                patronImage.Visibility = Visibility.Collapsed;
            }
        }

        public void LoadBackground(){
            if (Properties.Settings.Default.background.Equals("reallydark")){
                gridColour.Background = reallyDark;
            }
            else if (Properties.Settings.Default.background.Equals("dark")){
                gridColour.Background = dark;
            }
            else if (CSMarkDesktop.Properties.Settings.Default.background.Equals("bluedark")){
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

            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            title.Foreground = Foreground;
            infoLabel.Foreground = Foreground;
            versionLabel.Foreground = Foreground;
        }
        private void patronImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e){
            platform.OpenURLInBrowser(Properties.Settings.Default.patreonURL);
        }
    }
}