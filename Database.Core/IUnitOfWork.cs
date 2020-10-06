using System;
using System.Data;

namespace Database.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection DbConnection { get; }
        IDbTransaction DbTransaction { get; }

        IUnitOfWork BeginTransaction();
        void Commit();
        void Rollback();
    }
}