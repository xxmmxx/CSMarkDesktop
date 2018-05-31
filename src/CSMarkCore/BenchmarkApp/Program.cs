/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using AluminiumCoreLib.Utilities;
using CSMarkLib;
using System;

namespace BenchmarkApp{
    class Program{
        enum Command{
            MultiBenchSaveResult,
            MultiBenchSaveResultJson,
            MultiBenchSaveResultTxt,
            MultiBenchDoNotSaveResult,
            SingleBenchSaveResult,
            SingleBenchSaveResultJson,
            SingleBenchSaveResultTxt,
            SingleBenchDoNotSaveResult,
        }
        static void Main(string[] args){
            Platform platform = new Platform();
            Console.Title = "CSMarkCore BenchmarkApp v" + platform.ReturnVersionString() + " Community Edition";

            Command command = Command.MultiBenchSaveResult;
            DateTime startTime = DateTime.Now;
            DateTime stopTime = DateTime.Now;
            BenchmarkController bench = new BenchmarkController();

            //Accept command line arguments
            if (args.Length == 0)
            {
                //If the user doesn't specify the command, just start the stress test.
                command = Command.MultiBenchSaveResult;
            }
            // If the user has given 1 arguments they should correspond to 1) Start Time.
            else if (args.Length == 1)
            {
                command = Command.StressTest_AndStartTime;
                startTime = Convert.ToDateTime(args[0]);
            }
            //If the user has given 2 arguments they should correspond to 1) Start Time and 2) End Time.
            else if (args.Length == 2)
            {
                command = Command.StressTest_AndStartAndEndTime;
                startTime = Convert.ToDateTime(args[0]);
                stopTime = Convert.ToDateTime(args[1]);
            }
        }
    }
}
