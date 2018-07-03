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
        Brush background;
        Brush foreground;

        private SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(125, 244, 66));

        private SolidColorBrush black = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));
        private SolidColorBrush blueDark = new SolidColorBrush(Color.FromRgb(43, 76, 119));
        private SolidColorBrush blueGray = new SolidColorBrush(Color.FromRgb(80, 148, 237));
        private SolidColorBrush blurple = new SolidColorBrush(Color.FromRgb(114, 137, 218));

        public BenchResults(Brush background, Brush foreground){
            InitializeComponent();

            this.foreground = foreground;
            this.background = background;
            //Load the background colors
            gridColor.Background = background;
            processor.Content += Properties.Results.Default.Processor;

            singleOverall.Background = background;
            multiOverall.Background = background;
            singleOverall.Foreground = foreground;
            multiOverall.Foreground = foreground;

            pythagorasSingle.Background = background;
            pythagorasMulti.Background = background;
            geometricSumNSingle.Background = background;
            geometricSumNMulti.Background = background;
            compoundInterestSingle.Background = background;
            compoundInterestMulti.Background = background;
            changeReturnSingle.Background = background;
            changeReturnMulti.Background = background;

            pythagorasSingle.Foreground = foreground;
            pythagorasMulti.Foreground = foreground;
            geometricSumNSingle.Foreground = foreground;
            geometricSumNMulti.Foreground = foreground;
            compoundInterestSingle.Foreground = foreground;
            compoundInterestMulti.Foreground = foreground;
            changeReturnSingle.Foreground = foreground;
            changeReturnMulti.Foreground = foreground;

            singleOverallInfo.Background = background;
            multiOverallInfo.Background = background;

            Brush colorBrush;

            if (Background.Equals(blurple)){
                colorBrush = blueDark;
            }
            else if (Background.Equals(blueGray)){
                colorBrush = blueDark;
            }
            else if (Background.Equals(blueDark)){
                colorBrush = blueGray;
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
            processor.Background = background;
            processorCoreCount.Background = background;
            processorCoreCount.Foreground = foreground;
            processorThreadCount.Background = background;
            processorThreadCount.Foreground = foreground;

            scoreBreakdown.Background = background;
            scoreBreakdown.Foreground = foreground;

            benchRunTime.Background = background;
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