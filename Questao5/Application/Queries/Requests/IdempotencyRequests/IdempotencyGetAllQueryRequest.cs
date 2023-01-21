using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Requests.IdempotencyRequests
{
    public class IdempotencyGetAllQueryRequest : IRequest<IEnumerable<Idempotency>>
    {
    }
}