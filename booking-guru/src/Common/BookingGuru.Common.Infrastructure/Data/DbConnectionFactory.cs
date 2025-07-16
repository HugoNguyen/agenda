using BookingGuru.Common.Application.Data;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace BookingGuru.Common.Infrastructure.Data;

internal sealed class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        return conn;
    }
}
