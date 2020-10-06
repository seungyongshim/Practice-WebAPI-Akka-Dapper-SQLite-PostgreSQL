using Dapper;
using Database.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Database.PostgreSQL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
            DbConnection.Open();
        }

        public IDbTransaction DbTransaction { get; private set; }
        public IDbConnection DbConnection { get; }

        public IUnitOfWork BeginTransaction()
        {
            DbTransaction = DbConnection.BeginTransaction();
            return this;
        }

        public void Commit()
        {
            DbTransaction?.Commit();
        }

        public void Rollback()
        {
            DbTransaction?.Rollback();
        }

        private bool disposed = false;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
