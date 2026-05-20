using EstoqueProdutos.Domain.Abstractions;
using EstoqueProdutos.Domain.Entities;

namespace EstoqueProdutos.Domain.CQRS.Commands;

public sealed record CriarProdutoCommand(string Sku, string Nome, int QuantidadeDisponivel, decimal Preco, int CategoriaId);

public sealed class CriarProdutoHandler(IProdutoEstoqueRepository repository)
{
    public Task<int> HandleAsync(CriarProdutoCommand command, CancellationToken cancellationToken)
    {
        var entity = new ProdutoEstoque
        {
            Sku = command.Sku.Trim(),
            Nome = command.Nome.Trim(),
            QuantidadeDisponivel = command.QuantidadeDisponivel,
            Preco = command.Preco,
            CategoriaId = command.CategoriaId,
            AtualizadoEm = DateTimeOffset.UtcNow
        };

        return repository.CriarAsync(entity, cancellationToken);
    }
}
