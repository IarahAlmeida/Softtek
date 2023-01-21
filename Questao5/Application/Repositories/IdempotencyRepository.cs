using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Repositories.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Repositories
{
    public class IdempotencyRepository : IRepository<Idempotency>
    {
        private readonly DatabaseConfig _databaseConfig;

        public IdempotencyRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task Add(Idempotency item)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            await connection.ExecuteAsync("INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@Id, @Request, @Response);", item);
        }

        public async Task Delete(string id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            await connection.ExecuteAsync("DELETE FROM idempotencia WHERE chave_idempotencia = @id;", new { id });
        }

        public async Task Edit(Idempotency item)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            await connection.ExecuteAsync("UPDATE idempotencia SET requisicao = @Request, resultado = @Response WHERE chave_idempotencia = @Id;", item);
        }

        public async Task<Idempotency?> Get(string id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            return await connection.QuerySingleOrDefaultAsync<Idempotency>("SELECT chave_idempotencia AS Id, requisicao AS Request, resultado AS Response FROM idempotencia WHERE Id = @id;", new { id });
        }

        public async Task<IEnumerable<Idempotency>> GetAll()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            return await connection.QueryAsync<Idempotency>("SELECT chave_idempotencia AS Id, requisicao AS Request, resultado AS Response FROM idempotencia;");
        }
    }
}