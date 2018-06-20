/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using AluminiumCoreLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CSMarkDesktop.Windows{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window{
        private SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(125, 244, 66));
        private SolidColorBrush myRedBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        private SolidColorBrush myPurpleBrush = new SolidColorBrush(Color.FromRgb(179, 66, 244));
        private SolidColorBrush myPinkBrush = new SolidColorBrush(Color.FromRgb(244, 66, 241));

        private SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));
        private SolidColorBrush blueDark = new SolidColorBrush(Color.FromRgb(43, 76, 119));
        private SolidColorBrush blueGray = new SolidColorBrush(Color.FromRgb(73, 121, 183));
        private SolidColorBrush lightBlueGray = new SolidColorBrush(Color.FromRgb(144, 158, 175));

        private SolidColorBrush modernCSmarkGreen = new SolidColorBrush(Color.FromRgb(133, 186, 106));
        private SolidColorBrush modernDarkCSMarkGreen = new SolidColorBrush(Color.FromRgb(31, 139, 76));

        public Settings(){
            InitializeComponent();
            LoadBackground();
            LoadSettings();
        }

        private void LoadSettings(){
            enableCheckBetaUpdateBtn.IsChecked = Properties.Settings.Default.UseBetaUpdateChannel;
            enableCheckUpdateOnStartupBtn.IsChecked = Properties.Settings.Default.CheckForUpdatesOnStartup;
            enableMinimizeOnQuitBtn.IsChecked = Properties.Settings.Default.exitButtonShouldQuitApp;
            enableHideBecomePatronBtn.IsChecked = Properties.Settings.Default.HideBecomeAPatronButton;
        }
        private void ApplySettings(){
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Save();
        }
        private void LoadBackground(){
            if (Properties.Settings.Default.background.Equals("reallydark"))
            {
                gridColour.Background = reallyDark;
            }
            else if (Properties.Settings.Default.background.Equals("dark"))
            {
                gridColour.Background = dark;
            }
            else if (Properties.Settings.Default.background.Equals("bluedark"))
            {
                gridColour.Background = blueDark;
            }
            else if (Properties.Settings.Default.background.Equals("bluegray"))
            {
                gridColour.Background = blueGray;
            }
            else if (Properties.Settings.Default.background.Equals("lightbluegray"))
            {
                gridColour.Background = lightBlueGray;
            }
            else{
                gridColour.Background = dark;
            }

            enableCheckBetaUpdateBtn.Background = gridColour.Background;
            enableCheckUpdateOnStartupBtn.Background = gridColour.Background;
            enableMinimizeOnQuitBtn.Background = gridColour.Background;
            enableHideBecomePatronBtn.Background = gridColour.Background;
            WindowTitle.Background = gridColour.Background;
            applySettingsBtn.Background = gridColour.Background;
            closeBtn.Background = gridColour.Background;
            changeLabel.Background = gridColour.Background;

            SolidColorBrush fore;

            if (gridColour.Background != lightBlueGray && gridColour.Background != blueGray){
                fore = new SolidColorBrush(Color.FromRgb(255,255,255));
            }
            else{
                fore = new SolidColorBrush(Color.FromRgb(44, 47, 51));              
            }

            enableCheckBetaUpdateBtn.Foreground = fore;
            enableCheckUpdateOnStartupBtn.Foreground = fore;
            enableMinimizeOnQuitBtn.Foreground = fore;
            enableHideBecomePatronBtn.Foreground = fore;
            WindowTitle.Foreground = fore;
            applySettingsBtn.Foreground = fore;
            closeBtn.Foreground = fore;
            changeLabel.Foreground = fore;
        }
        private void enableCheckBetaUpdateBtn_Checked(object sender, RoutedEventArgs e){
            Properties.Settings.Default.UseBetaUpdateChannel = (bool)enableCheckBetaUpdateBtn.IsChecked;
        }
        private void enableCheckUpdateOnStartupBtn_Checked(object sender, RoutedEventArgs e){
            Properties.Settings.Default.CheckForUpdatesOnStartup = (bool)enableCheckUpdateOnStartupBtn.IsChecked;
        }
        private void enableMinimizeOnQuitBtn_Checked(object sender, RoutedEventArgs e){
            Properties.Settings.Default.exitButtonShouldQuitApp = (bool)enableMinimizeOnQuitBtn.IsChecked;
        }
        private void closeBtn_Click(object sender, RoutedEventArgs e){
            Close();
        }
        private void applySettingsBtn_Click(object sender, RoutedEventArgs e){
            ApplySettings();
        }
        private void enableHideBecomePatronBtn_Checked(object sender, RoutedEventArgs e){
                Properties.Settings.Default.HideBecomeAPatronButton = (bool)enableHideBecomePatronBtn.IsChecked;      
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e){
            
        }
        private void enableHideBecomePatronBtn_Unchecked(object sender, RoutedEventArgs e){
            Properties.Settings.Default.HideBecomeAPatronButton = (bool)enableHideBecomePatronBtn.IsChecked;
        }
        private void enableMinimizeOnQuitBtn_Unchecked(object sender, RoutedEventArgs e){
            Properties.Settings.Default.exitButtonShouldQuitApp = (bool)enableMinimizeOnQuitBtn.IsChecked;
        }
        private void enableCheckUpdateOnStartupBtn_Unchecked(object sender, RoutedEventArgs e){
            Properties.Settings.Default.CheckForUpdatesOnStartup = (bool)enableCheckUpdateOnStartupBtn.IsChecked;
        }
        private void enableCheckBetaUpdateBtn_Unchecked(object sender, RoutedEventArgs e){
            Properties.Settings.Default.UseBetaUpdateChannel = (bool)enableCheckBetaUpdateBtn.IsChecked;
            
        }
    }
}