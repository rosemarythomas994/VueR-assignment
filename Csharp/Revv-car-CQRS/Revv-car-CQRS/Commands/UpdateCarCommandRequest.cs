using MediatR;

namespace Revv_car_CQRS.Commands
{
    public class UpdateCarCommandRequest : IRequest<UpdateCarCommandResponse>
    {
        public string Id { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public string Place { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
