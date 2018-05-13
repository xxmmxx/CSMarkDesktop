/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using AluminiumCoreLib.Utilities;
using CSMarkLib;
using Rollbar;
using System;
using System.IO;

namespace CSMarkDesktopStressTestApp{
    class Program{
        static void Main(string[] args){
            /// Accept command line arguments here.

            Platform platform = new Platform();
            Console.Title = "CSMarkDesktop Stress Test App v" + platform.ReturnVersionString();
            
            StressTestController stress = new StressTestController();

            //Show license information
            platform.ShowLicenseInConsole("LicenseMessage.txt", 3000);

            //Setup Rollbar Error Detection.
            //You will need to create your own file in this directory called "Rollbar.txt" with a Rollbar API key to use Rollbar error reporting.
            try{
                string postServerItemAccessToken = File.ReadAllText("Rollbar.txt");
                RollbarLocator.RollbarInstance.Configure(new RollbarConfig(postServerItemAccessToken) { Environment = "production" });
            }
            catch{
                Console.WriteLine("We were unable to setup Rollbar Error Reporting.");
            }

            Console.WriteLine("                                                                        ");
            Console.WriteLine("By using CSMarkDesktop, you agree to our Privacy Policy located");
            Console.WriteLine(" at https://github.com/CSMarkBenchmark/CSMarkDesktop/blob/master/PrivacyPolicy.md");
            Console.WriteLine("                                                                        ");

            Console.WriteLine("Starting Stress Test. To terminate the stress test, please exit this application window or press enter.");

            stress.StartMultiStressTest();

            Console.ReadLine();
        }
    }
}