using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WebApplication1.Models.Entity;

namespace WebApplication1.App_Start
{
    public class dbContext : DbContext
    {
        public DbSet<Visits> Visits { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public dbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
