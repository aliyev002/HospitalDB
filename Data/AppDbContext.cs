using Amazon.EC2.Model;
using EFProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservation = EFProject.Models.Reservation;

namespace EFProject.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = DESKTOP-VKCKL67\\MSSQLSERVER02; Database=HospitalDB;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .Property(u => u.IsAdmin)
               .HasDefaultValue(false);  
            
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);  
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired() // Bu metod deyir ki Null ola bilmez
                .HasMaxLength(50);  
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            
            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.Id); 
            modelBuilder.Entity<Doctor>()
                .Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Doctor>()
                .Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Doctor>()
                .Property(d => d.Department)
                .IsRequired()
                .HasMaxLength(100); 
            modelBuilder.Entity<Doctor>()
                .Property(d => d.Experience)
                .IsRequired();  

            
            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.Id);  
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)  
                .WithMany(u => u.Reservations)  
                .HasForeignKey(r => r.UserId);  

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Doctor)  
                .WithMany(d => d.Reservations)  
                .HasForeignKey(r => r.DoctorId);  

            
            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.DoctorId, r.ReservedTime })  // Sürətli axtarış üçün!!!
                .IsUnique();  // Təkrarlanmanın qarşısını almaq üçün!!!
        }
    }
}

