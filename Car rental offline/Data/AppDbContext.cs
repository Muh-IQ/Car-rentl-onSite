using Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public  class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Fule> Fule { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<RentalBooking> RentalBooking { get; set; }

        public DbSet<RentalTransaction> RentalTransaction { get; set; }
        public DbSet<VehicleReturn> VehicleReturn { get; set; }

    }
}
