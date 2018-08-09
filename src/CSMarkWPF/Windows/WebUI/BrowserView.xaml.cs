/* Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using AluminiumCoreLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace CSMarkDesktop.Windows.WebUI
{
    /// <summary>
    /// Interaction logic for BrowserView.xaml
    /// </summary>
    public partial class BrowserView : Window{

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        private static bool IsInternetAvailable()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }

        public BrowserView(string Uri, int WindowHeight, int WindowWidth){
            InitializeComponent();          
            Height = WindowHeight;
            Width = WindowWidth;

            if (IsInternetAvailable() == true) {
                wvc.Navigate(new Uri(Uri));
            }
            else
            {
                var x = Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "Windows" + System.IO.Path.DirectorySeparatorChar +  "WebUI" + System.IO.Path.DirectorySeparatorChar + "404" + System.IO.Path.DirectorySeparatorChar + "index.html";
                new Platform().OpenURLInBrowser(x);
                //wvc.Navigate(new Uri(x));
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            wvc.Dispose();
        }
    }
}