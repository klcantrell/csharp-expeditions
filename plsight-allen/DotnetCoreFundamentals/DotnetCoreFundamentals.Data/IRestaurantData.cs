﻿using DotnetCoreFundamentalsCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotnetCoreFundamentals.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAllRestaurant();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 1, Name = "Cinnamon Club", Location = "London", Cuisine = CuisineType.Indian },
                new Restaurant { Id = 1, Name = "La Costa", Location = "California", Cuisine = CuisineType.Mexican }
            };
        }

        public IEnumerable<Restaurant> GetAllRestaurant()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }
    }
}
