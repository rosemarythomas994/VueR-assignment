using MediatR;
using Revv_car_CQRS.Model;

namespace Revv_car_CQRS.Commands
{
    public class CreateCarCommandRequest : IRequest<CreateCarCommandResponse>
    {
        public IFormFile ImageFile { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; }
        public string Place { get; set; } = default!;
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; } = default!;
    }
}
