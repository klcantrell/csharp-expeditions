using DotnetCoreFundamentalsCoreLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreFundamentals.Data
{
    public class DotnetCoreFundamentalsDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
