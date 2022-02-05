using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _1262228_Arosh.Models;
using _1262228_Arosh.ViewModels;

namespace _1262228_Arosh.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<AcademicResult> AcademicResults { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SeatPlan> SeatPlans { get; set; }



        protected override void OnModelCreating(ModelBuilder Builder)
        {
            base.OnModelCreating(Builder);

            Builder.Entity<Result>().Property(t => t.TotalMark).UsePropertyAccessMode(PropertyAccessMode.Property);
        }



        //public DbSet<_1262228_Arosh.ViewModels.StudentVM> StudentVM { get; set; }


    }
}
