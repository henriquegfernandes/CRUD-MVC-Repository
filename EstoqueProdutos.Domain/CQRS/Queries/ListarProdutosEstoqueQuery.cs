using EstoqueProdutos.Domain.Abstractions;
using EstoqueProdutos.Domain.DTOs;

namespace EstoqueProdutos.Domain.CQRS.Queries;

public sealed record ListarProdutosEstoqueQuery(int Pagina = 1, int TamanhoPagina = 30);

public sealed class ListarProdutosEstoqueHandler(IProdutoEstoqueRepository repository)
{
    public Task<IReadOnlyList<ProdutoResumoDto>> HandleAsync(ListarProdutosEstoqueQuery query, CancellationToken cancellationToken)
    {
        var pagina = query.Pagina < 1 ? 1 : query.Pagina;
        var tamanho = query.TamanhoPagina switch { < 1 => 30, > 100 => 100, _ => query.TamanhoPagina };
        return repository.ListarAsync(pagina, tamanho, cancellationToken);
    }
}
