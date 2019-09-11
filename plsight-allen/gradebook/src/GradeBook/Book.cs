using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class Book
    {
        public const string DEVELOPER = "Kal";
        public readonly string Category;
        public string Name;
        public int Length
        {
            get => grades.Count;
        }
        private List<double> grades;
        private double highGrade;
        private double lowGrade;
        private double HighGrade
        {
            get => highGrade == double.MinValue ? 0.0 : highGrade;
            set => highGrade = value;
        }
        private double LowGrade
        {
            get => lowGrade == double.MaxValue ? 0.0 : lowGrade;
            set => lowGrade = value;
        }
        private char LetterGrade;

        public Book(string name, string category)
        {
            Name = name;
            Category = category;
            this.grades = new List<double>();
            this.highGrade = double.MinValue;
            this.lowGrade = double.MaxValue;
        }

        public void AddLetterGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90.0);
                    break;

                case 'B':
                    AddGrade(80.0);
                    break;

                case 'C':
                    AddGrade(70.0);
                    break;

                case 'D':
                    AddGrade(60.0);
                    break;

                default:
                    AddGrade(0.0);
                    break;
            }
        }

        public void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                HighGrade = Math.Max(grade, highGrade);
                LowGrade = Math.Min(grade, lowGrade);
                grades.Add(grade);
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

        public event GradeAddedDelegate GradeAdded;

        public double ComputeAverage()
        {
            if (grades.Count == 0)
            {
                return 0.0;
            }

            var result = 0.0;
            foreach (var grade in grades)
            {
                result += grade;
            }
            result /= grades.Count;

            switch (result)
            {
                case var d when d >= 90.0:
                    LetterGrade = 'A';
                    break;
                case var d when d >= 80.0:
                    LetterGrade = 'B';
                    break;
                case var d when d >= 70.0:
                    LetterGrade = 'C';
                    break;
                case var d when d >= 60.0:
                    LetterGrade = 'D';
                    break;
                default:
                    LetterGrade = 'F';
                    break;
            }

            return result;
        }

        public Statistics GetStatistics()
        {
            var average = ComputeAverage();
            var result = new Statistics();
            result.Letter = LetterGrade;
            result.Low = LowGrade;
            result.High = HighGrade;
            result.Average = average;
            return result;
        }
    }
}