using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace FasterMaster
{
    public static class Program
    {
        static void Main(string[] args)
        {
            //RestartExplorer();
            //SetPCIdle();
            //DeleteTempFiles();
            gpupdate();
            //System.GC.Collect();
            //Console.ReadLine();
        }



        static void RestartExplorer()
        {
            foreach (Process p in Process.GetProcessesByName("explorer"))
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

        static void gpupdate()
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
    }
}
