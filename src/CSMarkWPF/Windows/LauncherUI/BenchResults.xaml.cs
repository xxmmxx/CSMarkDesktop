/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
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

namespace CSMarkDesktop.Windows.LauncherUI{
    /// <summary>
    /// Interaction logic for BenchResults.xaml
    /// </summary>
    public partial class BenchResults : Window{

        private SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(125, 244, 66));

        private SolidColorBrush black = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));
        private SolidColorBrush blueDark = new SolidColorBrush(Color.FromRgb(43, 76, 119));
        private SolidColorBrush blurple = new SolidColorBrush(Color.FromRgb(114, 137, 218));

        public BenchResults(){
            InitializeComponent();
            Brush foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                if (Properties.Settings.Default.background.Equals("reallydark"))
                {
                    Background = reallyDark;
                }
                if (Properties.Settings.Default.background.Equals("dark"))
                {
                    Background = dark;
                }
                if (Properties.Settings.Default.background.Equals("bluedark"))
                {
                    Background = blueDark;
                }
                if (Properties.Settings.Default.background.Equals("blurple"))
                {
                    Background = blurple;
                }
                if (Properties.Settings.Default.background.Equals("justblack"))
                {
                    Background = black;
                }

                //Load the Background colors
            gridColor.Background = Background;
            processor.Content += Properties.Results.Default.Processor;

            singleOverall.Background = Background;
            multiOverall.Background = Background;
            singleOverall.Foreground = foreground;
            multiOverall.Foreground = foreground;

            pythagorasSingle.Background = Background;
            pythagorasMulti.Background = Background;
            geometricSumNSingle.Background = Background;
            geometricSumNMulti.Background = Background;
            compoundInterestSingle.Background = Background;
            compoundInterestMulti.Background = Background;
            changeReturnSingle.Background = Background;
            changeReturnMulti.Background = Background;

            pythagorasSingle.Foreground = foreground;
            pythagorasMulti.Foreground = foreground;
            geometricSumNSingle.Foreground = foreground;
            geometricSumNMulti.Foreground = foreground;
            compoundInterestSingle.Foreground = foreground;
            compoundInterestMulti.Foreground = foreground;
            changeReturnSingle.Foreground = foreground;
            changeReturnMulti.Foreground = foreground;

            singleOverallInfo.Background = Background;
            multiOverallInfo.Background = Background;

            Brush colorBrush;

            if (Background.Equals(blurple)){
                colorBrush = blueDark;
            }
            else if (Background.Equals(blueDark)){
                colorBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            else if (Background.Equals(dark) || Background.Equals(reallyDark) || Background.Equals(black)){
                colorBrush = myGreenBrush;
            }
            else{
                colorBrush = dark;
            }

            singleOverallInfo.Foreground = colorBrush;
            multiOverallInfo.Foreground = colorBrush;

            processor.Foreground = foreground;
            processor.Background = Background;
            processorCoreCount.Background = Background;
            processorCoreCount.Foreground = foreground;
            processorThreadCount.Background = Background;
            processorThreadCount.Foreground = foreground;

            scoreBreakdown.Background = Background;
            scoreBreakdown.Foreground = foreground;

            benchRunTime.Background = Background;
            benchRunTime.Foreground = foreground;

            var result = Properties.Results.Default.BenchmarkResult;
            pythagorasSingle.Content += result.GetPythagorasSingle().ToString();
            pythagorasMulti.Content += result.GetPythagorasMulti().ToString();
            geometricSumNSingle.Content += result.GetGeometricSumNSingle().ToString();
            geometricSumNMulti.Content += result.GetGeometricSumNMulti().ToString();
            compoundInterestSingle.Content += result.GetCompoundInterestSingle().ToString();
            compoundInterestMulti.Content += result.GetCompoundInterestMulti().ToString();
            changeReturnSingle.Content += result.GetChangeReturnSingle().ToString();
            changeReturnMulti.Content += result.GetChangeReturnMulti().ToString();
            processorCoreCount.Content += Properties.Results.Default.CPUCoreCount;
            processorThreadCount.Content += Properties.Results.Default.CPUThreadCount;
            singleOverallInfo.Content = result.GetOverallSingle().ToString() + " CSMark Points";
            multiOverallInfo.Content = result.GetOverallMulti().ToString() + " CSMark Points";           
        }
    }
}