using Domain;
using System.Threading.Tasks;

namespace WebApp.Core
{
    public interface IUserService
    {
        Task PutAsync(User user);
    }
}