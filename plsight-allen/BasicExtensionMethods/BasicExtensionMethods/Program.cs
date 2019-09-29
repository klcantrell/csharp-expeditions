using System;
using System.Linq;
using System.Collections.Generic;
//using BasicExtensionMethods.Linq;

namespace BasicExtensionMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            var developers = new Employee[]
            {
                new Employee{ Id = 1, Name = "Scott" },
                new Employee{ Id = 2, Name = "Chris" },
            };

            var sales = new List<Employee>()
            {
                new Employee {  Id = 3, Name = "Alex" }
            };

            Console.WriteLine(sales.Count());

            //var query = developers.Where(e => e.Name.Length == 5)
            //                                   .OrderByDescending(e => e.Name);

            var query = from developer in developers
                        where developer.Name.Length == 5
                        orderby developer.Name
                        select developer;

            foreach (var employee in query)
            {
                Console.WriteLine(employee.Name);
            }

            Func<int, int> square = x => x * x;
            Func<int, int, int> add = (x, y) => x + y;
            Action<int> write = x => Console.WriteLine(x);

            write(square(add(3, 5)));
        }
    }
}
