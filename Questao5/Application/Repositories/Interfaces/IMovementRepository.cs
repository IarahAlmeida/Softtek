namespace Questao5.Application.Repositories.Interfaces
{
    public interface IMovementRepository<T> : IRepository<T>
    {
        Task<IEnumerable<T>> GetAllByAccountId(string id);
    }
}