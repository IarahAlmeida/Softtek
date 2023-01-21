using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Requests.MovementRequests
{
    public class MovementGetAllQueryRequest : IRequest<IEnumerable<Movement>>
    {
    }
}