using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using Revv.cars.Shared.Commands;

using Revv.cars.handler.RepoInterface;

namespace Revv.Cars.handler.Handler
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        private readonly ICarRepository _repo;

        public CreateCarCommandHandler(ICarRepository repo)
        {
            _repo = repo;
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

            // Ensure the correct type is used for 'Car'
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
