using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Repositories.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Repositories
{
    public class MovementRepository : IMovementRepository<Movement>
    {
        private readonly DatabaseConfig _databaseConfig;

        public MovementRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task Add(Movement item)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            await connection.ExecuteAsync("INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@Id, @AccountId, @Date, @Type, @Value);", item);
        }

        public async Task Delete(string id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            await connection.ExecuteAsync("DELETE FROM movimento WHERE idmovimento = @Id;", id);
        }

        public async Task Edit(Movement item)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            await connection.ExecuteAsync("UPDATE movimento SET datamovimento = @Date, tipomovimento = @Type, valor = @Value WHERE idmovimento = @Id AND idcontacorrente = @AccountId;", item);
        }

        public async Task<Movement?> Get(string id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            return await connection.QuerySingleOrDefaultAsync<Movement>("SELECT idmovimento AS Id, idcontacorrente AS AccountId, datamovimento AS Date, tipomovimento AS Type, valor AS Value FROM movimento WHERE Id = @id;", new { id });
        }

        public async Task<IEnumerable<Movement>> GetAll()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            return await connection.QueryAsync<Movement>("SELECT idmovimento AS Id, idcontacorrente AS AccountId, datamovimento AS Date, tipomovimento AS Type, valor AS Value FROM movimento;");
        }

        public async Task<IEnumerable<Movement>> GetAllByAccountId(string id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            return await connection.QueryAsync<Movement>("SELECT idmovimento AS Id, idcontacorrente AS AccountId, datamovimento AS Date, tipomovimento AS Type, valor AS Value FROM movimento WHERE idcontacorrente = @id;", new { id });
        }
    }
}