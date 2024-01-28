
using System.Reflection.Metadata;
using Schemes.Exceptions;
using VaultSharp;

namespace Business.Services;

public interface IVaultService
{
    Task SaveOrUpdateCredentials<T>(string path, T values);
    Task<object> GetCredentialByPath(string path);
    Task<string[]> GetAllCredentials(string path);

}

public class VaultService : IVaultService
{
    private readonly IVaultClient vaultClient;

    public VaultService(IVaultClient vaultClient)
    {
        this.vaultClient = vaultClient;
    }

    // $"users/{username}/databases{databaseId}"
    public async Task SaveOrUpdateCredentials<T>(string path, T values)
    {
        await vaultClient.V1.Secrets.KeyValue.V1.WriteSecretAsync(path, values);
    }
    
    // $"users/{username}/databases{databaseId}"
    public async Task<object> GetCredentialByPath(string path)
    {
        var secret = await vaultClient.V1.Secrets.KeyValue.V1.ReadSecretAsync(path);
        
        if (secret == null)
        {
            throw new HttpException(Constants.ErrorMessages.CredentialNotFound, 404);
        }
        
        return secret.Data;
    }
    // $"users/{username}/databases"

    public async Task<string[]> GetAllCredentials(string path)
    {
        var secret = await vaultClient.V1.Secrets.KeyValue.V1.ReadSecretPathsAsync(path);

        if (secret == null)
        {
            throw new HttpException(Constants.ErrorMessages.CredentialNotFound, 404);
        }
        return secret.Data.Keys.ToArray();
    }
    
}