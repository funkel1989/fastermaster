using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FasterMaster
{
    public static class Program
    {
        static void Main(string[] args)
        {
            RestartExplorer();
            SetPCIdle();
            DeleteTempFiles();

            System.GC.Collect();
            Console.ReadLine();
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
    }
}
