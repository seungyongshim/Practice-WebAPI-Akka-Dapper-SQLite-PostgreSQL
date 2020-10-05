using Dapper;
using Database.Core;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;

namespace Database.SQLite
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(string SqlConnection, ILogger<UserRepository> logger)
        {
            Connect = () => new SqliteConnection(SqlConnection);
            Logger = logger;
        }

        public Func<IDbConnection> Connect { get; }
        public ILogger<UserRepository> Logger { get; }

        public void Delete(long key)
        {
            using (var c = Connect())
            {
                c.Open();
                c.Execute($@"DELETE FROM USERS WHERE USER_ID = {key}");
            }
        }

        public void Delete(User userInformation)
        {
            using (var c = Connect())
            {
                c.Open();
                c.Execute($@"DELETE FROM USERS WHERE USER_ID = {userInformation.USER_ID}");
            }
        }

        public IEnumerable<User> FindAll()
        {
            using (var c = Connect())
            {
                c.Open();
                return c.Query<User>("SELECT * FROM USERS");
            }
        }

        public void Initialize()
        {
            Logger.LogTraceStart();
            using (var c = Connect() as SqliteConnection)
            {
                c.Open();

                var tableCommand = "CREATE TABLE IF NOT "
                                 + "EXISTS USERS "
                                 + "("
                                 + "USER_ID INTEGER PRIMARY KEY, "
                                 + "PASSWORD TEXT, "
                                 + "USER_NAME TEXT, "
                                 + "USER_GROUP TEXT, "
                                 + "BLOB BLOB "
                                 + ")";

                SqliteCommand createTable = new SqliteCommand(tableCommand, c);

                createTable.ExecuteReader();
            }
            Logger.LogTraceEnd();
        }

        public void Insert(User user)
        {
            using (var c = Connect())
            {
                c.Open();
                c.Execute(@"INSERT INTO USERS (PASSWORD, USER_NAME, USER_GROUP, BLOB)" +
                                  "VALUES (:PASSWORD, :USER_NAME, :USER_GROUP, :BLOB)", user);
            }
        }
    }
}