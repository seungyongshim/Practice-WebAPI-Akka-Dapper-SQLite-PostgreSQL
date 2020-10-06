using Dapper;
using Database.Core;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Database.SQLite
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork, ILogger<UserRepository> logger)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        public IUnitOfWork UnitOfWork { get; }
        public ILogger<UserRepository> Logger { get; }

        public void Delete(long key)
        {
            UnitOfWork.DbConnection.Execute($@"DELETE FROM USERS WHERE ID = {key}");
        }

        public void Delete(User userInformation)
        {
            UnitOfWork.DbConnection.Execute($@"DELETE FROM USERS WHERE ID = {userInformation.ID}");
        }

        public IEnumerable<User> FindAll()
        {
            return UnitOfWork.DbConnection.Query<User>("SELECT * FROM USERS");
        }

        public void Initialize()
        {
            Logger.LogTraceStart();
            var c = UnitOfWork.DbConnection as SqliteConnection;

            var tableCommand = "CREATE TABLE IF NOT "
                             + "EXISTS USERS "
                             + "("
                             + "ID INTEGER PRIMARY KEY, "
                             + "PASSWORD TEXT, "
                             + "USER_NAME TEXT, "
                             + "USER_GROUP_FK INTEGER, "
                             + "BLOB BLOB "
                             + ")";

            var createTable = new SqliteCommand(tableCommand, c);

            createTable.ExecuteReader();
            Logger.LogTraceEnd();
        }

        public User Insert(User user)
        {
            return UnitOfWork.DbConnection.QuerySingle<User>("INSERT INTO USERS (PASSWORD, USER_NAME, USER_GROUP_FK, BLOB)" +
                                                             "VALUES (:PASSWORD, :USER_NAME, :USER_GROUP_FK, :BLOB);" +
                                                             "SELECT * FROM USERS WHERE ID = last_insert_rowid();",
                                                             user,
                                                             UnitOfWork.DbTransaction);
        }
    }
}