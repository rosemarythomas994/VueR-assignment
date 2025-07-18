using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Revv_Cars.Model;
using Revv_Cars.Service;

namespace Revv_Cars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarService> _logger;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var cars = _carService.Get();
            return Ok(cars);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 24)
                return BadRequest("Invalid or missing Car ID.");

            var car = _carService.Get(id);
            if (car == null)
                return NotFound($"Car with ID {id} not found.");

            return Ok(car);
        }
        //[Authorize]
        //[HttpPost]
        //public IActionResult Create([FromBody] Car car)
        //{
        //    try
        //    {
        //        if (car == null)
        //            return BadRequest("Car data is missing.");

        //        var createdCar = _carService.Create(car);
        //        return CreatedAtAction(nameof(Get), new { id = createdCar.Id }, createdCar);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Server error: {ex.Message}");
        //    }

        //}
        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CarUpload carUpload)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value.Errors.Select(err => err.ErrorMessage).ToArray()
                    );

                return BadRequest(new { message = "Validation failed", errors });
            }

            if (carUpload.ImageFile == null || carUpload.ImageFile.Length == 0)
                return BadRequest("Image file is required.");

            
                var imageFolder = Path.Combine("wwwroot", "images");
                if (!Directory.Exists(imageFolder))
                    Directory.CreateDirectory(imageFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(carUpload.ImageFile.FileName);
                var filePath = Path.Combine(imageFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await carUpload.ImageFile.CopyToAsync(stream);
                }

                var car = new Car
                {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Image = fileName,
                    Brand = carUpload.Brand,
                    Model = carUpload.Model,
                    Year = carUpload.Year,
                    Place = carUpload.Place,
                    Number = carUpload.Number,
                    Date = carUpload.Date,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = carUpload.UserId
                };

                var created = _carService.Create(car);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        
            
        }





        //[Authorize]
        //[HttpPut("{id}")]
        //public IActionResult Update(string id, [FromBody] Car carIn)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(id) || carIn == null || string.IsNullOrEmpty(carIn.Id))
        //            return BadRequest("Car ID or data is missing.");

        //        if (id.Length != 24 || carIn.Id.Length != 24)
        //            return BadRequest("Invalid Car ID format.");

        //        if (id != carIn.Id)
        //            return BadRequest($"Route ID '{id}' does not match body ID '{carIn.Id}'.");

        //        var existingCar = _carService.Get(id);
        //        if (existingCar == null)
        //            return NotFound($"Car with ID {id} not found.");

        //        _carService.Update(id, carIn);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Server error: {ex.Message}");
        //    }


        //}

        //[HttpPut("{id}")]
        //[Authorize]
        //public IActionResult Update(string id, [FromBody] Car carIn)
        //{
        //    if (string.IsNullOrEmpty(id) || carIn == null || string.IsNullOrEmpty(carIn.Id))
        //        return BadRequest("Car ID or data is missing.");

        //    if (id != carIn.Id)
        //        return BadRequest($"Route ID '{id}' does not match body ID '{carIn.Id}'.");

        //    var existingCar = _carService.Get(id);
        //    if (existingCar == null)
        //        return NotFound();

        //    carIn.UpdatedAt = DateTime.UtcNow;
        //    _carService.Update(id, carIn);
        //    return NoContent();
        //}
        [HttpPut("{id}")]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateWithImage(string id, [FromForm] CarUpload carUpload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCar = _carService.Get(id);
            if (existingCar == null)
                return NotFound($"Car with ID {id} not found.");

            string fileName = existingCar.Image;

            if (carUpload.ImageFile != null && carUpload.ImageFile.Length > 0)
            {
                var folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);

                fileName = Guid.NewGuid() + Path.GetExtension(carUpload.ImageFile.FileName);
                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await carUpload.ImageFile.CopyToAsync(stream);

                // Delete old image if exists
                var oldImagePath = Path.Combine(folder, existingCar.Image ?? "");
                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);
            }

            existingCar.Image = fileName;
            existingCar.Brand = carUpload.Brand;
            existingCar.Model = carUpload.Model;
            existingCar.Year = carUpload.Year;
            existingCar.Place = carUpload.Place;
            existingCar.Number = carUpload.Number;
            existingCar.Date = carUpload.Date;
            existingCar.UserId = carUpload.UserId;
            existingCar.UpdatedAt = DateTime.UtcNow;

            _carService.Update(id, existingCar);
            return NoContent();
        }



        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] Car carIn)
        {
                if (carIn == null || string.IsNullOrEmpty(carIn.Id))
                    return BadRequest("Car or ID is missing.");

                if (carIn.Id.Length != 24)
                    return BadRequest("Invalid Car ID format.");

                var existingCar = _carService.Get(carIn.Id);
                if (existingCar == null)
                    return NotFound($"Car with ID {carIn.Id} not found.");

                _carService.Update(carIn.Id, carIn);
                return NoContent();
            
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            
                if (string.IsNullOrEmpty(id))
                    return BadRequest("Car ID is missing.");

                if (id.Length != 24)
                    return BadRequest("Invalid Car ID format. Must be 24-character hex string.");

                var car = _carService.Get(id);
                if (car == null)
                    return NotFound($"No car found with ID: {id}");

                if (car.Id != id)
                    return NotFound($"Car ID mismatch. Requested ID: {id}, found ID: {car.Id}");

                _carService.Remove(id);
                return NoContent();
           
        }
    }
}
