
using EFProject.Data;
using EFProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservation = EFProject.Models.Reservation;


namespace EFProject.AdminUser
{
    public class UserSystem
    {
        public void RegisterUser(AppDbContext context, string firstName, string lastName, string email, string phoneNumber, bool isAdmin)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                IsAdmin = isAdmin  
            };

            context.Users.Add(user);
            context.SaveChanges();
        }

        public void ReserveDoctor(AppDbContext context, string userFirstName, string userLastName, string doctorFirstName, string doctorLastName, DateTime reservationTime)
        {
            var user = context.Users.FirstOrDefault(u => u.FirstName == userFirstName && u.LastName == userLastName);
            var doctor = context.Doctors.FirstOrDefault(d => d.FirstName == doctorFirstName && d.LastName == doctorLastName);

            if (user == null || doctor == null)
            {
                Console.WriteLine("İstifadəçi və ya həkim tapılmadı.");
                return;
            }

            var reservation = new Reservation
            {
                UserId = user.Id,
                DoctorId = doctor.Id,
                ReservedTime = reservationTime
            };

            context.Reservations.Add(reservation);
            context.SaveChanges();
            Console.WriteLine("Rezervasiya uğurla tamamlandı.");
        }
    }
}

