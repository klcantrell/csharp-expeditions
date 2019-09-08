using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book();
            book.AddGrade(56.1);
            book.AddGrade(90.5);
            var stats = book.GetStatistics();

            Console.WriteLine($"Average is {stats.Average:N2}");
            Console.WriteLine($"High Grade is {stats.High:N2}");
            Console.WriteLine($"Low Grade is {stats.Low:N2}");
        }
    }
}
