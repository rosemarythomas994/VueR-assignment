using MediatR;
using Revv_car_CQRS.Model;
using Revv_car_CQRS.Repository;

namespace Revv_car_CQRS.Commands
{
    public class CreateCarCommandResponse
    {
        public string Id { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public string Place { get; set; } = null!;
        public int Number { get; set; }
        public string Date { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
