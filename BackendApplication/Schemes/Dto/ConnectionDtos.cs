
using Schemes.Enums;

namespace Schemes.Dto;


public class TestConnectionRequest
{
    public string ConnectionString { get; set; }
    public DatabaseType DatabaseType { get; set; }
}

public class CreateConnectionRequestBase
{ 
    public string DatabaseName { get; set; }
    public string DatabaseType { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class UpdateConnectionRequestBase
{ 
    public string? DatabaseName { get; set; }
    public string? DatabaseType { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class CreateUrlConnectionRequest : CreateConnectionRequestBase
{
    public string Url { get; set; }
}

public class CreateHostConnectionRequest : CreateConnectionRequestBase
{
    public string Host { get; set; }
    public int Port { get; set; }
}

public class UpdateUrlConnectionRequest : UpdateConnectionRequestBase
{
    public string? Url { get; set; }

}

public class UpdateHostConnectionRequest : UpdateConnectionRequestBase
{
    public string? Host { get; set; }
    public int? Port { get; set; }
    public string? DatabaseOrSchema { get; set; }
}

public class ConnectionResponse
{
    public string ConnectionType { get; set; }
    public int UserId { get; set; }
    public string DatabaseName { get; set; }
    public string DatabaseType { get; set; }
    
    public string Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateTime { get; set; }
}
