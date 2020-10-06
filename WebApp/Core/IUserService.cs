using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Core
{
    public interface IUserService
    {
        Task<User> PutAsync(User user);
        Task<IEnumerable<User>> GetAsync();
    }
}