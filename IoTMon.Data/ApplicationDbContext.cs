using IoTMon.Models;
using IoTMon.Models.Enums;
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
            // many to many
            modelBuilder.Entity<DeviceSensor>()
                .HasKey(m => new { m.DeviceId, m.SensorId });
            modelBuilder.Entity<DeviceSensor>()
                .HasOne(ds => ds.Device)
                .WithMany(d => d.DeviceSensors)
                .HasForeignKey(d => d.DeviceId);
            modelBuilder.Entity<DeviceSensor>()
                .HasOne(ds => ds.Sensor)
                .WithMany(s => s.DeviceSensors)
                .HasForeignKey(s => s.SensorId);

            // enum type to string
            modelBuilder.Entity<Sensor>()
                .Property(s => s.ValueType)
                .HasConversion(
                    v => v.ToString(),
                    v => (ValueTypeEnum)Enum.Parse(typeof(ValueTypeEnum), v));

            modelBuilder.Entity<Alert>()
                .Property(a => a.ValueType)
                .HasConversion(
                    v => v.ToString(),
                    v => (ValueTypeEnum)Enum.Parse(typeof(ValueTypeEnum), v));

            // uniques
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Sensor>()
                .HasIndex(s => s.Label)
                .IsUnique();

            modelBuilder.SeedDevicesAndSensors();
            modelBuilder.SeedUsers();

            base.OnModelCreating(modelBuilder);
        }
    }
}
