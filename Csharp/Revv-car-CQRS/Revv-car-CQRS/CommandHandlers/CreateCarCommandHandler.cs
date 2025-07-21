using MediatR;
using Revv_car_CQRS.Commands;
using Revv_car_CQRS.Model;
using Revv_car_CQRS.Notifications;
using Revv_car_CQRS.Repository;

namespace Revv_car_CQRS.CommandHandlers
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        private readonly ICarRepository _repo;
        private readonly IMediator _mediator;
        public CreateCarCommandHandler(ICarRepository repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }

        public async Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest cmd, CancellationToken ct)
        {
            var folder = Path.Combine("wwwroot", "images");
            Directory.CreateDirectory(folder);

            var ext = Path.GetExtension(cmd.ImageFile.FileName);
            var safeUser = cmd.UserId.Split('@')[0].Replace(".", "_");
            var filename = $"{cmd.Brand}_{cmd.Model}_{safeUser}_{DateTime.UtcNow:yyyyMMddHHmmss}{ext}";
            var path = Path.Combine(folder, filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await cmd.ImageFile.CopyToAsync(stream, ct);
            }

            var car = new Car
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                Image = filename,
                Brand = cmd.Brand,
                Model = cmd.Model,
                Year = cmd.Year,
                Place = cmd.Place,
                Number = cmd.Number,
                Date = cmd.Date.ToString("yyyy-MM-dd"),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = cmd.UserId
            };

            var result = _repo.Create(car);
            await _mediator.Publish(new CarCreatedEvent
            {
                CarId = result.Id,
                UserId = result.UserId,
                Brand = result.Brand,
                Model = result.Model,
                Year = result.Year,
                Place = result.Place
            });
            return new CreateCarCommandResponse
            {
                Id = result.Id,
                Image = result.Image,
                Brand = result.Brand,
                Model = result.Model,
                Year = result.Year,
                Place = result.Place,
                Number = result.Number,
                Date = result.Date,
                CreatedAt = result.CreatedAt ?? DateTime.MinValue,
                UpdatedAt = result.UpdatedAt ?? DateTime.MinValue,
                UserId = result.UserId
            };
        }
    }
}
