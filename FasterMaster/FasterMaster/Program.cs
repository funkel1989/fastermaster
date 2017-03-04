using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;

namespace FasterMaster
{
    public static class Program
    {
        static void Main(string[] args)
        {
            int userInput = 0;

            

            do
            {
                userInput = DisplayMenu();

                if (userInput == 1)
                {
                    QuickClean();
                }
                else if (userInput == 2)
                {
                    DeepClean();
                }
                else if (userInput == 3)
                {
                    SuperClean();
                }
            } while (userInput != 4);

            
           

        }

        static public int DisplayMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine("1. Quick Clean");
                Console.WriteLine("2. Deep Clean");
                Console.WriteLine("3. Super Clean (admin needed)");
                Console.WriteLine("4. exit");
                int result;
                if (Int32.TryParse(Console.ReadLine(), out result))
                    return Convert.ToInt32(result);
                else
                    Console.WriteLine("Please enter a number");
            }
        }

        static void QuickClean()
        {
            RestartExplorer();
            killInternetExplorer();
            SetPCIdle();
            cleanIETempFiles();
            cleanIEcookies();
            cleanIEFormData();
            DeleteTempFiles();
            Console.WriteLine("quickclean");
            Console.ReadLine();
        }

        static void DeepClean()
        {
            RestartExplorer();
            killInternetExplorer();
            SetPCIdle();
            cleanIETempFiles();
            cleanIEcookies();
            cleanIEFormData();
            DeleteTempFiles();
            GPUpdate();
            Console.WriteLine("deepclean");
            Console.ReadLine();
        }

        static void SuperClean()
        {
            Console.WriteLine("superclean");
            Console.ReadLine();
        }

        static void GatherInformation()
        {
            //get computer name
            string computername = System.Environment.MachineName;
            //get IP address
            string ipaddress = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();

            SystemInfo si = new SystemInfo();       //Create an object of SystemInfo class.
            si.getOperatingSystemInfo();            //Call get operating system info method which will display operating system information.
            si.getProcessorInfo();                  //Call get  processor info method which will display processor info.
        }

        static void SendDataToAdmin()
        {

        }

        static void RestartExplorer()
        {
            foreach (Process p in Process.GetProcessesByName("explorer"))
            {
                p.Kill();
            }
        }

        static void killInternetExplorer()
        {
            foreach (Process p in Process.GetProcessesByName("iexplore"))
            {
                p.Kill();
            }
        }

        static void SetPCIdle()
        {
            ProcessStartInfo setIdle = new ProcessStartInfo();
            setIdle.FileName = @"C:\windows\SysWOW64\rundll32.exe";
            setIdle.Arguments = @"advapi32.dll ProcessIdleTasks";
            Process.Start(setIdle);
        }

        static void cleanIETempFiles()
        {
            ProcessStartInfo setIdle = new ProcessStartInfo();
            setIdle.FileName = @"C:\windows\SysWOW64\rundll32.exe";
            setIdle.Arguments = @"InetCpl.cpl ClearMyTracksByProcess 8";
            Process.Start(setIdle);
        }

        static void cleanIEcookies()
        {
            ProcessStartInfo setIdle = new ProcessStartInfo();
            setIdle.FileName = @"C:\windows\SysWOW64\rundll32.exe";
            setIdle.Arguments = @"InetCpl.cpl ClearMyTracksByProcess 2";
            Process.Start(setIdle);
        }

        static void cleanIEFormData()
        {
            ProcessStartInfo setIdle = new ProcessStartInfo();
            setIdle.FileName = @"C:\windows\SysWOW64\rundll32.exe";
            setIdle.Arguments = @"InetCpl.cpl ClearMyTracksByProcess 16";
            Process.Start(setIdle);
        }



        static void DeleteTempFiles()
        {
            string folderPath = string.Empty;
            folderPath = System.Environment.GetEnvironmentVariable("temp");

            List<string> directories = new List<string>();
            List<string> files = new List<string>();
            string sourceDir = folderPath;

            FileHelper.GetDirectoriesAndFiles(sourceDir, directories, files);


            foreach (string file in files)
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch { }


            }

            foreach (string directory in directories)
            {
                try
                {
                    Directory.Delete(directory);
                }
                catch { }

            }
        }

        static void GPUpdate()
        {
            FileInfo grouppolicyfile = new FileInfo("gpupdate.exe");
            Process proc = new Process();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = grouppolicyfile.Name;
            proc.StartInfo.Arguments = "/force";
            proc.Start();
            //program stops while GPupdate runs

            while (!proc.HasExited)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            Console.WriteLine("Group Policy Has been updated");            
        }

        static void ScheduleCurrentUserProfileDelete()
        {

        }

        static void SetPolicyClearOldProfiles()
        {

        }

        static void DeleteUserRegKey()
        {

        }

       
    }
}
