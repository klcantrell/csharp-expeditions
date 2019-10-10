using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace FuelEfficiency
{
    class CarDb : DbContext
    {
        public DbSet<Car> Cars { get; set; }
    }
}
