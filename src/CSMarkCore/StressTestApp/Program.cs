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

namespace CSMarkCoreStressTestApp{
    class Program{
        enum Command{
            StressTest,
            StressTest_AndStartTime,
            StressTest_AndStartAndEndTime,
        }
        static void Main(string[] args){
            Platform platform = new Platform();
            Console.Title = "CSMarkCore StressTest App v" + platform.ReturnVersionString() + " Community Edition";

            Command command = Command.StressTest;
            DateTime startTime = DateTime.Now;
            DateTime stopTime = DateTime.Now;
            StressTestController stc = new StressTestController();

            //Accept command line arguments
            if (args.Length == 0){
                //If the user doesn't specify the command, just start the stress test.
                command = Command.StressTest;
            }
            // If the user has given 1 arguments they should correspond to 1) Start Time.
            else if (args.Length == 1){
                command = Command.StressTest_AndStartTime;
                startTime = Convert.ToDateTime(args[0]);
            }
            //If the user has given 2 arguments they should correspond to 1) Start Time and 2) End Time.
            else if (args.Length == 2){
                command = Command.StressTest_AndStartAndEndTime;
                startTime = Convert.ToDateTime(args[0]);
                stopTime = Convert.ToDateTime(args[1]);
            }

            string CSMarkVersion = platform.ReturnVersionString() + "_";
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

            string shutdownNotice = "Don't turn off your PC.";
            //Warn the user if the process count is quite high.
            platform.WarnProcessCount(200, "Warning: High Process Count Detected.");

            Console.WriteLine("                                                                        ");
            Console.WriteLine("By using CSMarkCore, you agree to our Privacy Policy located at");
            Console.WriteLine(" https://github.com/CSMarkBenchmark/CSMarkDesktop/blob/master/PrivacyPolicy.md");
            Console.WriteLine("                                                                        ");
            Console.WriteLine(shutdownNotice);
            Console.WriteLine("                                                                        ");

            Console.WriteLine("Detecting CLI Arguments...");
            Console.WriteLine("Starting Stress Test...");

            if(command == Command.StressTest){
                //Start the Stress Test
                 stc.StartMultiStressTest();
            }
            else if (command == Command.StressTest_AndStartTime){
                //Start the stress test at the start time.
                stc.StartMultiStressTest(startTime);
            }
            else if(command == Command.StressTest_AndStartAndEndTime){
                //Start the stress test at the start time.
                stc.StartMultiStressTest(startTime);
                //Stop the stress test at the stop time.
                stc.StopStressTest(stopTime);
            }

            Console.WriteLine("To exit this application, press ENTER.");
            Console.ReadLine();
            stc.StopStressTest();
        }
    }
}