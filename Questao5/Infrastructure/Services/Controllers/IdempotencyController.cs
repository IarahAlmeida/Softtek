using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests.IdempotencyRequests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdempotencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdempotencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new IdempotencyGetAllQueryRequest());
            return Ok(response);  // TODO: handle error
        }
    }
}