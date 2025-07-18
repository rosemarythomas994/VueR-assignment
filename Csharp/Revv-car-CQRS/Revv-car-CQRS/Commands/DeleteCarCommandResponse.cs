using MediatR;
using Revv_car_CQRS.Repository;

namespace Revv_car_CQRS.Commands
{
    public class DeleteCarCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
