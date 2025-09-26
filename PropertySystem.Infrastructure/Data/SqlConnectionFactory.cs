using PropertySystem.Application.Abstractions.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace PropertySystem.Infrastructure.Data;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;
    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<IDbConnection> CreateConnection()
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        return connection;
    }
}
