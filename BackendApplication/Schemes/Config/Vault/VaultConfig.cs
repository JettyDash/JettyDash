namespace Schemes.Vault;

public class VaultConfig
{
    public string Address { get; set;}
    public string Token { get; set; }
    public int EngineVersion { get; set; }
    public string DatabaseSecretsPath { get; set; }
    public string Mount { get; set; }
}