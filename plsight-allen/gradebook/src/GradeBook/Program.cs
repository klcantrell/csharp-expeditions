using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new DiskBook("Kal's gradebook", "Science");
            book.GradeAdded += OnGradeAdded;
            book.GradeAdded += OnGradeAdded;
            Console.WriteLine("Let's calculate grade stats.");

            EnterGrades(book);

            if (book.Length > 0)
            {
                Statistics stats = book.GetStatistics();
                Console.WriteLine($"The Awesome {book.Category} book of {book.Name}");
                Console.WriteLine($"Average is {stats.AverageGrade:N2}");
                Console.WriteLine($"High Grade is {stats.HighGrade:N2}");
                Console.WriteLine($"Low Grade is {stats.LowGrade:N2}");
                Console.WriteLine($"Letter Grade is {stats.LetterGrade}");
            }
            else
            {
                Console.WriteLine("You have not entered any grades.  Goodbye.");
            }
        }

        private static void EnterGrades(IBook book)
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
