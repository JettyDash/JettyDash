
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
                    Password = "null",
                    Email = "null",
                    Role = Role.Guest,
                    PasswordRetryCount = 0,
                    IsActive = true,
                    LastActivityDateTime = DateTime.UtcNow,
                }
            );
        }
    }