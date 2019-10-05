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
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query =
                from car in cars
                join manufacturer in manufacturers
                    on new { car.Manufacturer, car.Year } 
                        equals new { Manufacturer = manufacturer.Name, manufacturer.Year }
                orderby car.Combined descending, car.Name
                select new
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined,
                };

            var query2 =
                cars.Join(manufacturers, 
                c => new { c.Manufacturer, c.Year }, 
                m => new { Manufacturer = m.Name, m.Year },
                (c, m) => new
                {
                    m.Headquarters,
                    c.Name,
                    c.Combined
                })
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name);

            foreach (var car in query2.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }
        }

        private static List<Car> ProcessCars(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCar();

            return query.ToList();
        }
        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .ToManufacturer();

            return query.ToList();
        }
    }
    public static class CarExtenions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {

            foreach (var line in source)
            {
                var columns = line.Split(',');
                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7]),
                };
            }
        }

        public static IEnumerable<Manufacturer> ToManufacturer(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');
                yield return new Manufacturer
                {
                    Name = columns[0],
                    Headquarters = columns[1],
                    Year = int.Parse(columns[2]),
                };
            }
        }
    }
}
