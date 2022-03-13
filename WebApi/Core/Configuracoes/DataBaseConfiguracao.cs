using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositorio.Contexto;

namespace WebApi.Core.Configuracoes;
public static class DataBaseConfiguracao
{
    public static IServiceCollection AddConfiguracaoEntity(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var dataBase = configuration.GetValue<string>("MongoDataBase");
        services.AddScoped(x => new ContextoMongo(connectionString, dataBase));

        return services;
    }
}
