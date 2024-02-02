namespace Schemes.DTOs;

public class VaultConnectionData(string connectionString)
{
    public string ConnectionString { get; set; } = connectionString;
}
