using IoTMon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // enum type to string
            modelBuilder.Entity<Sensor>()
                .Property(s => s.ValueType)
                .HasConversion(
                    v => v.ToString(),
                    v => (ValueType)Enum.Parse(typeof(ValueType), v));

            modelBuilder.Entity<Alert>()
                .Property(a => a.ValueType)
                .HasConversion(
                    v => v.ToString(),
                    v => (ValueType)Enum.Parse(typeof(ValueType), v));

            // uniques
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Sensor>()
                .HasIndex(s => s.Label)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
