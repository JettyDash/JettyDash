using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Schemes.Enum;

namespace Infrastructure.Seeding;


public static class ConnectionSeedData
    {
        public static void SeedConnections(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>().HasData(
                new Connection
                {
                    UserId = 1,
                    ConnectionId = 1,
                    ConnectionType = ConnectionType.Host,
                    DatabaseName = "Real Time firebird",
                    DatabaseType = DatabaseType.Unknown,
                    Status = ConnectionStatus.Connected,
                    CreationDate = new DateTime(2023, 08, 12, 20, 13, 46, 384, DateTimeKind.Utc),
                    LastUpdateTime = default,
                },
                new Connection
                {
                    UserId = 1,
                    ConnectionId = 2,
                    ConnectionType = ConnectionType.Host,
                    DatabaseName = "active_users_only",
                    DatabaseType = DatabaseType.PostgresSql,
                    Status = ConnectionStatus.Active,
                    CreationDate = new DateTime(2024, 01, 12, 20, 15, 46, 384, DateTimeKind.Utc),
                    LastUpdateTime = default,
                },
                new Connection
                {
                    UserId = 1,
                    ConnectionId = 3,
                    ConnectionType = ConnectionType.Url,
                    DatabaseName = "not_so_active_users",
                    DatabaseType = DatabaseType.PostgresSql,
                    Status = ConnectionStatus.Paused,
                    CreationDate = new DateTime(2023, 09, 11, 20, 14, 56, 384, DateTimeKind.Utc),
                    LastUpdateTime = default,
                },
                new Connection
                {
                    UserId = 1,
                    ConnectionId = 4,
                    // VaultIdentifier = String.Empty,
                    ConnectionType = ConnectionType.Host,
                    DatabaseName = "players",
                    DatabaseType = DatabaseType.SqlServer,
                    Status = ConnectionStatus.Active,
                    CreationDate = new DateTime(2022, 09, 11, 20, 22, 22, 384, DateTimeKind.Utc),
                    LastUpdateTime = default,
                },
                new Connection
                {
                    UserId = 1,
                    ConnectionId = 5,
                    // VaultIdentifier = String.Empty,
                    ConnectionType = ConnectionType.Url,
                    DatabaseName = "nobody_cares_about_this_database",
                    DatabaseType = DatabaseType.MySql,
                    Status = ConnectionStatus.Inactive,
                    CreationDate = new DateTime(2021, 01, 09, 20, 22, 22, 384, DateTimeKind.Utc),
                    LastUpdateTime = default,
                },
                new Connection
                {
                    UserId = 1,
                    ConnectionId = 6,
                    // VaultIdentifier = String.Empty,
                    ConnectionType = ConnectionType.Host,
                    DatabaseName = "rfma",
                    DatabaseType = DatabaseType.Oracle,
                    Status = ConnectionStatus.Paused,
                    CreationDate = new DateTime(2011, 01, 09, 20, 22, 22, 384, DateTimeKind.Utc),
                    LastUpdateTime = default,
                },
                new Connection
                {
                    UserId = 1,
                    ConnectionId = 7,
                    // VaultIdentifier = String.Empty,
                    ConnectionType = ConnectionType.Url,
                    DatabaseName = "kelebekler",
                    DatabaseType = DatabaseType.MySql,
                    Status = ConnectionStatus.Paused,
                    CreationDate = new DateTime(2008, 01, 09, 20, 22, 22, 384, DateTimeKind.Utc),
                    LastUpdateTime = default,
                }
            );
        }
    }