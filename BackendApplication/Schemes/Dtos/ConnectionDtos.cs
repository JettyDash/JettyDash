namespace Schemes.Dtos;



public class ConnectionRequestBase
{ 
    public string? DatabaseName { get; set; }
    public string? DatabaseType { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class CreateUrlConnectionRequest : ConnectionRequestBase
{
    public string? Url { get; set; }
}

public class CreateHostConnectionRequest : ConnectionRequestBase
{
    public string? Host { get; set; }
    public int? Port { get; set; }
    public string? DatabaseOrSchema { get; set; }
}

public class UpdateUrlConnectionRequest : CreateUrlConnectionRequest
{
}

public class UpdateHostConnectionRequest : CreateHostConnectionRequest
{
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
