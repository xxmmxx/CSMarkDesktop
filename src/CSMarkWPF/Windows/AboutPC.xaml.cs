/*  Copyright 2017-2018 AluminiumTech
   This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
  */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CSMarkDesktop.Windows
{
    /// <summary>
    /// Interaction logic for AboutPC.xaml
    /// </summary>
    public partial class AboutPC : Window{
        private SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(125, 244, 66));
        private SolidColorBrush myRedBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        private SolidColorBrush black = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private SolidColorBrush reallyDark = new SolidColorBrush(Color.FromRgb(35, 39, 42));
        private SolidColorBrush dark = new SolidColorBrush(Color.FromRgb(44, 47, 51));

        private AluminiumCoreLib.Hardware.Processor cpu = new AluminiumCoreLib.Hardware.Processor();
        private AluminiumCoreLib.Hardware.OperatingSystem os = new AluminiumCoreLib.Hardware.OperatingSystem();
        private AluminiumCoreLib.Hardware.OperatingSystemAdvanced osAdvanced = new AluminiumCoreLib.Hardware.OperatingSystemAdvanced();
        private AluminiumCoreLib.Hardware.Memory mem = new AluminiumCoreLib.Hardware.Memory();

        public AboutPC(){
            InitializeComponent();
            LoadBackground();
            LoadSpecs();
        }

        public void LoadBackground(){
            if (Properties.Settings.Default.background.Equals("reallydark")){
                gridColour.Background = reallyDark;
            }
            else if (Properties.Settings.Default.background.Equals("dark")){
                gridColour.Background = dark;
            }
            else if (Properties.Settings.Default.background.Equals("justblack")){
                gridColour.Background = black;
            }
            else{
                gridColour.Background = dark;
            }

            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ramLabel.Foreground = Foreground;
            title.Foreground = Foreground;
            threadCountLabel.Foreground = Foreground;
            coreCountLabel.Foreground = Foreground;
            cpuLabel.Foreground = Foreground;
            l2CacheLabel.Foreground = Foreground;
            l3CacheLabel.Foreground = Foreground;
            cpuClockSpeedLabel.Foreground = Foreground;
            osVersionLabel.Foreground = Foreground;
            osLabel.Foreground = Foreground;
            socketLabel.Foreground = Foreground;
            hwVirtualizationLabel.Foreground = Foreground;
            manufacturerLabel.Foreground = Foreground;
            gpuLabel.Foreground = Foreground;
        }

        public void LoadSpecs(){
            //Detect the hardware on the user's PC.
            cpu.GetProcessorInformationAsTask();
            os.GetWindowsInfoAsTask();
            osAdvanced.GetAdvancedOSInfoAsTask();            
            mem.GetMemoryInformationAsTask();

            ramLabel.Content = "RAM: " + mem.Available_ram + " Available   |   " + mem.Total_ram + " Installed";
            coreCountLabel.Content = "Core Count: " + cpu.CoreCountInt.ToString();
            threadCountLabel.Content = "Thread Count: " + cpu.ThreadCountInt.ToString();
            cpuLabel.Content = "Processor: " + cpu.CPU;

            if((cpu.L2CacheSizeKB / 1000) < 1){
                l2CacheLabel.Content = "L2 Cache: " + cpu.L2CacheSizeKB.ToString() + "KB";
            }
            else if((cpu.L2CacheSizeKB / 1000) >= 1){
                l2CacheLabel.Content = "L2 Cache: " + (cpu.L2CacheSizeKB / 1000).ToString() + "MB";
            }
            
            l3CacheLabel.Content = "L3 Cache: " + cpu.L3CacheSizeMB.ToString() + "MB";
            socketLabel.Content = "Socket: " + cpu.Socket;
            hwVirtualizationLabel.Content = "Hardware Virtualization Enabled: " + cpu.Virtualization; 
            cpuClockSpeedLabel.Content = "Base Clockspeed: " + cpu.ClockspeedInt.ToString() + "MHz";
            osVersionLabel.Content = "OS Version: " + Environment.OSVersion.Version + " (v" + os.Release + ")";
            osLabel.Content = "OS: " + os.ProductName + " " + osAdvanced.Bitness;
            manufacturerLabel.Content = "Processor Manufacturer: " + cpu.Manufacturer;

            try{
                string GPU = "";
                UInt32 vram = 0;

                ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + "Win32_VideoController");
                foreach (ManagementObject mj in mos.Get()){
                    GPU = Convert.ToString(mj["Name"]);

                    try{
                        vram = Convert.ToUInt32(mj["AdapterRAM"]);
                        //Convert from Bytes to KB
                        vram = vram / 1024;
                        //Convert from KB to MB
                        vram = vram / 1000;
                        //Convert from MB to GB
                        vram = vram / 1000;

                        if (vram <= 4){
                            gpuLabel.Content += " with " + vram + " of Graphics Memory";
                        }
                    }
                    catch{
                        vram = 0;
                    }
                }      
                gpuLabel.Content = "Graphics Processor: ";

                if (GPU.Contains("Radeon") || GPU.Contains("FirePro")){
                    gpuLabel.Content += "AMD";
                }
                else if(GPU.Contains("GeForce") || GPU.Contains("Quadro")){
                    gpuLabel.Content += "Nvidia";
                }
                else if (GPU.Contains("Adreno")){
                    gpuLabel.Content += "Qualcomm";
                }

                gpuLabel.Content += " " + GPU;
            }
            catch(Exception ex)
            {
                gpuLabel.Content = "Graphics Processor: " + ex;
            }           
        }
    }
}