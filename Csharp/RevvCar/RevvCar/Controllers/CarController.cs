using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevvCar.Models;
using RevvCar.Services;

namespace RevvCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(string id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _carService.CreateCarAsync(car);
            return CreatedAtAction(nameof(GetCarById), new { id = car._id }, car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(string id, [FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingCar = await _carService.GetCarByIdAsync(id);
            if (existingCar == null)
            {
                return NotFound();
            }
            await _carService.UpdateCarAsync(id, car);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(string id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            await _carService.DeleteCarAsync(id);
            return NoContent();
        }
    }
}