using System;
using System.Collections.Generic;
using System.Text;

namespace Queries
{
    class Movie
    {
        private float rating;
        private int year;

        public string Title { get; set; }
        public float Rating { get; set; }
        public int Year 
        { 
            get
            {
                Console.WriteLine($"Returning {year} for {Title}");
                return year;
            }
            set
            {
                year = value;
            }
        }
    }
}
