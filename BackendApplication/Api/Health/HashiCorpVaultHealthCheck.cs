// using Microsoft.Extensions.Diagnostics.HealthChecks;
// using VaultSharp;
// namespace Api.Health;
//
// public class HashiCorpVaultHealthCheck(IVaultClient vaultClient) : IHealthCheck
// {
//     public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
//     {
//         try
//         {
//             // You can customize this based on the health endpoint of your Vault instance.
//             var healthStatus = await vaultClient.V1.System.GetHealthStatusAsync();
//
//             // Check the health status here and return HealthCheckResult accordingly.
//             if (healthStatus?.Initialized ?? false)
//             {
//                 return HealthCheckResult.Healthy("Vault is initialized and healthy.");
//             }
//             else
//             {
//                 return HealthCheckResult.Unhealthy("Vault is not initialized or healthy.");
//             }
//         }
//         catch (Exception ex)
//         {
//             // Log any exceptions here.
//             return HealthCheckResult.Unhealthy($"Error checking Vault health: {ex.Message}");
//         }
//     }
// }
