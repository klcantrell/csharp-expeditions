using System;
using System.Linq;
using System.IO;
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
            const string invalidInputMessage = "Invalid input. Please enter y or n next time.";
            var numberOfFilesNotDisplayed = files.Count - maxToDisplay;

            var filesToDisplay = files
                .Take(maxToDisplay)
                .Where(f => !f.name.Equals(executableName))
                .Aggregate("", (displayText, next) => $"{displayText}\n{next.name}");

            var directoriesToDisplay = files
                .Aggregate(new Dictionary<string, HashSet<string>>(), (acc, next) =>
                {
                    var (_, _, year, month) = next;
                    if (acc.ContainsKey(next.year))
                    {
                        acc[year].Add(month);
                        return acc;
                    }
                    else
                    {
                        acc.Add(year, new HashSet<string>() { month });
                        return acc;
                    }
                })
                .Aggregate("", (displayText, next) =>
                {
                    var (year, months) = next;
                    return String.Concat(
                        displayText,
                        $"\n{year}",
                        String.Join("", months.Select((month) => $"\n  -- {month}"))
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
            var userInput = Console.ReadLine();
            Console.WriteLine();

            if (userInput.Length != 1
                || (!userInput.First().ToString().ToLower().Equals("n")
                    && !userInput.First().ToString().ToLower().Equals("y")))
            {
                Console.WriteLine(invalidInputMessage);
                Environment.Exit(1);
            }

            if (userInput.First().ToString().ToLower().Equals("n"))
            {
                Console.WriteLine("Ok, bye!");
                Environment.Exit(0);
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
