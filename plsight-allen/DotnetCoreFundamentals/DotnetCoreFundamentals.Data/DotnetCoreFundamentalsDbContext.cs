using DotnetCoreFundamentals.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreFundamentals.Data
{
    public class DotnetCoreFundamentalsDbContext : DbContext
    {
        public DotnetCoreFundamentalsDbContext(DbContextOptions<DotnetCoreFundamentalsDbContext> options) : base(options)
        {
        
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
