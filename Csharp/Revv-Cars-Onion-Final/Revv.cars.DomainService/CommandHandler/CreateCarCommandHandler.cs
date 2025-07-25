using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Revv.cars.DomainService.RepoInterface;
using Revv.cars.Shared.Commands;

namespace Revv.cars.DomainService.CommandHandler
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        private readonly ICarRepository _repo;
        private readonly IMapper _mapper;

        public CreateCarCommandHandler(ICarRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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

            // Map from Shared.Car to Domain.Model.Car
            //var sharedCar = new Revv.cars.Shared.Car
            //{
            //    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
            //    Image = filename,
            //    Brand = cmd.Brand,
            //    Model = cmd.Model,
            //    Year = cmd.Year,
            //    Place = cmd.Place,
            //    Number = cmd.Number,
            //    Date = cmd.Date.ToString("yyyy-MM-dd"),
            //    CreatedAt = DateTime.UtcNow,
            //    UpdatedAt = DateTime.UtcNow,
            //    UserId = cmd.UserId
            //};

            //var domainCar = _mapper.Map<Revv.cars.Domain.Model.Car>(sharedCar);
            //var result = _repo.Create(domainCar);

            //return _mapper.Map<CreateCarCommandResponse>(result);


            var sharedCar = _mapper.Map<cars.Shared.Car>(cmd);

            sharedCar.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            sharedCar.Image = filename;
            sharedCar.CreatedAt = DateTime.UtcNow;
            sharedCar.UpdatedAt = DateTime.UtcNow;

            var domainCar = _mapper.Map<cars.Domain.Model.Car>(sharedCar);
            var result = _repo.Create(domainCar);

            return _mapper.Map<CreateCarCommandResponse>(result);

        }
    }
}
