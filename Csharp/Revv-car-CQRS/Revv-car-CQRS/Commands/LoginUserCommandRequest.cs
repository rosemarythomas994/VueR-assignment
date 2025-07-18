using MediatR;

namespace Revv_car_CQRS.Commands
{
    public class LoginUserCommandRequest : IRequest<string> 
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

}
