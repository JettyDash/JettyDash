using Schemes.Enums;

namespace Schemes.DTOs.Pipelines;

public interface IContextualDto
{
    int UserId { get; set; }
    string UserRole { get; set; }
}


public class ConnectionPipeline(string userRole, int userId) : IContextualDto
{
    public int UserId { get; set; } = userId;
    public string UserRole { get; set; } = userRole;
}

public class CreateHostConnectionPipeline(string userRole, int userId) : ConnectionPipeline(userRole, userId)
{
    public string ConnectionString { get; set; } = string.Empty;
    public DatabaseType DatabaseType { get; set; } = DatabaseType.Unknown;
    public string VaultIdentifier { get; set; } = string.Empty;
}



public class GetAllConnectionPipeline(string userRole, int userId) : ConnectionPipeline(userRole, userId)
{
}
