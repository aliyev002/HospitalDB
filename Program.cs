using EFProject.AdminUser;
using EFProject.Data;
using EFProject.Models;
using Microsoft.EntityFrameworkCore;
using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            using (var context = new AppDbContext())
            {
                Console.WriteLine("Email-i daxil edin:");
                string email = Console.ReadLine().Trim();

                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    Console.WriteLine("Istifadeci tapilmadi, qeydiyyatdan kecmek isteyirsinizmi? (yes/no)");
                    string registerChoice = Console.ReadLine().Trim().ToLower();

                    if (registerChoice == "yes")
                    {
                        RegisterNewUser(context);
                    }
                    else
                    {
                        Console.WriteLine("Proqramdan cixis edilir...");
                        return;
                    }
                }
                else
                {
                    if (user.IsAdmin)
                    {
                        AdminOperations(context);
                    }
                    else
                    {
                        UserOperations(context, user);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bas veren xeta: {ex.Message}");
        }
    }

    private static void RegisterNewUser(AppDbContext context)
    {
        var userSystem = new UserSystem();

        try
        {
            Console.Write("Ad: ");
            string firstName = Console.ReadLine();
            Console.Write("Soyad: ");
            string lastName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Telefon nomresi: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Admin olaraq qeydiyyatdan kecmek isteyirsiniz? (yes/no): ");
            bool isAdmin = Console.ReadLine()?.Trim().ToLower() == "yes";

            userSystem.RegisterUser(context, firstName, lastName, email, phoneNumber, isAdmin);
            Console.WriteLine("Qeydiyyat tamamlandi.");
        }
        
        catch (Exception ex)
        {
            Console.WriteLine($"Bas veren xeta: {ex.Message}");
        }
    }

    private static void AdminOperations(AppDbContext context)
    {
        var admin = new Admin();
        bool exit = false;

        while (!exit)
        {
            try
            {
                Console.WriteLine("\nAdmin emeliyyatlari:");
                Console.WriteLine("1. Rezervasiyalara bax");
                Console.WriteLine("2. Yeni hekim elave et");
                Console.WriteLine("3. Cixis");

                Console.Write("Secim edin: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        admin.ViewReservations(context);
                        break;
                    case "2":
                        Console.Write("Hekim adi: ");
                        string doctorFirstName = Console.ReadLine();
                        Console.Write("Hekim soyadi: ");
                        string doctorLastName = Console.ReadLine();
                        Console.Write("Bolme: ");
                        string department = Console.ReadLine();
                        Console.Write("Tecrube (il): ");
                        int experience = int.Parse(Console.ReadLine());

                        admin.AddDoctor(context, doctorFirstName, doctorLastName, department, experience);
                        Console.WriteLine("Yeni hekim elave edildi.");
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Yanlis secim. Yeniden cehd edin.");
                        break;
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Bas veren xeta: {ex.Message}");
            }
        }
    }

    private static void UserOperations(AppDbContext context, User user)
    {
        var userSystem = new UserSystem();
        bool exit = false;

        while (!exit)
        {
            try
            {
                Console.WriteLine("\nİstifadeci emeliyyatlari:");
                Console.WriteLine("1. Hekim ucun rezervasiya et");
                Console.WriteLine("2. Cixis");

                Console.Write("Secim edin: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ReserveDoctor(userSystem, context, user);
                        break;
                    case "2":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Yanlis secim. Yeniden cehd edin.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bas veren xeta: {ex.Message}");
            }
        }
    }

    private static void ReserveDoctor(UserSystem userSystem, AppDbContext context, User user)
    {
        try
        {
            Console.Write("Hekim adi: ");
            string doctorFirstName = Console.ReadLine();
            Console.Write("Hekim soyadi: ");
            string doctorLastName = Console.ReadLine();
            Console.Write("Rezervasiya tarixi ve vaxti (YYYY-MM-DD HH:MM): ");
            DateTime reservationTime = DateTime.Parse(Console.ReadLine());

            userSystem.ReserveDoctor(context, user.FirstName, user.LastName, doctorFirstName, doctorLastName, reservationTime);
            Console.WriteLine("Rezervasiya tamamlandi.");
        }
        
        catch (Exception ex)
        {
            Console.WriteLine($"Bas veren xeta: {ex.Message}");
        }
    }
}
