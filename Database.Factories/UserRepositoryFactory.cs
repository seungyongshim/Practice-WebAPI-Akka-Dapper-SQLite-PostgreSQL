using Database.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Factories
{
    public class UserRepositoryFactory
    {
        public UserRepositoryFactory(IOptions<DatabaseOptions> options)
        {
            Options = options.Value;
        }

        public DatabaseOptions Options { get; }

        public IUserRepository Make(IServiceProvider sp) => Options.DatabaseType switch
        {
            DatabaseType.None => throw new System.NotImplementedException(),
            DatabaseType.Oracle => throw new System.NotImplementedException(),
            DatabaseType.PostgreSQL => new PostgreSQL.UserRepository(sp.GetService<IUnitOfWork>(),
                                                                  sp.GetService<ILogger<PostgreSQL.UserRepository>>()),
            DatabaseType.SQLite => new SQLite.UserRepository(sp.GetService<IUnitOfWork>(),
                                                             sp.GetService<ILogger<SQLite.UserRepository>>()),
            _ => throw new NotImplementedException(),
        };
    }
}
