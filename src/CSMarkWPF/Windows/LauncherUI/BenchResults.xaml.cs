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
            processor.Foreground = foreground;
            processor.Background = background;

            var result = Properties.Results.Default.BenchmarkResult;
            singleOverall.Content += result.GetOverallSingle().ToString() + " CSMark Points";
            multiOverall.Content += result.GetOverallMulti().ToString() + " CSMark Points";
        }
    }
}