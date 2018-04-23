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
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CSMark{
     class CSMarkController{
        StressTestController s = new StressTestController();

        public CSMarkController(){
            //Setup Rollbar Error Detection.
            //You will need to create your own file in this directory called "Rollbar.txt" with a Rollbar API key to use Rollbar error reporting.
            try{
                string postServerItemAccessToken = File.ReadAllText("Rollbar.txt");
                RollbarLocator.RollbarInstance.Configure(new RollbarConfig(postServerItemAccessToken) { Environment = "production" });
            }
            catch{
                Console.WriteLine("We were unable to setup Rollbar Error Reporting.");
            }
        }

        public static void ListCommands(){
            Information.ConsoleWriteLineUsingOSColor("List of commands:");
            Information.ConsoleWriteLineUsingOSColor("0");
            Information.ConsoleWriteLineUsingOSColor("1");
            Information.ConsoleWriteLineUsingOSColor("2");
            Information.ConsoleWriteLineUsingOSColor("clear");
            Information.ConsoleWriteLineUsingOSColor("about");
            Information.ConsoleWriteLineUsingOSColor("exit");
            Console.WriteLine("                                   ");
        }
        public static void ListCredits(){
            Platform platform = new Platform();
            Console.WriteLine("                                     ");
            Console.WriteLine("                                     ");
            string csmark_assemblyVersion = "";
            string alcorlib_assemblyVersion = "";
            string rollbar_assemblyVersion = "";

            #region Try to get the CSMarkLib version
            try
            {
                try
                {
                    csmark_assemblyVersion = Assembly.LoadFile(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "CSMarkLib.dll").GetName().Version.ToString();
                }
                catch
                {
                    csmark_assemblyVersion = Assembly.LoadFile(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "publish" + Path.DirectorySeparatorChar + "CSMarkLib.dll").GetName().Version.ToString();
                }
            }
            catch (Exception ex)
            {
                Information.ConsoleWriteLineUsingOSColor("We ran into some issues.");
                Information.ConsoleWriteLineUsingOSColor("Here are the details of the error in case you need it: ");
                Information.ConsoleWriteLineUsingOSColor(ex.ToString());
                csmark_assemblyVersion = "?";
                RollbarLocator.RollbarInstance.Error(ex);
            }
            #endregion

            #region Try to get the AluminiumCoreLib version
            try
            {
                try
                {
                    alcorlib_assemblyVersion = Assembly.LoadFile(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "AluminiumCoreLib.dll").GetName().Version.ToString();
                }
                catch
                {
                    alcorlib_assemblyVersion = Assembly.LoadFile(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "publish" + Path.DirectorySeparatorChar + "AluminiumCoreLib.dll").GetName().Version.ToString();
                }
            }
            catch (Exception ex)
            {
                Information.ConsoleWriteLineUsingOSColor("We ran into some issues.");
                Information.ConsoleWriteLineUsingOSColor("Here are the details of the error in case you need it: ");
                Information.ConsoleWriteLineUsingOSColor(ex.ToString());
                alcorlib_assemblyVersion = "?";
                RollbarLocator.RollbarInstance.Error(ex);
            }
            #endregion

            #region Try to get the Rollbar version
            try
            {
                try
                {
                    rollbar_assemblyVersion = Assembly.LoadFile(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Rollbar.dll").GetName().Version.ToString();
                }
                catch
                {
                    rollbar_assemblyVersion = Assembly.LoadFile(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "publish" + Path.DirectorySeparatorChar + "Rollbar.dll").GetName().Version.ToString();
                }
            }
            catch (Exception ex)
            {
                Information.ConsoleWriteLineUsingOSColor("We ran into some issues.");
                Information.ConsoleWriteLineUsingOSColor("Here are the details of the error in case you need it: ");
                Information.ConsoleWriteLineUsingOSColor(ex.ToString());
                rollbar_assemblyVersion = "?";
                RollbarLocator.RollbarInstance.Error(ex);
            }
            #endregion

            Console.WriteLine("                                   ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("CSMark Version: ");
            Information.ConsoleWriteLineUsingOSColor(platform.ReturnVersionString());
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Runtime ID: ");
            Information.ConsoleWriteLineUsingOSColor(platform.ReturnDotNetCoreRID());
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("CSMarkLib Version: ");
            Information.ConsoleWriteLineUsingOSColor(csmark_assemblyVersion);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("AluminiumCoreLib Version: ");
            Information.ConsoleWriteLineUsingOSColor(alcorlib_assemblyVersion);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Rollbar Version: ");
            Information.ConsoleWriteLineUsingOSColor(rollbar_assemblyVersion);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("OS ID: ");
            Information.ConsoleWriteLineUsingOSColor(RuntimeInformation.OSDescription);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Architecture ID: ");
            Information.ConsoleWriteLineUsingOSColor(platform.ReturnOSArchitecture());
            Console.WriteLine("                                   ");
            Console.WriteLine("                                   ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Credits:");
            Information.ConsoleWriteLineUsingOSColor("------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Developer: ");
            Information.ConsoleWriteLineUsingOSColor("AluminiumTech");
            Information.ConsoleWriteLineUsingOSColor("------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Community Contributions: ");
            Information.ConsoleWriteLineUsingOSColor("RaidMax (https://github.com/RaidMax)");
            Information.ConsoleWriteLineUsingOSColor("------------------------------------------------------------");
            Console.WriteLine("               ");
        }
        
        public static Result benchmark(BenchmarkController b){
                b.SetAutoMaxIterations();
                b.StartBenchmarkTests();

       //     Result r = b.SaveResult(true, true);
            b.PrintResultsToConsole(true, true, r);

            return r;
        }
        public static Result benchmarkSingle(BenchmarkController b){
            b.SetAutoMaxIterations();
            b.StartSingleBenchmarkTests();

     //       Result r = b.SaveResult(true, false);
            b.PrintResultsToConsole(true, false, r);

            return r;
        }
        public void untimedStress(bool terminate){
            if (terminate){
                s.StopStressTest();
            }
            else{
                s.StartMultiStressTest();
            }
        }
        public static void timedStressMilliseconds(double milliseconds){
            var s = new StressTestController();
            s.StartStressTestMilliSeconds(milliseconds, false);
        }
        public static void timedStressSeconds(double seconds){
            var s = new StressTestController();
            s.startStressTestSeconds(seconds, false);
        }
        public static void timedStressMinutes(double minutes){
            var s = new StressTestController();
            s.startStressTestMinutes(minutes, false);
        }
        public static void timedStressHours(double hours){
            var s = new StressTestController();
            s.startStressTestHours(hours, false);
        }

        public static void verify(BenchmarkController b, Result r){
            //Disable verification for now as it doesn't work in CSMarkLib 2.0.0 Preview 1.
            //  string verify = b.verifyResults(r, true, false).ToString();

          /*  if (verify.ToLower().Equals("validresult"))
            {
                Console.WriteLine("                                          ");
                Information.consoleWriteLineUsingOSColor("Your result has been verified and is Valid.");
            }
            else if (verify.ToLower().Equals("invalidresult"))
            {
                Console.WriteLine("                                          ");
                Information.consoleWriteLineUsingOSColor("Your result has been verified and is is INVALID.");
            }
            else if (verify.ToLower().Equals("verificationfailed"))
            {
                Console.WriteLine("                                          ");
                Information.consoleWriteLineUsingOSColor("Your result has not been verified due to Verification Failure.");
            }
            */
        }
        public static void Save(Result r, string responseYorN, string CSMarkVersion){
            Console.WriteLine("                                                ");
            Information.ConsoleWriteLineUsingOSColor("Would you like to save your result as a Text File?");
            Information.ConsoleWriteLineUsingOSColor(responseYorN);
            string save = Console.ReadLine().ToLower();

            if (save.ToLower() == "yes" || save.ToLower().Contains("y")){
                ResultSaver rs = new ResultSaver();
                rs.SaveToTextFile(CSMarkVersion, r);
            }
        }
    }
}
