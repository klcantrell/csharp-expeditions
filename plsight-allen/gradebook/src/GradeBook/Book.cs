using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Book
    {
        private List<double> grades;
        private double highGrade;
        private double lowGrade;
        public double HighGrade
        {
            get => highGrade == double.MinValue ? 0.0 : highGrade;
            private set => highGrade = value;
        }
        public double LowGrade
        {
            get => lowGrade == double.MaxValue ? 0.0 : lowGrade;
            private set => lowGrade = value;
        }

        public Book()
        {
            this.grades = new List<double>();
            this.highGrade = double.MinValue;
            this.lowGrade = double.MaxValue;
        }

        public void AddGrade(double grade)
        {
            HighGrade = Math.Max(grade, highGrade);
            LowGrade = Math.Min(grade, lowGrade);
            grades.Add(grade);
        }

        public double ComputeAverage()
        {
            if (grades.Count == 0)
            {
                return 0.0;
            }

            var result = 0.0;
            foreach (var number in grades)
            {
                result += number;
            }
            result /= grades.Count;
            return result;
        }

        public void ShowStatistics()
        {
            var average = ComputeAverage();
            Console.WriteLine($"Average is {average:N2}");
            Console.WriteLine($"High Grade is {HighGrade:N2}");
            Console.WriteLine($"Low Grade is {LowGrade:N2}");
        }

    }
}