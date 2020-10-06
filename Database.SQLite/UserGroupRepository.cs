using Dapper;
using Database.Core;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Database.SQLite
{
    public class UserGroupRepository : IUserGroupRepository
    {
        public UserGroupRepository(IUnitOfWork unitOfWork, ILogger<UserGroupRepository> logger)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        public IUnitOfWork UnitOfWork { get; }
        public ILogger<UserGroupRepository> Logger { get; }

        public void Initialize()
        {
            Logger.LogTraceStart();
            var c = UnitOfWork.DbConnection as SqliteConnection;

            var tableCommand = "CREATE TABLE IF NOT "
                             + "EXISTS USERGROUPS "
                             + "("
                             + "ID INTEGER PRIMARY KEY, "
                             + "NAME TEXT "
                             + ")";

            var createTable = new SqliteCommand(tableCommand, c);

            createTable.ExecuteReader();
            Logger.LogTraceEnd();
        }

        public UserGroup Find(long key)
        {
            return UnitOfWork.DbConnection.Query<UserGroup>($"SELECT * FROM USERS WHERE ID = {key}").First();
        }
    }
}