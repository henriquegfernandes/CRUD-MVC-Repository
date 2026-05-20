using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using EstoqueProdutos.DataAccess.Configuration;

namespace EstoqueProdutos.DataAccess.Persistence;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}

public sealed class SqlConnectionFactory(IOptions<DatabaseOptions> options) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection() => new SqlConnection(options.Value.ConnectionString);
}
