using EstoqueProdutos.DataAccess;
using EstoqueProdutos.Domain.CQRS.Commands;
using EstoqueProdutos.Domain.CQRS.Queries;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, cfg) => cfg.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddScoped<ListarProdutosEstoqueHandler>();
builder.Services.AddScoped<CriarProdutoHandler>();
builder.Services.AddScoped<AtualizarEstoqueHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
