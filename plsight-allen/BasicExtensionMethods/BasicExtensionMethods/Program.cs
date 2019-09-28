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
            Employee[] developers = new Employee[]
            {
                new Employee{ Id = 1, Name = "Scott" },
                new Employee{ Id = 2, Name = "Chris" },
            };

            List<Employee> sales = new List<Employee>()
            {
                new Employee {  Id = 3, Name = "Alex" }
            };

            Console.WriteLine(sales.Count());
        }
    }
}
