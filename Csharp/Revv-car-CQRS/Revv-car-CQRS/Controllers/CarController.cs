using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Revv_car_CQRS.Commands;
using Revv_car_CQRS.Model;
using Revv_car_CQRS.Queries;



namespace Revv_car_CQRS.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CarController> _logger;

        public CarController(IMediator mediator, ILogger<CarController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarsQueryRequest());
            _logger.LogInformation("Retrieved {Count} cars", result.Cars.Count);

            return Ok(result.Cars);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 24)
            {
                _logger.LogWarning("Invalid Car ID format received: {Id}", id);

                return BadRequest("Invalid Car ID format.");
            }
            var result = await _mediator.Send(new GetCarByIdQueryRequest { Id = id });
            _logger.LogInformation("Successfully loggined");

            if (result == null)
            {
                _logger.LogWarning("Car with ID {Id} not found", id);

                return NotFound($"Car with ID {id} not found.");
            }
            _logger.LogInformation("Car with ID {Id} successfully retrieved", id);

            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateCarCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (result == null)
            {
                _logger.LogError("Car creation failed for user {UserId}", request.UserId);
                return BadRequest("Failed to create car.");
            }
            _logger.LogInformation("Car successfully created. ID: {CarId}, User: {UserId}", result.Id, result.UserId);

            return Ok(result);
        }


        [HttpPut("{id}")]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(string id, [FromForm] CarUpload upload)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update failed due to invalid model state for Car ID {Id}", id);
                return BadRequest(ModelState);
            }

            var command = new UpdateCarCommandRequest
            {
                Id = id,
                ImageFile = upload.ImageFile,
                Brand = upload.Brand!,
                Model = upload.Model!,
                Year = upload.Year,
                Place = upload.Place!,
                Number = upload.Number.ToString(),
                Date = upload.Date,
                UserId = upload.UserId!
            };

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                _logger.LogError("Failed to update car with ID {Id}. Reason: {Message}", id, result.Message);
                return BadRequest(result.Message);
            }
            _logger.LogInformation("Car with ID {Id} successfully updated", id);

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 24)
            {
                _logger.LogWarning("Invalid Car ID format for delete request: {Id}", id);
                return BadRequest("Invalid Car ID format.");
            }

            var response = await _mediator.Send(new DeleteCarCommandRequest { Id = id });

            if (!response.IsSuccess)
            {
                _logger.LogWarning("Attempt to delete non-existent car. ID: {Id}", id);
                return NotFound(response.Message);
            }
            _logger.LogInformation("Car with ID {Id} successfully deleted", id);

            return Ok(response.Message);
        }
    }
}
