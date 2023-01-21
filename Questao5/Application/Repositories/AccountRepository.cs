using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Repositories.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        private readonly DatabaseConfig _databaseConfig;

        public AccountRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task Add(Account item)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            await connection.ExecuteAsync("INSERT INTO contacorrente (idcontacorrente, numero, nome, ativo) VALUES (@Id, @Number, @Name, @IsActive);", item);
        }

        public async Task Delete(string id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            await connection.ExecuteAsync("DELETE FROM contacorrente WHERE idcontacorrente = @id;", new { id });
        }

        public async Task Edit(Account item)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            await connection.ExecuteAsync("UPDATE contacorrente SET numero = @Number, nome = @Name, ativo = @IsActive WHERE idcontacorrente = @Id;", item);
        }

        public async Task<Account?> Get(string id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            return await connection.QuerySingleOrDefaultAsync<Account?>("SELECT idcontacorrente AS Id, numero AS Number, nome AS Name, ativo AS IsActive FROM contacorrente WHERE Id = @id;", new { id });
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            return await connection.QueryAsync<Account>("SELECT idcontacorrente AS Id, numero AS Number, nome AS Name, ativo AS IsActive FROM contacorrente;");
        }
    }
}