﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace FuelEfficiency
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateXML();
            //QueryXML();

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());

            InsertData();
            QueryData();
        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb();
            db.Database.Log = Console.WriteLine;

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        private static void QueryData()
        {
            var db = new CarDb();
            db.Database.Log = Console.WriteLine;

            var query =
                from car in db.Cars
                orderby car.Combined descending, car.Name ascending
                group car by car.Manufacturer into manufacturer
                select new {
                    Name = manufacturer.Key,
                    Cars = (from car in manufacturer
                            orderby car.Combined descending
                            select car).Take(2)
                };

            foreach (var group in query)
            {
                Console.WriteLine(group.Name);
                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
        }

        private static void QueryXML()
        {
            var document = XDocument.Load("fuel.xml");
            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";

            var query =
                from element in document.Descendants(ex + "Car")
                where element.Attribute("Manufacturer")?.Value == "BMW"
                select element.Attribute("Name").Value;

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static void CreateXML()
        {
            var records = ProcessCars("fuel.csv");

            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
            var document = new XDocument();
            var cars = new XElement(ns + "Cars",
                from record in records
                select new XElement(ex + "Car",
                    new XAttribute("Name", record.Name),
                    new XAttribute("Combined", record.Combined),
                    new XAttribute("Manufacturer", record.Manufacturer))
            );

            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));

            document.Add(cars);
            document.Save("fuel.xml");
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
