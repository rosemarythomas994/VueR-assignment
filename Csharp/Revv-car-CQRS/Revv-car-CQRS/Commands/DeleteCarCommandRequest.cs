using MediatR;

namespace Revv_car_CQRS.Commands
{
    public class DeleteCarCommandRequest : IRequest<DeleteCarCommandResponse>
    {
        public string Id { get; set; } = default!;
    }
}
