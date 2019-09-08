using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("");
            Statistics stats;
            Console.WriteLine("Let's calculate grade stats.");

            while (true)
            {
                Console.WriteLine("Please enter a numerical grade. Enter 'done' when you're finished.");
                var input = Console.ReadLine();
                if (input == "done")
                {
                    break;
                }
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("Let's try that again...");
                }
            }

            if (book.Length > 0)
            {
                stats = book.GetStatistics();
                Console.WriteLine($"Average is {stats.Average:N2}");
                Console.WriteLine($"High Grade is {stats.High:N2}");
                Console.WriteLine($"Low Grade is {stats.Low:N2}");
                Console.WriteLine($"Letter Grade is {stats.Letter}");
            }
            else
            {
                Console.WriteLine("You have not entered any grades.  Goodbye.");
            }
        }
    }
}
