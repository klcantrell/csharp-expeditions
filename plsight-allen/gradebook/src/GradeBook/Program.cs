using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new InMemoryBook("Kal's gradebook", "Science");
            book.GradeAdded += OnGradeAdded;
            book.GradeAdded += OnGradeAdded;
            Statistics stats;
            Console.WriteLine("Let's calculate grade stats.");

            EnterGrades(book);

            if (book.Length > 0)
            {
                stats = book.GetStatistics();
                Console.WriteLine($"The Awesome {book.Category} book of {book.Name}");
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

        private static void EnterGrades(Book book)
        {
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
                    Console.WriteLine("Let's do it again...");
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
