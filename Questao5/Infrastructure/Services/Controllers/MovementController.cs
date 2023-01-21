using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests.MovementRequests;
using Questao5.Application.Queries.Requests.MovementRequests;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new MovementGetAllQueryRequest());
            return Ok(response); // TODO: handle error
        }

        [HttpPost]
        public async Task<IActionResult> Post(MovementCreateCommandRequest command)
        {
            var response = await _mediator.Send(command);
            if (response.ErrorMessage != null)
            {
                if (response.ErrorCode == ErrorType.Internal.ToString())
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}