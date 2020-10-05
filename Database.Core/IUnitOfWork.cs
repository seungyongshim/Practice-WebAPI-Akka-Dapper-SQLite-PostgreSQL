using System;

namespace Database.Core
{
    internal interface IUnitOfWork<T> : IDisposable
    {
    }
}