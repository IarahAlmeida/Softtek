using MediatR;
using Questao5.Application.Queries.Requests.MovementRequests;
using Questao5.Application.Repositories.Interfaces;
using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers.MovementHandlers
{
    public class MovementGetAllQueryHandler : IRequestHandler<MovementGetAllQueryRequest, IEnumerable<Movement>>
    {
        private readonly IMovementRepository<Movement> _repository;

        public MovementGetAllQueryHandler(IMovementRepository<Movement> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Movement>> Handle(MovementGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var movements = await _repository.GetAll();

                return await Task.FromResult(movements);
            }
            catch (Exception)
            {
                return await Task.FromResult(new List<Movement>()); // TODO: create a query response
            }
        }
    }
}