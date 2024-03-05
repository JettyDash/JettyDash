namespace Infrastructure.SeedData;

public class ConnectionSeedData
{
    
}

using System;
using Infrastructure.Entities;

namespace Infrastructure.Seeding
{
    public static class ConnectionSeedData
    {
        public static void SeedConnections(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>().HasData(
                new Connection
                {
                    Id = 0,
                    Name = "Real Time fv",
                    DatabaseType = "FIREBIRD",
                    Status = "active",
                    Date = DateTime.Parse("2023-08-12T20:13:46.384Z")
                },
                new Connection
                {
                    Id = 1,
                    Name = "active_users_only",
                    DatabaseType = "POSTGRES",
                    Status = "active",
                    Date = DateTime.Parse("2024-01-12T20:15:46.384Z")
                },
                new Connection
                {
                    Id = 2,
                    Name = "not_so_active_users",
                    DatabaseType = "POSTGRES",
                    Status = "paused",
                    Date = DateTime.Parse("2023-09-11T20:14:56.384Z")
                },
                new Connection
                {
                    Id = 3,
                    Name = "players",
                    DatabaseType = "MSSQL",
                    Status = "active",
                    Date = DateTime.Parse("2022-09-11T20:22:22.384Z")
                },
                new Connection
                {
                    Id = 4,
                    Name = "nobody_cares_about_this_database",
                    DatabaseType = "MYSQL",
                    Status = "inactive",
                    Date = DateTime.Parse("2021-01-09T20:22:22.384Z")
                },
                new Connection
                {
                    Id = 5,
                    Name = "rfma",
                    DatabaseType = "ORACLE",
                    Status = "active",
                    Date = DateTime.Parse("2011-01-09T20:22:22.384Z")
                },
                new Connection
                {
                    Id = 6,
                    Name = "kelebekler",
                    DatabaseType = "MYSQL",
                    Status = "paused",
                    Date = DateTime.Parse("2008-01-09T20:22:22.384Z")
                }
            );
        }
    }
}
