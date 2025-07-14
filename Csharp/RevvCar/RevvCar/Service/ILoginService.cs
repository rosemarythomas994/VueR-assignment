using RevvCar.Models;

namespace RevvCar.Services
{
    public interface ILoginService
    {
        Task<User> AuthenticateAsync(string username, string password);
        string GenerateJwtToken(User user);
    }
}