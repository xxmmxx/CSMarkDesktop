/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using AluminiumCoreLib.Utilities;
using System;

namespace BenchmarkApp{
    class Program{
        static void Main(string[] args)
        {
            Platform platform = new Platform();
            Console.Title = "CSMarkDesktop BenchmarkApp v" + platform.ReturnVersionString() + " Community Edition";
        }
    }
}