using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Core.Configuracoes
{
    public static class JsonConfiguracao
    {
        public static IServiceCollection AddJsonCofig(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.UseCamelCasing(true);
            });
            return services;
        }
    }
}
