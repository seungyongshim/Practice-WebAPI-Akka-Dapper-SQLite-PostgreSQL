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
            UnitOfWork.DbConnection.Execute($@"DELETE FROM USERS WHERE USER_ID = {key}");
        }

        public void Delete(User userInformation)
        {
            UnitOfWork.DbConnection.Execute($@"DELETE FROM USERS WHERE USER_ID = {userInformation.USER_ID}");
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
                             + "USER_ID INTEGER PRIMARY KEY, "
                             + "PASSWORD TEXT, "
                             + "USER_NAME TEXT, "
                             + "USER_GROUP TEXT, "
                             + "BLOB BLOB "
                             + ")";

            var createTable = new SqliteCommand(tableCommand, c);

            createTable.ExecuteReader();
            Logger.LogTraceEnd();
        }

        public User Insert(User user)
        {
            return UnitOfWork.DbConnection.QuerySingle<User>(@"INSERT INTO USERS (PASSWORD, USER_NAME, USER_GROUP, BLOB)" +
                                                              "VALUES (:PASSWORD, :USER_NAME, :USER_GROUP, :BLOB);" +
                                                              "SELECT * FROM USERS WHERE USER_ID = last_insert_rowid();"
                                                              , user
                                                              , UnitOfWork.DbTransaction);
        }
    }
}