using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Core
{
    public interface IUserGroupRepository : IRepository<UserGroup>
    {
        UserGroup Find(long key);
    }
}
