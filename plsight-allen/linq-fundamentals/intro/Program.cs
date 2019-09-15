using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\windows";
            ShowLargeFilesWithoutLinq(path);
            Console.WriteLine("***");
            ShowLargeFilesWithLinq(path);
        }

        private static void ShowLargeFilesWithoutLinq(string path)
        {
            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());
            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine($"{files[i].Name,-20} : {files[i].Length,10:N0}");
            }
        }

        private static void ShowLargeFilesWithLinq(string path)
        {
            var query = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;
            foreach (var file in query.Take(5))
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            }
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
