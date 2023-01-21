namespace Questao5.Application.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task Add(T item);

        Task Delete(string id);

        Task Edit(T item);

        Task<T?> Get(string id);

        Task<IEnumerable<T>> GetAll();
    }
}