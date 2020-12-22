using System;
using System.IO;
using System.Diagnostics;

namespace PlayWithCSharpAOT
{
    class Program
    {
        static void Main(string[] args)
        {
            var executableName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            var currentDirectory = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentDirectory);
            foreach (var file in files)
            {
                if (Path.GetFileName(file) == executableName)
                {
                    continue;
                }
                var creationYear = File.GetCreationTime(file).Year;
                var newParent = Path.Combine(currentDirectory, creationYear.ToString());
                if (!Directory.Exists(newParent))
                {
                    Console.WriteLine($"Directory {newParent} does not exist");
                    Console.WriteLine($"Creating directory for {creationYear}");
                    Directory.CreateDirectory(newParent);
                }
                var fileName = Path.GetFileName(file);
                Console.WriteLine($"Moving {file} to {newParent}");
                File.Move(file, Path.Combine(newParent,fileName));
            }
        }
    }
}
