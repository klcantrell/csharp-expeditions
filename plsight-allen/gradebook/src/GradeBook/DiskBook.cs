using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GradeBook
{
    public class DiskBook : Book
    {
        private string fileName;
        private Statistics stats;

        public DiskBook(string name, string category) : base(name, category)
        {
            Category = category;
            stats = new Statistics();
            fileName = $"{Name}.txt";

            File.WriteAllText(fileName, string.Empty);
        }

        public override string Category
        {
            get;
        }

        public override int Length
        {
            get => 1;
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText(fileName))
            {
                if (grade <= 100 && grade >= 0)
                {
                    writer.WriteLine(grade);
                    if (GradeAdded != null)
                    {
                        GradeAdded(this, new EventArgs());
                    }
                }
                else
                {
                    throw new ArgumentException($"Invalid {nameof(grade)}");
                }
            }
            var grades = new List<double>(
                File
                    .ReadLines(fileName)
                    .Select(gradeAsString => double.Parse(gradeAsString))
                    .ToArray()
            );
            stats.UpdateStats(grade, grades);
        }

        public override Statistics GetStatistics()
        {
            return stats;
        }
    }
}