namespace EstoqueProdutos.Domain.DTOs;

public sealed record ProdutoResumoDto(int Id, string Sku, string Nome, int QuantidadeDisponivel, decimal Preco);
