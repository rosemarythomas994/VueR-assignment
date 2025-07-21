using MediatR;
using Revv_car_CQRS.Commands;
using Revv_car_CQRS.Model;
using Revv_car_CQRS.Repository;

namespace Revv_car_CQRS.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        private readonly IUserRepository _userRepo;

        public RegisterUserCommandHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            var existingUser = _userRepo.GetByUsername(request.Username);
            if (existingUser != null)
            {
                return Task.FromResult(new RegisterUserCommandResponse
                {
                    Success = false,
                    Message = "Username already exists"
                });
            }

            var hashed = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                Password = hashed,
                Role = request.Role
            };

            _userRepo.Create(user);

            return Task.FromResult(new RegisterUserCommandResponse
            {
                Success = true,
                Message = "User registered successfully"
            });
        }
    }
}
