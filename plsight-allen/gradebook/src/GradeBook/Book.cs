using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }

    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        string Category { get; }
        int Length { get; }
        event GradeAddedDelegate GradeAdded;

    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name, string category) : base(name)
        {
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

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();

        public abstract string Category
        {
            get;
        }

        public abstract int Length
        {
            get;
        }
    }

    public class InMemoryBook : Book
    {
        public const string DEVELOPER = "Kal";
        public override int Length
        {
            get => grades.Count;
        }
        public override string Category
        {
            get;
        }
        private List<double> grades;
        private Statistics stats;

        public InMemoryBook(string name, string category) : base(name, category)
        {
            Category = category;
            this.grades = new List<double>();
            this.stats = new Statistics();
        }


        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                stats.UpdateStats(grade, grades);
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

        public override event GradeAddedDelegate GradeAdded;


        public override Statistics GetStatistics()
        {
            return stats;
        }
    }
}