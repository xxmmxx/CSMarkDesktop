/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using AluminiumCoreLib.Utilities;
using System;
using System.Diagnostics;
using System.IO;

namespace CSMark{
   static class Information{
        /// <summary>
        /// Change to the appropriate Console Color for the OS.
        /// </summary>
        public static void ConsoleWriteLineUsingOSColor(string message){
            ConsoleColor beforeColor = Console.ForegroundColor;
            ConsoleColor os = new ConsoleColor();
            Platform platform = new Platform();

            if (platform.ReturnDotNetCoreRID().ToLower().Contains("win")){
                os = ConsoleColor.Gray;
            }
            else if(platform.ReturnDotNetCoreRID().ToLower().Contains("mac")){
                os = ConsoleColor.Black;
            }
            else if(platform.ReturnDotNetCoreRID().ToLower().Contains("linux")){
                os = ConsoleColor.Gray;
            }

            Console.ForegroundColor = os;
            Console.WriteLine(message);
            Console.ForegroundColor = beforeColor;
        }
        /// <summary>
        /// Change to the appropriate Console Color for the OS.
        /// </summary>
        public static void ConsoleWriteUsingOSColor(string message)
        {
            ConsoleColor beforeColor = Console.ForegroundColor;
            ConsoleColor os = new ConsoleColor();
            Platform platform = new Platform();

            if (platform.ReturnDotNetCoreRID().ToLower().Contains("win"))
            {
                os = ConsoleColor.Gray;
            }
            else if (platform.ReturnDotNetCoreRID().ToLower().Contains("mac"))
            {
                os = ConsoleColor.Black;
            }
            else if (platform.ReturnDotNetCoreRID().ToLower().Contains("linux"))
            {
                os = ConsoleColor.Gray;
            }

            Console.ForegroundColor = os;
            Console.Write(message);
            Console.ForegroundColor = beforeColor;
        }
    }
}
