using Dapper;
using Database.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Database.SQLite
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
            DbConnection.Open();
            DbTransaction = dbConnection.BeginTransaction();
        }

        public IDbTransaction DbTransaction { get; }
        public IDbConnection DbConnection { get; }

        public void Commit()
        {
            DbTransaction.Commit();
        }

        public void Rollback()
        {
            DbTransaction.Rollback();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DbTransaction.Dispose();
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
