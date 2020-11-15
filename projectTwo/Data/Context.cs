using Microsoft.EntityFrameworkCore;
using projectDataDimension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectDataDimension.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }
         //public DbSet<Employee> Employee { get; set; }
        public DbSet<BusinessTravel> BusinessTravel { get; set; }
        public DbSet<JobRole> JobRole { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Education> Education { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           // modelBuilder.Entity<Employee>().HasKey("EmployeeNumber");
        }


    }
}
