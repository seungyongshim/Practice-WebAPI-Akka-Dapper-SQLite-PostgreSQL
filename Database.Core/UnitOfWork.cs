using System;
using System.Data;

namespace Database.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        public UnitOfWork(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
            DbConnection.Open();
        }

        public IDbConnection DbConnection { get; }
        public IDbTransaction DbTransaction { get; private set; }

        public IUnitOfWork BeginTransaction()
        {
            DbTransaction = DbConnection.BeginTransaction();
            return this;
        }

        public void Commit()
        {
            DbTransaction?.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            DbTransaction?.Rollback();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DbTransaction?.Dispose();
                    DbConnection.Dispose();
                }
            }
            disposed = true;
        }
    }
}