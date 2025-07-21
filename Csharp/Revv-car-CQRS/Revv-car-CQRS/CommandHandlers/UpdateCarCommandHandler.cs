using MediatR;
using Revv_car_CQRS.Commands;
using Revv_car_CQRS.Repository;

namespace Revv_car_CQRS.CommandHandlers
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommandRequest, UpdateCarCommandResponse>
    {
        private readonly ICarRepository _repo;

        public UpdateCarCommandHandler(ICarRepository repo)
        {
            _repo = repo;
        }

        public async Task<UpdateCarCommandResponse> Handle(UpdateCarCommandRequest cmd, CancellationToken ct)
        {
            var car = _repo.Get(cmd.Id);
            if (car == null)
                return new UpdateCarCommandResponse { Success = false, Message = $"Car with ID {cmd.Id} not found." };

            // Handle image upload
            if (cmd.ImageFile != null && cmd.ImageFile.Length > 0)
            {
                var folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);

                var ext = Path.GetExtension(cmd.ImageFile.FileName);
                var fileName = $"{cmd.Brand.Replace(" ", "_")}_{cmd.Model.Replace(" ", "_")}_{cmd.UserId.Split('@')[0].Replace(".", "_")}_{DateTime.UtcNow:yyyyMMddHHmmss}{ext}";
                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await cmd.ImageFile.CopyToAsync(stream, ct);

                // Delete old image if it exists
                if (!string.IsNullOrEmpty(car.Image))
                {
                    var oldPath = Path.Combine(folder, car.Image);
                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

                car.Image = fileName;
            }

            if (!int.TryParse(cmd.Number, out var parsedNumber))
                return new UpdateCarCommandResponse { Success = false, Message = "Invalid Number format." };

            if (!DateTime.TryParse(cmd.Date, out var parsedDate))
                return new UpdateCarCommandResponse { Success = false, Message = "Invalid Date format." };

            // Update fields
            car.Brand = cmd.Brand;
            car.Model = cmd.Model;
            car.Year = cmd.Year;
            car.Place = cmd.Place;
            car.Number = parsedNumber;
            car.Date = parsedDate.ToString("yyyy-MM-dd");
            car.UserId = cmd.UserId;
            car.UpdatedAt = DateTime.UtcNow;

            _repo.Update(cmd.Id, car);

            return new UpdateCarCommandResponse { Success = true, Message = "Car updated successfully." };
        }
    }
}
