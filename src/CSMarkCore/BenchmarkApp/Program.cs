﻿/*  Copyright 2017-2018 AluminiumTech
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

namespace CSMarkCoreBenchmarkApp{
    class Program{
        enum BenchCommand{
            MultiBenchSaveResult,
            SingleBenchSaveResult
        }
        enum ResultFileType{
            NoResult,
            Text,
            Xml,
            Json
        }
        
        static void Main(string[] args){
            Platform platform = new Platform();
            Console.Title = "CSMarkCore BenchmarkApp v" + platform.ReturnVersionString() + " Community Edition";
            BenchCommand command = BenchCommand.MultiBenchSaveResult;
            ResultFileType rft = ResultFileType.Text;
            DateTime startTime = DateTime.Now;
            DateTime stopTime = DateTime.Now;

            //Accept command line arguments
            if (args.Length == 0){
                //If the user doesn't specify the command, just start the stress test.
                command = BenchCommand.MultiBenchSaveResult;
                rft = ResultFileType.Text;
            }
            // If the user has given 1 arguments it should correspond to 1) Single or Multi
            else if (args.Length == 1 && args[0].ToString().Contains("single")){
                command = BenchCommand.SingleBenchSaveResult;
                rft = ResultFileType.Text;
            }
            // If the user has given 1 arguments it should correspond to 1) Single or Multi
            else if (args.Length == 1 && args[0].ToString().Contains("multi")){
                command = BenchCommand.MultiBenchSaveResult;
                rft = ResultFileType.Text;
            }
            //If the user has given 2 arguments they should correspond to 1) Single or Multi and 2) Start Time.
            else if (args.Length == 2){
                if (args[0].ToString().Contains("multi")){
                    command = BenchCommand.MultiBenchSaveResult;
                }
                else{
                    command = BenchCommand.SingleBenchSaveResult;
                }

                if (args[1].ToString().Contains("xml")){
                    rft = ResultFileType.Xml;
                }
                else if (args[1].ToString().Contains("text")) {
                    rft = ResultFileType.Text;
                }
                else if (args[1].ToString().Contains("json"))
                {
                    rft = ResultFileType.Json;
                }                
            }
            //If the user has given 2 arguments they should correspond to 1) Single or Multi and 3) Start Time.
            else if (args.Length == 3){

                if (args[0].ToString().Contains("multi")){
                    command = BenchCommand.MultiBenchSaveResult;
                }
                else{
                    command = BenchCommand.SingleBenchSaveResult;
                }

                if (args[1].ToString().Contains("xml"))
                {
                    rft = ResultFileType.Xml;
                }
                else if (args[1].ToString().Contains("text"))
                {
                    rft = ResultFileType.Text;
                }
                else if (args[1].ToString().Contains("json"))
                {
                    rft = ResultFileType.Json;
                }

                startTime = Convert.ToDateTime(args[2]);
            }

            //Start the Benchmark Application.
            string CSMarkVersion = platform.ReturnVersionString() + "_";
            BenchmarkController bench = new BenchmarkController();
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
            platform.WarnProcessCount(200, "Warning: High Process Count Detected. This may affect your benchmarking scores.");

            Console.WriteLine("                                                                        ");
            Console.WriteLine("By using CSMarkCore, you agree to our Privacy Policy located at");
            Console.WriteLine(" https://github.com/CSMarkBenchmark/CSMarkDesktop/blob/master/PrivacyPolicy.md");
            Console.WriteLine("                                                                        ");
            Console.WriteLine(shutdownNotice);
            Console.WriteLine("                                                                        ");

            Console.WriteLine("Detecting CLI Arguments...");
            Console.WriteLine("Starting Benchmarks...");

            ResultSaver rs = new ResultSaver();
            string dir = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Results";

            if (command == BenchCommand.SingleBenchSaveResult){
            //    bench.DoWarmup(true);
                bench.StartSingleBenchmarkTests();
                var y = bench.ReturnBenchmarkObjects();
                Result x = new ResultSaver().SaveResult(false, y);
                bench.PrintResultsToConsole(false, x);
                bench.VerifyBenchmarkIntegrity(x, false);

                if (rft.Equals(ResultFileType.NoResult)){
                    //Do nothing
                }
                else if (rft.Equals(ResultFileType.Text)){
                    rs.SaveToTextFile(dir, platform.ReturnVersionString(), x);
                }
                else if (rft.Equals(ResultFileType.Json)){
                    rs.SaveToJSONFile(dir, platform.ReturnVersionString(), x);
                }
                else if (rft.Equals(ResultFileType.Xml)){
                    rs.SaveToXMLFile(dir, platform.ReturnVersionString(), x);
                }
            }
            else if (command == BenchCommand.MultiBenchSaveResult){
                bench.StartBenchmarkTests();
                var y = bench.ReturnBenchmarkObjects();
                Result x = new ResultSaver().SaveResult(true, y);
                bench.PrintResultsToConsole(true, x);
                bench.VerifyBenchmarkIntegrity(x, true);

                if (rft.Equals(ResultFileType.NoResult)){
                    //Do nothing
                }
                else if (rft.Equals(ResultFileType.Text)){
                    rs.SaveToTextFile(dir, platform.ReturnVersionString(), x);
                }
                else if (rft.Equals(ResultFileType.Json)){
                    rs.SaveToJSONFile(dir, platform.ReturnVersionString(), x);
                }
                else if (rft.Equals(ResultFileType.Xml)){
                    rs.SaveToXMLFile(dir, platform.ReturnVersionString(), x);
                }
            }

            Console.WriteLine("To exit this application, press ENTER.");
            Console.ReadLine();
        }
    }
}