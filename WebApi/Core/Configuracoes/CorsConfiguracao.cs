using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Core.Configuracoes
{
    public static class CorsConfiguracao
    {
        public static IServiceCollection AddCorsCustom(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsOptions", x =>
                                                x.AllowAnyHeader()
                                                .AllowAnyMethod()
                                                .AllowCredentials()));
            return services;
        }
    }
}
