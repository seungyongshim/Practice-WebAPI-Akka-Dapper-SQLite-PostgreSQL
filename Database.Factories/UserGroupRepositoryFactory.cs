using Database.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Factories
{
    public class UserGroupRepositoryFactory
    {
        public UserGroupRepositoryFactory(IOptions<DatabaseOptions> options)
        {
            Options = options.Value;
        }

        public DatabaseOptions Options { get; }

        public IUserGroupRepository Make(IServiceProvider sp) => Options.DatabaseType switch
        {
            DatabaseType.None => throw new System.NotImplementedException(),
            DatabaseType.Oracle => throw new System.NotImplementedException(),
            DatabaseType.PostgreSQL => new PostgreSQL.UserGroupRepository(sp.GetService<IUnitOfWork>(),
                                                                          sp.GetService<ILogger<PostgreSQL.UserGroupRepository>>()),
            DatabaseType.SQLite => new SQLite.UserGroupRepository(sp.GetService<IUnitOfWork>(),
                                                                  sp.GetService<ILogger<SQLite.UserGroupRepository>>()),
            _ => throw new NotImplementedException(),
        };
    }
}
