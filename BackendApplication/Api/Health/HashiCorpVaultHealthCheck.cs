


using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
namespace Api.Health;

public class HashiCorpVaultHealthCheck : IHealthCheck
{
    private readonly IVaultClient _vaultClient;

    public HashiCorpVaultHealthCheck(string vaultAddress, string token)
    {
        var vaultClientSettings = new VaultClientSettings(vaultAddress, new TokenAuthMethodInfo(token));
        _vaultClient = new VaultClient(vaultClientSettings);
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // You can customize this based on the health endpoint of your Vault instance.
            var healthStatus = await _vaultClient.V1.System.GetHealthStatusAsync();

            // Check the health status here and return HealthCheckResult accordingly.
            if (healthStatus?.Initialized ?? false)
            {
                return HealthCheckResult.Healthy("Vault is initialized and healthy.");
            }
            else
            {
                return HealthCheckResult.Unhealthy("Vault is not initialized or healthy.");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions here.
            return HealthCheckResult.Unhealthy($"Error checking Vault health: {ex.Message}");
        }
    }
}
