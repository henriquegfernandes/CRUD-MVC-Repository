using EstoqueProdutos.DataAccess.Configuration;
using EstoqueProdutos.DataAccess.Persistence;
using EstoqueProdutos.DataAccess.Repositories;
using EstoqueProdutos.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EstoqueProdutos.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IProdutoEstoqueRepository, ProdutoEstoqueRepository>();
        return services;
    }
}
