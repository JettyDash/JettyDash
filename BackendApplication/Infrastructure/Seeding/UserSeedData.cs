
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Schemes.Enums;

namespace Infrastructure.Seeding;



public static class UserSeedData
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "guest",
                    Password = "84983C60F7DAADC1CB8698621F802C0D9F9A3C3C295C810748FB048115C186EC",
                    Email = "null",
                    Role = Role.Guest,
                    PasswordRetryCount = 0,
                    IsActive = true,
                    LastActivityDateTime = DateTime.UtcNow,
                }
            );
        }
    }