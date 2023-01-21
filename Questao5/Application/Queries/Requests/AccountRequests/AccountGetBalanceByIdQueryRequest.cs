using MediatR;
using Questao5.Application.Queries.Responses.AccountResponses;

namespace Questao5.Application.Queries.Requests.AccountRequests
{
    public class AccountGetBalanceByIdQueryRequest : IRequest<AccountGetBalanceByIdQueryResponse>
    {
        public string Id { get; set; } = string.Empty;
    }
}