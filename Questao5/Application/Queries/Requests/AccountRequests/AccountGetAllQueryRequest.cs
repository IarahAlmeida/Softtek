using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Requests.AccountRequests
{
    public class AccountGetAllQueryRequest : IRequest<IEnumerable<Account>>
    {
    }
}
