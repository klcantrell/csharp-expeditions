using System;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;

namespace PlayWithCSharpAOT
{
    class Program
    {
        static readonly string executableName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var files = GetFileData(Directory.GetFiles(currentDirectory));

            ConfirmFiles(files);

            foreach (var file in files)
            {
                var (fileFullPath, fileName, fileYear, fileMonth) = file;

                if (fileName == executableName) continue;

                var newParent = Path.Combine(currentDirectory, fileYear, fileMonth);
                if (!Directory.Exists(newParent))
                {
                    Console.WriteLine($"Directory for {fileYear}/{fileMonth} does not exist");
                    Console.WriteLine($"Creating directory for {fileYear}/{fileMonth}");
                    Directory.CreateDirectory(newParent);
                }
                Console.WriteLine($"Moving {fileName} to {fileYear}/{fileMonth}");
                File.Move(fileFullPath, Path.Combine(newParent, fileName));
            }
        }

        static void ConfirmFiles(List<FileData> files)
        {
            const int maxToDisplay = 10;
            var numberOfFilesNotDisplayed = files.Count - maxToDisplay;

            var filesToDisplay = files
                .Take(maxToDisplay)
                .Where(f => !f.name.Equals(executableName))
                .Aggregate("", (displayText, next) => $"{displayText}\n{next.name}");

            var directoriesToDisplay = files
                .Aggregate(new Dictionary<string, HashSet<string>>(), (acc, next) =>
                {
                    if (acc.ContainsKey(next.year))
                    {
                        acc[next.year].Add(next.month);
                        return acc;
                    }
                    else
                    {
                        acc.Add(next.year, new HashSet<string>() { next.month });
                        return acc;
                    }
                })
                .Aggregate("", (displayText, next) =>
                {
                    return String.Concat(
                        displayText,
                        $"\n{next.Key}",
                        String.Join("", next.Value.Select((month) => $"\n  -- {month}"))
                    );
                });

            var confirmationMessage = String.Concat(
                filesToDisplay,
                numberOfFilesNotDisplayed > 0 ? $"\n...and {numberOfFilesNotDisplayed} others" : "",
                "\n\n...will be moved into the following directories...",
                $"\n{directoriesToDisplay}"
            );

            Console.WriteLine(confirmationMessage);
            Console.WriteLine();

            Console.Write("Do you want to proceed? (y/n) ");
            var keyPressed = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();

            int response;
            bool responseIsInt = int.TryParse(keyPressed.Key.ToString(), out response);

            if ((responseIsInt && response == 78)
                    || (!responseIsInt && keyPressed.Key.ToString().ToLower().Equals("n")))
            {
                Console.WriteLine("Ok, bye!");
                Environment.Exit(0);
            }

            if ((responseIsInt && response != 89)
                    || (!responseIsInt && !keyPressed.Key.ToString().ToLower().Equals("y")))
            {
                Console.WriteLine(keyPressed.Key.ToString().ToLower());
                Console.WriteLine("Unrecognized response. Follow the instructions next time!");
                Environment.Exit(1);
            }
        }

        static List<FileData> GetFileData(string[] files)
        {
            return files.Select(f =>
                new FileData(
                    f,
                    Path.GetFileName(f),
                    File.GetCreationTime(f).Year.ToString(),
                    File.GetCreationTime(f).ToString("MMM")
                )
            ).ToList();
        }
    }

    record FileData(string fullPath, string name, string year, string month);
}
