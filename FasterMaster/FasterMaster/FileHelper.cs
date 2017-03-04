using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FasterMaster
{
    class FileHelper
    {
        public static void GetDirectoriesAndFiles(string sourceDirectory, List<string> directories, List<string> files)
        {
            // Store a stack of our directories.
            Stack<string> stack = new Stack<string>();

            // Add initial directory.  prevents deleting temp folder
            stack.Push(sourceDirectory);

            while (stack.Count > 0)
            {
                // Get top directory
                string dir = stack.Pop();

                try
                {
                    // Add all files at this directory to the result List.
                    files.AddRange(Directory.GetFiles(dir, "*.*"));

                    // Add all directories at this directory.
                    foreach (string dn in Directory.GetDirectories(dir))
                    {
                        stack.Push(dn);
                        directories.Add(dn);
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    //skip file
                    Console.WriteLine("fail1");
                }
            }
        }
    }
}
