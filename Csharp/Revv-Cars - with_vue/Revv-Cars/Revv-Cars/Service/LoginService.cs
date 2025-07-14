using Microsoft.Extensions.Options;
using Revv_Cars.Model;
using Revv_Cars.Repository;

namespace Revv_Cars.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? GetByUsername(string username)
            => _userRepository.GetByUsername(username);

        public void Register(User user)
            => _userRepository.Create(user);
    }
}
