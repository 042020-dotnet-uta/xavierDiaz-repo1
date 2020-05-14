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
        public ProjectOneContext (DbContextOptions<ProjectOneContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
