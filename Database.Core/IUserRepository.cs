using Domain;
using System.Collections.Generic;

namespace Database.Core
{
    public interface IUserRepository
    {
        void Delete(long key);
        void Delete(User user);
        IEnumerable<User> FindAll();
        void Initialize();
        void Insert(User user);
    }
}