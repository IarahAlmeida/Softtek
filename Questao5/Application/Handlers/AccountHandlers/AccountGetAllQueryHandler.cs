using MediatR;
using Questao5.Application.Queries.Requests.AccountRequests;
using Questao5.Application.Repositories.Interfaces;
using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers.AccountHandlers
{
    public class AccountGetAllQueryHandler : IRequestHandler<AccountGetAllQueryRequest, IEnumerable<Account>>
    {
        private readonly IRepository<Account> _repository;

        public AccountGetAllQueryHandler(IRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Account>> Handle(AccountGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _repository.GetAll();

                return await Task.FromResult(accounts);
            }
            catch (Exception)
            {
                return await Task.FromResult(new List<Account>()); // TODO: create a query response
            }
        }
    }
}