using Dapper;
using Database.Core;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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
            var t = UnitOfWork.DbTransaction as SqliteTransaction;

            var tableCommand = "CREATE TABLE IF NOT "
                             + "EXISTS USERS "
                             + "("
                             + "USER_ID INTEGER PRIMARY KEY, "
                             + "PASSWORD TEXT, "
                             + "USER_NAME TEXT, "
                             + "USER_GROUP TEXT, "
                             + "BLOB BLOB "
                             + ")";

            var createTable = new SqliteCommand(tableCommand, c, t);

            createTable.ExecuteReader();
            Logger.LogTraceEnd();
        }

        public void Insert(User user)
        {
            UnitOfWork.DbConnection.Execute(@"INSERT INTO USERS (PASSWORD, USER_NAME, USER_GROUP, BLOB)" +
                                             "VALUES (:PASSWORD, :USER_NAME, :USER_GROUP, :BLOB)", user);
        }
    }
}