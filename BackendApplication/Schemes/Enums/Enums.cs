namespace Schemes.Enums;


public enum Role
{
    Admin,
    User,
    Guest
}

public enum DatabaseType
{
    Unknown,
    MySql=3306,
    PostgreSql=5432,
    SqlServer=1433,
    Oracle=1521,
}

public enum ConnectionType
{
    Host,
    Url
}

public enum ConnectionStatus
{
    Connected,
    ConnectionFailed,
}