using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectOne.Models;
using Microsoft.EntityFrameworkCore;


namespace ProjectOne.Data
{
    public class ProjectOneContext : DbContext
    {
        public ProjectOneContext()
        {

        }
        public ProjectOneContext (DbContextOptions<ProjectOneContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProjectOneContext-1;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
