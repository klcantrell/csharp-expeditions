using System;
using System.Collections.Generic;

namespace GradeBook
{
    public class Statistics
    {
        public Statistics()
        {
            this.highGrade = double.MinValue;
            this.lowGrade = double.MaxValue;
        }

        private double highGrade;
        private double lowGrade;
        private double averageGrade;
        private char letterGrade;

        public double HighGrade
        {
            get => highGrade == double.MinValue ? 0.0 : highGrade;
            private set => highGrade = Math.Max(value, highGrade);
        }
        public double LowGrade
        {
            get => lowGrade == double.MaxValue ? 0.0 : lowGrade;
            private set => lowGrade = Math.Min(value, lowGrade);
        }
        public char LetterGrade
        {
            get
            {
                switch (AverageGrade)
                {
                    case var d when d >= 90.0:
                        return LetterGrade = 'A';
                    case var d when d >= 80.0:
                        return LetterGrade = 'B';
                    case var d when d >= 70.0:
                        return LetterGrade = 'C';
                    case var d when d >= 60.0:
                        return LetterGrade = 'D';
                    default:
                        return LetterGrade = 'F';
                }

            }
            private set => letterGrade = value;
        }
        public double AverageGrade
        {
            get => averageGrade;
            private set => averageGrade = value;
        }

        public void UpdateStats(double grade, List<double> grades)
        {
            HighGrade = grade;
            LowGrade = grade;
            AverageGrade = ComputeAverage(grades);
        }

        private double ComputeAverage(List<double> grades)
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

            return result;
        }
    }
}