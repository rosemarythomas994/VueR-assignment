using Revv_Cars.Model;

namespace Revv_Cars.Repository
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        User? GetByUsernameAndPassword(string username, string password);
        void Create(User user);
    }

}
