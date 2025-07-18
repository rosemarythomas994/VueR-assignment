using Revv_car_CQRS.Model;

namespace Revv_car_CQRS.Repository
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        User? GetByUsernameAndPassword(string username, string password);
        void Create(User user);
    }

}
