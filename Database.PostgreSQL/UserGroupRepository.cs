using Database.Core;
using Domain;
using Microsoft.Extensions.Logging;

namespace Database.PostgreSQL
{
    public class UserGroupRepository : IUserGroupRepository
    {
        public UserGroupRepository(IUnitOfWork unitOfWork, ILogger<UserGroupRepository> logger)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        public IUnitOfWork UnitOfWork { get; set; }
        internal ILogger<UserGroupRepository> Logger { get; set; }

        public UserGroup Find(long key)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}