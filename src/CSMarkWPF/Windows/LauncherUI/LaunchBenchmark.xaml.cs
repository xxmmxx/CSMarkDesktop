/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using AluminiumCoreLib.Utilities;
using CSMarkLib;
using CSMarkLib.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CSMarkDesktop.Windows.LauncherUI{
    /// <summary>
    /// Interaction logic for LaunchBenchmark.xaml
    /// </summary>
    public partial class LaunchBenchmark : Window{
        Brush background;
        Brush foreground;
        string AppVersion;
        DispatcherTimer dispatcher = new DispatcherTimer();

        public LaunchBenchmark(string AppVersion, Brush background, Brush foreground){
            InitializeComponent();
            this.foreground = foreground;
            this.background = background;
            this.AppVersion = AppVersion;

            //Load the background colors
            gridColor.Background = background;
            info.Background = background;
            info.Foreground = foreground;

            var benchController = new BenchmarkController();
            var task = new Task(() => benchController.StartBenchmarkTests());
            task.Start();

            task.Wait((60 * 5) * 1000);

            string dir = Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + "results";

            try
            {
                Directory.CreateDirectory(dir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "An error occured");
            }

            HashMap<BenchmarkType, Benchmark> hash = benchController.ReturnBenchmarkObjects();
            var resultSaver = new ResultSaver();
            var result = resultSaver.SaveResult(true, hash);
            resultSaver.SaveToTextFile(dir, AppVersion, result);

            Properties.Results.Default.BenchmarkResult = result;
            AluminiumCoreLib.Hardware.Processor cpu = new AluminiumCoreLib.Hardware.Processor();
            cpu.GetProcessorInformationAsTask();
            Thread.Sleep(1000);
            Properties.Results.Default.Processor = cpu.CPU;
            Properties.Results.Default.Save();

            Hide();
            var window = new BenchResults(background, foreground);
            window.ShowDialog();
        }
    }
}