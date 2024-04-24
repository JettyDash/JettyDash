namespace Schemes.Enum;


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
    PostgresSql=5432,
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
    // ConnectionFailed,
    Active,
    Inactive,
    Paused
}