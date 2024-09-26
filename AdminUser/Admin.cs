using EFProject.Data;
using EFProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.AdminUser
{
    public class Admin
    {
        public void ViewReservations(AppDbContext context)
        {
            var reservations = context.Reservations
                .Include(r => r.User)
                .Include(r => r.Doctor)
                .ToList();

            foreach (var reservation in reservations)
            {
                Console.WriteLine($"user: {reservation.User.FirstName} {reservation.User.LastName}, " +
                                  $"doctor: {reservation.Doctor.FirstName} {reservation.Doctor.LastName}, " +
                                  $"time: {reservation.ReservedTime}");
            }
        }

        public void AddDoctor(AppDbContext context, string firstName, string lastName, string department, int experience)
        {
            var doctor = new Doctor
            {
                FirstName = firstName,
                LastName = lastName,
                Department = department,
                Experience = experience
            };

            context.Doctors.Add(doctor);
            context.SaveChanges();
        }
    }

}
