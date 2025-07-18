using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Revv_Cars.Model;
using Revv_Cars.Service;

namespace Revv_Cars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var cars = _carService.Get();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || id.Length != 24)
                    return BadRequest("Invalid or missing Car ID.");

                var car = _carService.Get(id);
                if (car == null)
                    return NotFound($"Car with ID {id} not found.");

                return Ok(car);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Car car)
        {
            try
            {
                if (car == null)
                    return BadRequest("Car data is missing.");

                var createdCar = _carService.Create(car);
                return CreatedAtAction(nameof(Get), new { id = createdCar.Id }, createdCar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }

        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Car carIn)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || carIn == null || string.IsNullOrEmpty(carIn.Id))
                    return BadRequest("Car ID or data is missing.");

                if (id.Length != 24 || carIn.Id.Length != 24)
                    return BadRequest("Invalid Car ID format.");

                if (id != carIn.Id)
                    return BadRequest($"Route ID '{id}' does not match body ID '{carIn.Id}'.");

                var existingCar = _carService.Get(id);
                if (existingCar == null)
                    return NotFound($"Car with ID {id} not found.");

                _carService.Update(id, carIn);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }


        }
        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] Car carIn)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }
    }
}
