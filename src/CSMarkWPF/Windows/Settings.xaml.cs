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

        private SolidColorBrush black = new SolidColorBrush(Color.FromRgb(0,0,0));
        private SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));
        private SolidColorBrush blueDark = new SolidColorBrush(Color.FromRgb(43, 76, 119));
        private SolidColorBrush blueGray = new SolidColorBrush(Color.FromRgb(80, 148, 237));
        private SolidColorBrush blurple = new SolidColorBrush(Color.FromRgb(114, 137, 218));

        private SolidColorBrush modernDarkCSMarkGreen = new SolidColorBrush(Color.FromRgb(31, 139, 76));

        public Settings(){
            InitializeComponent();
            LoadBackground();
            LoadSettings();
            changeLabel.Visibility = Visibility.Hidden;
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
            if (Properties.Settings.Default.background.Equals("reallydark")){
                gridColour.Background = reallyDark;
            }
            else if (Properties.Settings.Default.background.Equals("dark")){
                gridColour.Background = dark;
            }
            else if (Properties.Settings.Default.background.Equals("bluedark")){
                gridColour.Background = blueDark;
            }
            else if (Properties.Settings.Default.background.Equals("bluegray")){
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

            var background = gridColour.Background;
            var foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            enableCheckBetaUpdateBtn.Background = background;
            enableCheckUpdateOnStartupBtn.Background = background;
            enableMinimizeOnQuitBtn.Background = background;
            enableHideBecomePatronBtn.Background = background;
            WindowTitle.Background = background;
            applySettingsBtn.Background = background;
            closeBtn.Background = background;
            changeLabel.Background = background;
            themeLabel.Background = background;

            enableCheckBetaUpdateBtn.Foreground = foreground;
            enableCheckUpdateOnStartupBtn.Foreground = foreground;
            enableMinimizeOnQuitBtn.Foreground = foreground;
            enableHideBecomePatronBtn.Foreground = foreground;
            WindowTitle.Foreground = foreground;
            applySettingsBtn.Foreground = foreground;
            closeBtn.Foreground = foreground;
            changeLabel.Foreground = foreground;
            themeLabel.Foreground = foreground;
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

        private void lightBlueGrayBtn_Click(object sender, RoutedEventArgs e){
            Properties.Settings.Default.background = "lightbluegray";
            Properties.Settings.Default.Save();
            LoadBackground();
        }
        private void blueGrayBtn_Click(object sender, RoutedEventArgs e){
            Properties.Settings.Default.background = "bluegray";
            Properties.Settings.Default.Save();
            LoadBackground();
        }
       private void blueDarkBtn_Click(object sender, RoutedEventArgs e){
            Properties.Settings.Default.background = "bluedark";
            Properties.Settings.Default.Save();
            LoadBackground();
        }
        private void darkBtn_Click(object sender, RoutedEventArgs e){
            Properties.Settings.Default.background = "dark";
            Properties.Settings.Default.Save();
            LoadBackground();
        }
        private void reallyDarkBtn_Click(object sender, RoutedEventArgs e){
            Properties.Settings.Default.background = "reallydark";
            Properties.Settings.Default.Save();
            LoadBackground();
        }
        private void blurpleBtn_Click(object sender, RoutedEventArgs e){
            Properties.Settings.Default.background = "blurple";
            Properties.Settings.Default.Save();
            LoadBackground();
        }
        private void justBlackBtn_Click(object sender, RoutedEventArgs e){
            Properties.Settings.Default.background = "justblack";
            Properties.Settings.Default.Save();
            LoadBackground();
        }
    }
}