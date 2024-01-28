namespace Schemes.Dtos;



public class ConnectionRequestBase
{
    public string ConnectionType { get; set; }
    public string DatabaseName { get; set; }
    public string Type { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public abstract class CreateUrlConnectionRequest : ConnectionRequestBase
{
    public string Url { get; set; }
}

public abstract class CreateHostConnectionRequest : ConnectionRequestBase
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string DatabaseOrSchema { get; set; }
}

public abstract class UpdateUrlConnectionRequest : CreateUrlConnectionRequest
{
}

public abstract class UpdateHostConnectionRequest : CreateHostConnectionRequest
{
}

public class ConnectionResponseBase
{
    public string ConnectionType { get; set; }
    public int UserId { get; set; }
    public string DatabaseName { get; set; }
    public string Type { get; set; }
    
    public string Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateTime { get; set; }
}

public abstract class ConnectionResponse(ConnectionResponseBase content) : BaseResponse<ConnectionResponseBase>(content);