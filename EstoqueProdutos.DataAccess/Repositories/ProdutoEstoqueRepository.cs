using Dapper;
using EstoqueProdutos.DataAccess.Persistence;
using EstoqueProdutos.Domain.Abstractions;
using EstoqueProdutos.Domain.DTOs;
using EstoqueProdutos.Domain.Entities;

namespace EstoqueProdutos.DataAccess.Repositories;

public sealed class ProdutoEstoqueRepository(ISqlConnectionFactory connectionFactory) : IProdutoEstoqueRepository
{
    public async Task<IReadOnlyList<ProdutoResumoDto>> ListarAsync(int pagina, int tamanhoPagina, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT Id, Sku, Nome, QuantidadeDisponivel, Preco
            FROM ProdutoEstoque
            ORDER BY Id DESC
            OFFSET @Offset ROWS FETCH NEXT @Take ROWS ONLY;
            """;

        using var connection = connectionFactory.CreateConnection();
        var produtos = await connection.QueryAsync<ProdutoResumoDto>(new CommandDefinition(
            sql,
            new { Offset = (pagina - 1) * tamanhoPagina, Take = tamanhoPagina },
            cancellationToken: cancellationToken));

        return produtos.AsList();
    }

    public async Task<ProdutoEstoque?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT TOP 1 * FROM ProdutoEstoque WHERE Id = @Id";
        using var connection = connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<ProdutoEstoque>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }

    public async Task<int> CriarAsync(ProdutoEstoque produto, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO ProdutoEstoque (Sku, Nome, QuantidadeDisponivel, Preco, CategoriaId, AtualizadoEm)
            OUTPUT INSERTED.Id
            VALUES (@Sku, @Nome, @QuantidadeDisponivel, @Preco, @CategoriaId, @AtualizadoEm);
            """;

        using var connection = connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(new CommandDefinition(sql, produto, cancellationToken: cancellationToken));
    }

    public async Task<bool> AtualizarEstoqueAsync(int id, int quantidadeDisponivel, CancellationToken cancellationToken)
    {
        const string sql = """
            UPDATE ProdutoEstoque
            SET QuantidadeDisponivel = @QuantidadeDisponivel,
                AtualizadoEm = SYSUTCDATETIME()
            WHERE Id = @Id;
            """;

        using var connection = connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = id, QuantidadeDisponivel = quantidadeDisponivel }, cancellationToken: cancellationToken));
        return rows > 0;
    }
}
