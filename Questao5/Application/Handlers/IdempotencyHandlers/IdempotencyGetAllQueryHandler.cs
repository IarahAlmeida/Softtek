using MediatR;
using Questao5.Application.Queries.Requests.AccountRequests;
using Questao5.Application.Queries.Requests.IdempotencyRequests;
using Questao5.Application.Repositories.Interfaces;
using Questao5.Domain.Entities;
using System.Linq;

namespace Questao5.Application.Handlers.AccountHandlers
{
    public class IdempotencyGetAllQueryHandler : IRequestHandler<IdempotencyGetAllQueryRequest, IEnumerable<Idempotency>>
    {
        private readonly IRepository<Idempotency> _repository;

        public IdempotencyGetAllQueryHandler(IRepository<Idempotency> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Idempotency>> Handle(IdempotencyGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var idempotencies = await _repository.GetAll();

                return await Task.FromResult(idempotencies);
            }
            catch (Exception)
            {
                return await Task.FromResult(new List<Idempotency>()); // TODO: create a query response
            }
        }
    }
}