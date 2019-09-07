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
            book.ShowStatistics();
        }
    }
}
