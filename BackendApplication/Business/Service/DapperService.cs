using System.Data.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using Schemes.Enum;

namespace Business.Service;

public interface IDapperServiceFactory
{
    IDapperService Create(string connectionString, DatabaseType providerName);
}

public class DapperServiceFactory : IDapperServiceFactory
{

    /* *******************  STARRED ********************* */
    /*    En önemli kısım burası burada DapperService değil
     * IDapperService döndürüyoruz ve bu sayede runtime içerisinde
     * istediğimiz servisi döndürebiliyoruz. Sadece DapperService değil
     * https://code-maze.com/dotnet-factory-pattern-dependency-injection/
     */
    public IDapperService Create(string connectionString, DatabaseType providerName)
    {
        return DapperService.CreateDapperService(connectionString, providerName);
    }
}

public class DapperService : IDapperService
{
    private string _connectionString;
    private DatabaseType _providerName;

    private DapperService(string connectionString, DatabaseType providerName)
    {
        _connectionString = connectionString;
        _providerName = providerName;
    }

    public static IDapperService CreateDapperService(string connectionString, DatabaseType providerName)
    {
        return new DapperService(connectionString, providerName);
    }
    private Task<DbConnection> CreateConnectionAsync(DatabaseType providerName)
    {
        return providerName switch
        {
            DatabaseType.SqlServer => Task.FromResult<DbConnection>(new SqlConnection(_connectionString)),
            DatabaseType.MySql => Task.FromResult<DbConnection>(new MySqlConnection(_connectionString)),
            DatabaseType.PostgresSql => Task.FromResult<DbConnection>(new NpgsqlConnection(_connectionString)),
            DatabaseType.Oracle => Task.FromResult<DbConnection>(new OracleConnection(_connectionString)),
            _ => throw new ArgumentOutOfRangeException(nameof(providerName), providerName, null)
        };
    }
    
    
    public async Task<(bool, string Message)> TestConnection(CancellationToken cancellationToken = default)
    {
        try
        {
            using (var connection = CreateConnectionAsync(_providerName))
            {
                await connection.Result.OpenAsync(cancellationToken).ConfigureAwait(false);
                return (true, "Connection established successfully");
            }
        }
        catch (Exception e)
        {
            
            return (false, e.Message);
        }
    }

    public async Task<List<T>> Query<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnectionAsync(_providerName))
        {
            var result = await connection.QueryAsync<T>(sql, parameters);
            return result.ToList();
        }
    }

    public async Task<T?> QueryFirstOrDefault<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnectionAsync(_providerName))
        {
            return connection.QueryFirstOrDefault<T>(sql, parameters);
        }
    }
    
    public async Task<T?> QuerySingleOrDefault<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnectionAsync(_providerName))
        {
            return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }
    }
    
    public async Task<IEnumerable<T>> QueryMultiple<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnectionAsync(_providerName))
        {
            return await connection.QueryAsync<T>(sql, parameters);
        }
    }

    
    public async Task<IEnumerable<T>> QueryWithPaging<T>(string sql, int page, int pageSize, object parameters = null)
    {
        var offset = (page - 1) * pageSize;
        sql = $"{sql} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

        using (var connection = await CreateConnectionAsync(_providerName))
        {
            return await connection.QueryAsync<T>(sql, parameters);
        }
    }
}

public interface IDapperService
{
    
    Task<(bool, string Message)> TestConnection(CancellationToken cancellationToken = default);
    Task<T?> QueryFirstOrDefault<T>(string sql, object parameters = null);

    Task<List<T>> Query<T>(string sql, object parameters = null);

    Task<T?> QuerySingleOrDefault<T>(string sql, object parameters = null);

    Task<IEnumerable<T>> QueryMultiple<T>(string sql, object parameters = null);

    Task<IEnumerable<T>> QueryWithPaging<T>(string sql, int page, int pageSize, object parameters = null);
}
