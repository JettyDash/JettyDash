using Schemes.Enums;

namespace Schemes.Dto.Pipelines;

public interface IContextualDto
{
    int UserId { get; set; }
    string UserRole { get; set; }
}


public class ConnectionPipeline(string userRole, int userId, string username) : IContextualDto
{
    public int UserId { get; set; } = userId;
    public string UserRole { get; set; } = userRole;
    public string Username { get; set; } = username;
}

public class CreateHostConnectionPipeline(string userRole, int userId, string username) : ConnectionPipeline(userRole, userId, username)
{
    public string ConnectionString { get; set; } = string.Empty;
    public DatabaseType DatabaseType { get; set; } = DatabaseType.Unknown;
    // public string VaultIdentifier { get; set; } = string.Empty;
}



public class GetAllConnectionPipeline(string userRole, int userId, string username) : ConnectionPipeline(userRole, userId, username)
{
}
