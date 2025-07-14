using Revv_Cars.Model;
using RevvCar.Models;

namespace Revv_Cars.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(User user);
    }

}
