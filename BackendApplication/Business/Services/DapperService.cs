using System.Data;
using System.Data.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using Schemes.Enums;

namespace Business.Services;

// public class DapperServiceFactory
// {
//     private readonly DapperService _dapperService;
//     public DapperServiceFactory(IOptions<LabelGenOptions> options)
//     {
//         var value = options.Value;
//         _dapperService = new(value.Prefix, value.Suffix);
//     }
//     public DapperService GetDapperService() => _dapperService;
// }
//
// public static class DapperServiceFactory
// {
//     public static IDapperService CreateDapperService(string connectionString, DatabaseType providerName)
//     {
//         var dapperService = new DapperService(connectionString, providerName);
//         return dapperService;
//     }
// }

public interface IDapperServiceFactory
{
    IDapperService Create(string connectionString, DatabaseType providerName);
}

public class DapperServiceFactory : IDapperServiceFactory
{
    public IDapperService Create(string connectionString, DatabaseType providerName)
    {
        return DapperService.CreateDapperService(connectionString, providerName);
    }
}

public class DapperService : IDapperService
{
    // make connection string changeable

    private string _connectionString;
    private DatabaseType _providerName;

    private DapperService(string connectionString, DatabaseType providerName)
    {
        _connectionString = connectionString;
        _providerName = providerName;
    }
    
    // public DapperService Create(string connectionString, DatabaseType providerName)
    // {
    //     return new DapperService(connectionString, providerName);
    // }

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
            DatabaseType.PostgreSql => Task.FromResult<DbConnection>(new NpgsqlConnection(_connectionString)),
            DatabaseType.Oracle => Task.FromResult<DbConnection>(new OracleConnection(_connectionString)),
            _ => throw new ArgumentOutOfRangeException(nameof(providerName), providerName, null)
        };
    }
    
    
    /// <summary>
    /// Test connection 
    /// </summary>
    /// <returns></returns>
    public async Task<bool> TestConnection(CancellationToken cancellationToken = default)
    {
        try
        {
            using (var connection = CreateConnectionAsync(_providerName))
            {
                // OpenAsync with cancellation support
                await connection.Result.OpenAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Executes a query and returns a list of results.
    /// </summary>
    public async Task<List<T>> Query<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnectionAsync(_providerName))
        {
            var result = await connection.QueryAsync<T>(sql, parameters);
            return result.ToList();
        }
    }

    /// <summary>
    /// Executes a query and returns the first result or default.
    /// </summary>
    public async Task<T?> QueryFirstOrDefault<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnectionAsync(_providerName))
        {
            return connection.QueryFirstOrDefault<T>(sql, parameters);
        }
    }

    /// <summary>
    /// Executes a query and returns a single result or default.
    /// </summary>
    public async Task<T?> QuerySingleOrDefault<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnectionAsync(_providerName))
        {
            return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }
    }

    /// <summary>
    /// Executes a query and returns multiple results.
    /// </summary>
    public async Task<IEnumerable<T>> QueryMultiple<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnectionAsync(_providerName))
        {
            return await connection.QueryAsync<T>(sql, parameters);
        }
    }


    /// <summary>
    /// Executes a query with paging and returns a subset of results.
    /// </summary>
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
    
    // DapperService Create(string connectionString, DatabaseType providerName);
    Task<bool> TestConnection(CancellationToken cancellationToken = default);
    Task<T?> QueryFirstOrDefault<T>(string sql, object parameters = null);

    Task<List<T>> Query<T>(string sql, object parameters = null);

    Task<T?> QuerySingleOrDefault<T>(string sql, object parameters = null);

    Task<IEnumerable<T>> QueryMultiple<T>(string sql, object parameters = null);

    Task<IEnumerable<T>> QueryWithPaging<T>(string sql, int page, int pageSize, object parameters = null);
}


/*/// <summary>
/// Executes a transaction with the provided action.
/// (Command)
/// </summary>
public void ExecuteTransaction(Action<IDbConnection, IDbTransaction> action)
{
    using (var connection = CreateConnection())
    {
        connection.Open();
        using (var transaction = connection.BeginTransaction())
        {
            try
            {
                action(connection, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}*/

/*/// <summary>
/// Executes a non-query SQL statement and returns the number of affected rows.
/// (Command)
/// </summary>
public int Execute(string sql, object parameters = null)
{
    using (var connection = CreateConnection())
    {
        return connection.Execute(sql, parameters);
    }
}*/

/*/// <summary>
/// Executes a scalar query and returns the result.
/// </summary>
public T ExecuteScalar<T>(string sql, object parameters = null)
{
    using (var connection = CreateConnection())
    {
        return connection.ExecuteScalar<T>(sql, parameters);
    }
}*/