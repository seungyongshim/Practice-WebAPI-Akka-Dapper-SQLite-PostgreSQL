using Database.Core;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Database.Factories
{
    public class DbConnectionFactory
    {
        public DbConnectionFactory(IOptions<DatabaseOptions> options)
        {
            Options = options.Value;
        }

        public DatabaseOptions Options { get; }

        public IDbConnection Make(IServiceProvider _) => Options.DatabaseType switch
        {
            DatabaseType.None => throw new System.NotImplementedException(),
            DatabaseType.Oracle => throw new System.NotImplementedException(),
            DatabaseType.PostgreSQL => new NpgsqlConnection(Options.ConnectionString),
            DatabaseType.SQLite => new SqliteConnection(Options.ConnectionString),
            _ => throw new NotImplementedException(),
        };
    }
}
