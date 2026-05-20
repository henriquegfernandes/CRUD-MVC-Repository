using EstoqueProdutos.Domain.DTOs;
using EstoqueProdutos.Domain.Entities;

namespace EstoqueProdutos.Domain.Abstractions;

public interface IProdutoEstoqueRepository
{
    Task<IReadOnlyList<ProdutoResumoDto>> ListarAsync(int pagina, int tamanhoPagina, CancellationToken cancellationToken);
    Task<ProdutoEstoque?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
    Task<int> CriarAsync(ProdutoEstoque produto, CancellationToken cancellationToken);
    Task<bool> AtualizarEstoqueAsync(int id, int quantidadeDisponivel, CancellationToken cancellationToken);
}
