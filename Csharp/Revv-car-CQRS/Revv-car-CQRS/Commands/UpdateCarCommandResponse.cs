using MediatR;
using Revv_car_CQRS.Repository;

namespace Revv_car_CQRS.Commands
{
    public class UpdateCarCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
    }
}
