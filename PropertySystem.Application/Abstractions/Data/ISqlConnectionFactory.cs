using System.Data;

namespace PropertySystem.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    Task<IDbConnection> CreateConnection();
}
