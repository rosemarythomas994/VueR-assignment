using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revv.cars.Domain.Model;
using Revv.cars.Shared.Commands;
using Revv.cars.Shared.Queries;

namespace Revv.cars.Application.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarsQueryRequest());
            return Ok(result.Cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 24)
                return BadRequest("Invalid Car ID format.");

            var result = await _mediator.Send(new GetCarByIdQueryRequest { Id = id });

            if (result == null)
                return NotFound($"Car with ID {id} not found.");

            return Ok(result);
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateCarCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (result == null)
                return BadRequest("Failed to create car.");

            return Ok(result);
        }


        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(string id, [FromForm] CarUpload upload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
                return BadRequest(result.Message);

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 24)
                return BadRequest("Invalid Car ID format.");

            var response = await _mediator.Send(new DeleteCarCommandRequest { Id = id });

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response.Message); 
        }
    }
}
