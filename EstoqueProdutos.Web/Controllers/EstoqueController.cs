using EstoqueProdutos.Domain.CQRS.Commands;
using EstoqueProdutos.Domain.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueProdutos.Web.Controllers;

[ApiController]
[Route("api/estoque")]
public sealed class EstoqueController(
    ListarProdutosEstoqueHandler listarHandler,
    CriarProdutoHandler criarHandler,
    AtualizarEstoqueHandler atualizarHandler) : ControllerBase
{
    [HttpGet("produtos")]
    public async Task<IActionResult> Listar([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 30, CancellationToken cancellationToken = default)
    {
        var produtos = await listarHandler.HandleAsync(new ListarProdutosEstoqueQuery(pagina, tamanhoPagina), cancellationToken);
        return Ok(produtos);
    }

    [HttpPost("produtos")]
    public async Task<IActionResult> Criar([FromBody] CriarProdutoCommand command, CancellationToken cancellationToken)
    {
        var id = await criarHandler.HandleAsync(command, cancellationToken);
        return CreatedAtAction(nameof(Listar), new { id }, new { id });
    }

    [HttpPatch("produtos/{id:int}/quantidade")]
    public async Task<IActionResult> AtualizarQuantidade(int id, [FromBody] int quantidadeDisponivel, CancellationToken cancellationToken)
    {
        var atualizado = await atualizarHandler.HandleAsync(new AtualizarEstoqueCommand(id, quantidadeDisponivel), cancellationToken);
        return atualizado ? NoContent() : NotFound();
    }
}
