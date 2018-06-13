/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace CSMarkDesktop.Windows
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window{
        private DispatcherTimer dispatcher;
        private int seconds = 2;

        public SplashScreen(){
            InitializeComponent();
            dispatcher = new DispatcherTimer();

            dispatcher.Tick += new EventHandler(dispatcher_Tick);

            dispatcher.Interval = new TimeSpan(0,0,0,seconds);
            dispatcher.Start();
        }
        private void dispatcher_Tick(object sender, EventArgs e){
            Hide();
            MainWindow main = new MainWindow();
            main.ShowDialog(); 
        }
    }
}