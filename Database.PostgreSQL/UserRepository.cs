using Dapper;
using Database.Core;
using Domain;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Collections.Generic;

namespace Database.PostgreSQL
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork, ILogger<UserRepository> logger)
        {
            UnitOfWork = unitOfWork;
        }

        public ILogger Logger { get; set; }
        public IUnitOfWork UnitOfWork { get; }

        public void Delete(long key)
        {
            UnitOfWork.DbConnection.Execute($@"DELETE FROM USERS WHERE USER_ID = {key}",
                                            transaction: UnitOfWork.DbTransaction);
        }

        public void Delete(User userInformation)
        {
            UnitOfWork.DbConnection.Execute($@"DELETE FROM USERS WHERE USER_ID = {userInformation.USER_ID}",
                                            transaction: UnitOfWork.DbTransaction);
        }

        public IEnumerable<User> FindAll()
        {
            return UnitOfWork.DbConnection.Query<User>("SELECT * FROM USERS");
        }

        public void Initialize()
        {
            var tableCommand = "CREATE TABLE IF NOT "
                             + "EXISTS USERS "
                             + "("
                             + "USER_ID BIGSERIAL PRIMARY KEY, "
                             + "PASSWORD TEXT, "
                             + "USER_NAME TEXT, "
                             + "USER_GROUP TEXT, "
                             + "BLOB bytea "
                             + ")";

            var createTable = new NpgsqlCommand(tableCommand, UnitOfWork.DbConnection as NpgsqlConnection);
            createTable.ExecuteReader();
        }

        public User Insert(User user)
        {
            var query = "INSERT INTO USERS (PASSWORD, USER_NAME, USER_GROUP, BLOB)"
                      + "VALUES (:PASSWORD, :USER_NAME, :USER_GROUP, :BLOB)"
                      + "RETURNING *";
            
            return UnitOfWork.DbConnection.QuerySingle<User>(query,
                                                             user,
                                                             transaction: UnitOfWork.DbTransaction);
        }
    }
}