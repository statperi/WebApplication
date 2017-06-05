using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CompanyContext : DbContext , ICompanyContext
    {
        public CompanyContext() : base("name=CompanyDBConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Map entity to table
            modelBuilder.Entity<Department>().ToTable("Departments", "dbo");
            modelBuilder.Entity<Employee>().ToTable("Employees", "dbo");

        }
    }
}