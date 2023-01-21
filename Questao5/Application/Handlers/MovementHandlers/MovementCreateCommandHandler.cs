using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests.MovementRequests;
using Questao5.Application.Commands.Responses.MovementResponses;
using Questao5.Application.Notifications;
using Questao5.Application.Notifications.MovementNotifications;
using Questao5.Application.Repositories.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Handlers.MovementHandlers
{
    public class MovementCreateCommandHandler : IRequestHandler<MovementCreateCommandRequest, MovementCreateCommandResponse>
    {
        private readonly IMediator _mediator;
        private readonly IMovementRepository<Movement> _repository;
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<Idempotency> _idempotencyRepository;

        public MovementCreateCommandHandler(IMediator mediator, IMovementRepository<Movement> repository, IRepository<Account> accountRepository, IRepository<Idempotency> idempotencyRepository)
        {
            _mediator = mediator;
            _repository = repository;
            _accountRepository = accountRepository;
            _idempotencyRepository = idempotencyRepository;
        }

        public async Task<MovementCreateCommandResponse> Handle(MovementCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var movement = new Movement(request.AccountId, request.Date, request.Type, request.Value);

            try
            {
                var idempotency = await _idempotencyRepository.Get(request.IdempotencyId);

                MovementCreateCommandResponse? response;

                if (idempotency != null)
                {
                    response = JsonConvert.DeserializeObject<MovementCreateCommandResponse>(idempotency.Response);

                    if (response == null)
                    {
                        throw new Exception();
                    }

                    return await Task.FromResult(response);
                }

                var account = await _accountRepository.Get(request.AccountId);

                if (account == null)
                {
                    response = await SendMappedErrorResponse(ErrorType.InvalidAccount, "Invalid account: account does not exist.", request, cancellationToken);

                    await CreateIdempotency(request.IdempotencyId, request, response); // TODO: adds expiricy time to this idempotency key

                    return response;
                }
                else if (!account.IsActive)
                {
                    response = await SendMappedErrorResponse(ErrorType.InactiveAccount, "Inactive account: account has been deactivated.", request, cancellationToken);

                    await CreateIdempotency(request.IdempotencyId, request, response); // TODO: adds expiricy time to this idempotency key

                    return response;
                }
                else if (request.Value <= 0)
                {
                    response = await SendMappedErrorResponse(ErrorType.InvalidValue, "Invalid value: value must be positive.", request, cancellationToken);

                    await CreateIdempotency(request.IdempotencyId, request, response);

                    return response;
                }
                else if (!((request.Type == 'C') || (request.Type == 'D')))
                {
                    response = await SendMappedErrorResponse(ErrorType.InvalidType, "Invalid type: movement must be of type 'C' or 'D'.", request, cancellationToken);

                    await CreateIdempotency(request.IdempotencyId, request, response);

                    return response;
                }

                response = new MovementCreateCommandResponse(movement.Id);

                await _repository.Add(movement);  // TODO: make this and the next lines a transaction

                await CreateIdempotency(request.IdempotencyId, request, response);

                await _mediator.Publish(new MovementCreateNotification(movement.Id, movement.AccountId, movement.Date, movement.Type, movement.Value, true), cancellationToken);

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                var response = new MovementCreateCommandResponse(ErrorType.Internal, "Something went wrong. Could not create movement.");

                await _mediator.Publish(new MovementCreateNotification(movement.Id, movement.AccountId, movement.Date, movement.Type, movement.Value, false), cancellationToken);

                await _mediator.Publish(new ErrorNotification(ex.Message, ex.StackTrace ?? string.Empty), cancellationToken);

                return await Task.FromResult(response);
            }
        }

        private async Task<MovementCreateCommandResponse> SendMappedErrorResponse(ErrorType type, string errorMessage, MovementCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new MovementCreateCommandResponse(type, errorMessage);

            await _mediator.Publish(new MovementCreateNotification(string.Empty, request.AccountId, request.Date, request.Type, request.Value, false), cancellationToken);

            return await Task.FromResult(response);
        }

        private async Task CreateIdempotency(string id, MovementCreateCommandRequest request, MovementCreateCommandResponse response)
        {
            var requestString = JsonConvert.SerializeObject(request);

            var responseString = JsonConvert.SerializeObject(response);

            var idempotency = new Idempotency(id, requestString, responseString);

            await _idempotencyRepository.Add(idempotency);
        }
    }
}