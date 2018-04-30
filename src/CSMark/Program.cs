/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using AluminiumCoreLib.Utilities;
using CSMarkLib;
using CSMarkLib.Results;
using Rollbar;
using System;
using System.Diagnostics;
using System.IO;

namespace CSMark {
    class Program {

        static void Main(string[] args){
            string stable_branch_URL = "https://gitlab.com/CSMarkBenchmark/CSMarkDesktop/raw/master/channels/stable.xml";
            string dev_branch_URL = "https://gitlab.com/CSMarkBenchmark/CSMarkDesktop/raw/master/channels/dev.xml";
            bool isStable = true;

            Platform platform = new Platform();
            string CSMarkVersion = platform.ReturnVersionString() + "_";
            BenchmarkController bench;
            StressTestController stress = new StressTestController();

            //Show license information
            platform.ShowLicenseInConsole("LicenseMessage.txt", 2500);

            CSMarkLib.UpdatingServices.AutoUpdater auto = new CSMarkLib.UpdatingServices.AutoUpdater();

            Stopwatch s = new Stopwatch();
            s.Start();

            if (isStable){
                auto.checkForUpdate(stable_branch_URL);
            }
            else{
                auto.checkForUpdate(dev_branch_URL);
            }

            try{
                while (s.ElapsedMilliseconds < 3000 && !auto.isCheckForUpdateCompleted()){
                }
            }
            catch(Exception ex){
                //Failed to check for updates.
                Console.WriteLine("CSMarkDesktop failed to check for updates.");
                Console.WriteLine(ex);
            }

            //Get .NET Core RID
            var rid = platform.ReturnDotNetCoreRID();
            if (rid.Contains("Windows ")){
                rid = rid.Replace("Windows ", "Win");
            }
            if (rid.Contains("macOS "))
            {
                rid = rid.Replace("macOS ", "osx");
            }
            if (rid.Contains("X64"))
            {
                rid = rid.Replace("X64", "x64");
            }
            if (rid.Contains("ARM"))
            {
                rid = rid.Replace("ARM", "arm");
            }
            if (rid.Contains("ARM64"))
            {
                rid = rid.Replace("ARM64", "arm64");
            }

            string downloadURL = auto.getDownloadURL() + rid + ".zip";
            string changelogURL = auto.getChangeLogURL();
            string installedVersion = auto.getInstalledVersionString();
            string latestVersion = auto.getLatestVersionString();

            //Protect against when we have no internet access.
            if (!installedVersion.Equals(latestVersion) && latestVersion != "0.0.0.0")
            {
                Console.WriteLine("A new version of CSMarkDesktop is available.");
                Console.WriteLine("                                            ");
                Console.WriteLine("Branch: Stable");
                Console.WriteLine("Latest Version: " + latestVersion);
                Console.WriteLine("Installed Version: " + installedVersion);
                Console.WriteLine("Changelog URL: " + changelogURL);
                Console.WriteLine("Download URL: " + downloadURL);
            }
            else{
                Console.WriteLine("Great news! CSMark is up to date.");
            }

            Console.WriteLine("                                                             ");
            CSMarkController cc = new CSMarkController();

            //Setup Rollbar Error Detection.
            //You will need to create your own file in this directory called "Rollbar.txt" with a Rollbar API key to use Rollbar error reporting.
            try{
                string postServerItemAccessToken = File.ReadAllText("Rollbar.txt");
                RollbarLocator.RollbarInstance.Configure(new RollbarConfig(postServerItemAccessToken) { Environment = "production" });
            }
            catch{
                Console.WriteLine("We were unable to setup Rollbar Error Reporting.");
            }

            string amount = "";
            string exitCommand = "exit";
            string stressCommand = "2";
            string aboutCommand = "about";
            string benchCommand = "0";
            string calcCommand = "_calc";
            string clearCommand = "clear";
            string helpCommand = "help";
            string respondYes = "yes";
            string respondNo = "no";
            string respondMilliseconds = "milliseconds";
            string respondSeconds = "seconds";
            string respondMinutes = "minutes";
            string respondHours = "hours";
            string respondStop = "stop";
            string respondBreak = "break";
            string confirmExit = "Are you sure you want to exit the application?";
            string responseYorN = "Please enter Yes or No.";
            string processCommand = "process";
            string terminate = "";

            string shutdownNotice = "Don't turn off your PC.";
     //       string timeNotice = "Estimated Time Required: ";
            string stressThread = "";
            bool singleThreadedStress = false;
            string privacy = "By using CSMarkDesktop, you agree to our Privacy Policy located at https://gitlab.com/CSMarkBenchmark/CSMarkDesktop/blob/master/PrivacyPolicy.md";

            string exitConfirmation = "";
            string newCommand = "";
            string timedStress = "";
            string choseTimed = "";

            Console.Title = "CSMarkDesktop " + platform.ReturnVersionString() + " Community Edition";
            Information.ConsoleWriteLineUsingOSColor("Welcome to CSMark Desktop Edition.");

            while (true){
                //Warn the user if the process count is quite high.
                platform.WarnProcessCount(200);

                Console.WriteLine("                                                                        ");
                Information.ConsoleWriteLineUsingOSColor(privacy);
                Information.ConsoleWriteUsingOSColor("To run the single threaded and multi threaded tests, please enter ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("0");
                Information.ConsoleWriteUsingOSColor("To run the stress test utility, please enter ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("2");
                Information.ConsoleWriteLineUsingOSColor("Please give feedback, or report bugs at our GitLab Issues page at https://www.gitlab.com/csmarkbenchmark/csmarkdesktop/issues .");
                Information.ConsoleWriteLineUsingOSColor("To find out what processes may be harming your CSMark score, please enter the 'process' command.");
                Console.ForegroundColor = ConsoleColor.Gray;

                newCommand = Console.ReadLine().ToLower();

                if (newCommand == stressCommand){
                    Information.ConsoleWriteLineUsingOSColor("Do you want to run a timed Stress Test?");
                    Information.ConsoleWriteLineUsingOSColor(responseYorN);
                    choseTimed = Console.ReadLine().ToLower();

                    if (choseTimed.Equals(respondNo)){
                        Information.ConsoleWriteLineUsingOSColor("Starting stress test.");
                        Information.ConsoleWriteLineUsingOSColor("To stop the stress test, please exit the program or enter STOP or BREAK.");

                        cc.untimedStress(false);

                        newCommand = Console.ReadLine().ToLower();
                        if (newCommand.Contains(respondBreak) || newCommand.Contains(respondStop)){
                           cc.untimedStress(true);
                        }
                        else{
                            cc.untimedStress(true);
                        }
                    }
                    else if (choseTimed.Equals(respondYes)){
                        Information.ConsoleWriteLineUsingOSColor("Select the time format in MILLISECONDS, SECONDS, MINUTES or HOURS.");
                        timedStress = Console.ReadLine().ToLower();

                        if (timedStress.ToLower().Equals("milliseconds") || timedStress.ToLower().Equals("seconds") || timedStress.ToLower().Equals("minutes") || timedStress.ToLower().Equals("hours"))
                        {
                            Information.ConsoleWriteLineUsingOSColor("How many " + timedStress + " would you like the stress test to run for?");
                            string stressTime = Console.ReadLine().ToLower();

                            if (timedStress == respondMilliseconds)
                            {
                                stress.StartStressTestMilliSeconds(double.Parse(stressTime), singleThreadedStress);
                            }
                            else if (timedStress == respondSeconds)
                            {
                                stress.startStressTestSeconds(double.Parse(stressTime), singleThreadedStress);
                            }
                            else if (timedStress == respondMinutes)
                            {
                                stress.startStressTestMinutes(double.Parse(stressTime), singleThreadedStress);
                            }
                            else if (timedStress == respondHours)
                            {
                                stress.startStressTestHours(double.Parse(stressTime), singleThreadedStress);
                            }
                        }
                    }
                }

                else if (newCommand.Contains(calcCommand)){
                    bench = new BenchmarkController();
                    Information.ConsoleWriteLineUsingOSColor("Please enter the amount of calculations you'd like to be performed in millions:");
                    amount = Console.ReadLine();

                    try{
                        bench.SetMaxIterations(double.Parse(amount) * 1000 * 1000);
                    }
                    catch (Exception ex)
                    {
                        Information.ConsoleWriteLineUsingOSColor("We ran into some issues.");
                        Information.ConsoleWriteLineUsingOSColor("Here are the details of the error in case you need it: ");
                        Information.ConsoleWriteLineUsingOSColor(ex.ToString());
                        RollbarLocator.RollbarInstance.Error(ex);
                        Console.ReadLine();
                    }
                }

                if (newCommand.ToLower().Contains(benchCommand.ToLower()))
                {
                    Information.ConsoleWriteLineUsingOSColor("Starting benchmarking test.");
                    Information.ConsoleWriteLineUsingOSColor(shutdownNotice);

                    if (!newCommand.Contains(calcCommand))
                    {
                    //do nothing
                    }

                    if (newCommand.Contains(benchCommand))
                    {
                        //Information.consoleWriteLineUsingOSColor(timeNotice + " " + bench.getEstimateTimeToFinish(true, true) + ".");

                        BenchmarkController b1 = new BenchmarkController();

                        Result x = CSMarkController.benchmark(b1);
                        CSMarkController.verify(b1, x);
                        CSMarkController.Save(x, responseYorN, platform.ReturnVersionString());
                    }
                }

                if (newCommand.ToLower().Equals(exitCommand.ToLower())){
                    Information.ConsoleWriteLineUsingOSColor(confirmExit);
                    Information.ConsoleWriteLineUsingOSColor(responseYorN);
                    exitConfirmation = Console.ReadLine().ToLower();

                    if (exitConfirmation.Equals(respondYes)){
                        Information.ConsoleWriteLineUsingOSColor(terminate);
                        Environment.Exit(0);
                    }
                }

                if (newCommand.ToLower().Contains("about") || newCommand.ToLower().Contains("credits") || newCommand.ToLower().Contains(helpCommand.ToLower())){
                    CSMarkController.ListCredits();
                }
                else if (newCommand.ToLower() == clearCommand.ToLower()){
                    Console.Clear();
                }
                else if (newCommand.ToLower().Contains(processCommand)){
                    Console.WriteLine("                                    ");
                    Console.WriteLine("                                    ");
                    new Platform().ListAllProcesses();
                }
                else if (!newCommand.Contains(exitCommand) && !newCommand.Contains(stressThread) && !newCommand.Contains(helpCommand) && !newCommand.Contains(benchCommand) && !newCommand.Contains(aboutCommand) && !newCommand.Contains(clearCommand)){
                    ConsoleColor beforeColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command not supported. Please enter a supported command.");
                    Console.ForegroundColor = beforeColor;
                }
            }
        }
    }
}
