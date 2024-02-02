using Schemes.Exceptions;
using VaultSharp;

namespace Business.Services;

public interface IVaultService
{
    Task SaveOrUpdateCredentials<T>(string path, T data, string mountPoint);
    Task<object> GetCredentialByPath(string path, string mountPoint);
    Task<string[]> GetAllCredentials(string path, string mountPoint);
}

public class VaultService : IVaultService
{
    private readonly IVaultClient vaultClient;

    public VaultService(IVaultClient vaultClient)
    {
        this.vaultClient = vaultClient;
    }

    // $"users/{username}/databases{databaseId}"
    public async Task SaveOrUpdateCredentials<T>(string path, T data, string mountPoint)
    {
        //   domainUrl/v1/:secret-mount-path/data/:path
        // https://127.0.0.1:8200/v1/secret/data/my-secret for v2 
        
        await vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(path, data, mountPoint: mountPoint)
            .ConfigureAwait(false);
    }

    // $"users/{username}/databases{databaseId}"
    public async Task<object> GetCredentialByPath(string path, string mountPoint)
    {
        var secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path, mountPoint:mountPoint);

        if (secret == null)
        {
            throw new HttpException(Constants.ErrorMessages.CredentialNotFound, 404);
        }

        return secret.Data;
    }
    // $"users/{username}/databases"

    public async Task<string[]> GetAllCredentials(string path, string mountPoint)
    {
        var secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretPathsAsync(path, mountPoint);

        if (secret == null)
        {
            throw new HttpException(Constants.ErrorMessages.CredentialNotFound, 404);
        }

        return secret.Data.Keys.ToArray();
    }
}