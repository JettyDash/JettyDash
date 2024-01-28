namespace Schemes.Enums;


public enum Role
{
    Admin,
    User,
    Guest
}

public enum DatabaseType
{
    MySql,
    Postgres,
    SqlServer,
    Oracle,
    Firebase,
    Firestore,
    MongoDb
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