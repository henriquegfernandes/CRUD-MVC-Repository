namespace EstoqueProdutos.Domain.Entities;

public sealed class ProdutoEstoque
{
    public int Id { get; init; }
    public string Sku { get; init; } = string.Empty;
    public string Nome { get; init; } = string.Empty;
    public int QuantidadeDisponivel { get; init; }
    public decimal Preco { get; init; }
    public int CategoriaId { get; init; }
    public DateTimeOffset AtualizadoEm { get; init; }
}
