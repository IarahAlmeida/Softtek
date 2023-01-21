using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests.AccountRequests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new AccountGetAllQueryRequest());
            return Ok(response);  // TODO: handle error
        }

        [HttpGet("GetBalance")]
        public async Task<IActionResult> GetBalance([FromQuery] AccountGetBalanceByIdQueryRequest query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);  // TODO: handle error
        }
    }
}