using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Business.Services;

public class DapperService : IDapperService
{
    // make connection string changeable

    private string _connectionString;
    
    
    public DapperService Create(string connectionString)
    {
        return new DapperService(connectionString);
    }
    
    public DapperService()
    {
        _connectionString = "";
    }
    
    private DapperService(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    /// <summary>
    /// Test connection 
    /// </summary>
    /// <returns></returns>
    public async Task<bool> TestConnection(CancellationToken cancellationToken = default)
    {
        using (var connection = await CreateConnection())
        {
            try
            {
                // OpenAsync with cancellation support
                await connection.OpenAsync(cancellationToken);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }

    private Task<SqlConnection> CreateConnection()
    {
        var connection = new SqlConnection(_connectionString);
        return Task.FromResult(connection);
    }

    /// <summary>
    /// Executes a query and returns a list of results.
    /// </summary>
    public async Task<List<T>> Query<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnection())
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
        using (var connection = await CreateConnection())
        {
            return connection.QueryFirstOrDefault<T>(sql, parameters);
        }
    }

    /// <summary>
    /// Executes a query and returns a single result or default.
    /// </summary>
    public async Task<T?> QuerySingleOrDefault<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }
    }

    /// <summary>
    /// Executes a query and returns multiple results.
    /// </summary>
    public async Task<IEnumerable<T>> QueryMultiple<T>(string sql, object parameters = null)
    {
        using (var connection = await CreateConnection())
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

        using (var connection = await CreateConnection())
        {
            return await connection.QueryAsync<T>(sql, parameters);
        }
    }
}

public interface IDapperService
{
    DapperService Create(string connectionString);
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