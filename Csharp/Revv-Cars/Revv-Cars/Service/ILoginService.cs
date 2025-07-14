using Revv_Cars.Model;

namespace Revv_Cars.Service
{
    public interface ILoginService
    {
        User? GetByUsername(string username);
        void Register(User user);
    }
}
