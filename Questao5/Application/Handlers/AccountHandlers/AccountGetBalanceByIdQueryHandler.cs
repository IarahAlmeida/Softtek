using MediatR;
using Questao5.Application.Queries.Requests.AccountRequests;
using Questao5.Application.Queries.Responses.AccountResponses;
using Questao5.Application.Repositories.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Handlers.AccountHandlers
{
    public class AccountGetBalanceByIdQueryHandler : IRequestHandler<AccountGetBalanceByIdQueryRequest, AccountGetBalanceByIdQueryResponse>
    {
        private readonly IRepository<Account> _repository;
        private readonly IMovementRepository<Movement> _movementRepository;

        public AccountGetBalanceByIdQueryHandler(IRepository<Account> repository, IMovementRepository<Movement> movementRepository)
        {
            _repository = repository;
            _movementRepository = movementRepository;
        }

        public async Task<AccountGetBalanceByIdQueryResponse> Handle(AccountGetBalanceByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _repository.Get(request.Id);

                if (account == null)
                {
                    return await SendMappedErrorResponse(ErrorType.InvalidAccount, "Invalid account: account does not exist.");
                }
                else if (!account.IsActive)
                {
                    return await SendMappedErrorResponse(ErrorType.InactiveAccount, "Inactive account: account has been deactivated.");
                }

                var movements = await _movementRepository.GetAllByAccountId(request.Id);

                var balance = movements.Sum(movement =>
                {
                    if (movement.Type == 'C')
                    {
                        return movement.Value;
                    }
                    else
                    {
                        return -movement.Value;
                    }
                });

                var response = new AccountGetBalanceByIdQueryResponse(account.Number, account.Name, balance);

                return await Task.FromResult(response);
            }
            catch (Exception)
            {
                var response = new AccountGetBalanceByIdQueryResponse(ErrorType.Internal);

                return await Task.FromResult(response);
            }
        }

        private static async Task<AccountGetBalanceByIdQueryResponse> SendMappedErrorResponse(ErrorType type, string errorMessage)
        {
            var response = new AccountGetBalanceByIdQueryResponse(type, errorMessage);

            return await Task.FromResult(response);
        }
    }
}