using Domain;
using System.Collections.Generic;

namespace Database.Core
{
    public interface IUserRepository : IRepository<User>
    {
        void Delete(long key);
        void Delete(User user);
        IEnumerable<User> FindAll();
        User Insert(User user);
    }
}