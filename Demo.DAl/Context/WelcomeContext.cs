using Demo.DAl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAl.Context
{
    public class WelcomeContext:IdentityDbContext<ApplicationUser>
    {
        public WelcomeContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().Property(m => m.IsActive).HasDefaultValue(false);

        }
    }
}
