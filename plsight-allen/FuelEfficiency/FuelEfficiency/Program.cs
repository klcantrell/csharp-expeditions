using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FuelEfficiency
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");
            //var query = cars
            //    .OrderByDescending(c => c.Combined)
            //    .ThenBy(c => c.Name);

            var query =
                from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2016
                orderby car.Combined descending, car.Name
                select car;

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Name} : {car.City}");
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            var query =
                from line in File.ReadAllLines(path).Skip(1)
                where line.Length > 1
                select Car.ParseFromCsv(line);
            return query.ToList();
        }
    }
}
