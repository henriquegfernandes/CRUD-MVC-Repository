using EstoqueProdutos.Domain.Abstractions;

namespace EstoqueProdutos.Domain.CQRS.Commands;

public sealed record AtualizarEstoqueCommand(int ProdutoId, int QuantidadeDisponivel);

public sealed class AtualizarEstoqueHandler(IProdutoEstoqueRepository repository)
{
    public Task<bool> HandleAsync(AtualizarEstoqueCommand command, CancellationToken cancellationToken) =>
        repository.AtualizarEstoqueAsync(command.ProdutoId, command.QuantidadeDisponivel, cancellationToken);
}
