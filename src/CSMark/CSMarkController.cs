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

        public static void GetVersions()
        {
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

        }
        
        public static Result benchmark(BenchmarkController b){
                b.StartBenchmarkTests();
                  
            HashMap<string, Benchmark> hash = b.ReturnBenchmarkObjects();
            Result r = new ResultSaver().SaveResult(true, hash);
            b.VerifyBenchmarkIntegrity(r, true);
            b.PrintResultsToConsole(true, true, r);

            return r;
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
