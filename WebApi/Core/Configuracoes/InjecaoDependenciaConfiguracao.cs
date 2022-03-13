using Core.Base;
using Core.Interfaces;
using Crosscuting.Notificacao;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rabbit.Setup;
using Rabbit.Setup.Contratos;
using Repositorio.Repositorios.Base;

namespace WebApi.Core.Configuracoes
{
    public static class InjecaoDependenciaConfiguracao
    {
        public static IServiceCollection AddInjecaoDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            #region Crosscuting
            services.AddScoped<INotificador, Notificador>();
            #endregion

            #region Repositorio
            services.AddScoped<BaseRepositoryInjector>();
            #endregion

            #region Core
            services.AddScoped<IMediatorCustom, MediatorCustom>();
            #endregion

            #region MediatR
            services.AddAutoMapper(typeof(InjecaoDependenciaConfiguracao).Assembly);
            services.AddMediatR(typeof(BaseRepositoryInjector).Assembly);
            #endregion

            #region Integracao
            var mediator = services.BuildServiceProvider().GetRequiredService<IMediatorCustom>();

            services.AddSingleton<IRabbitManager, RabbitManager>(x => new RabbitManager(configuration.GetConnectionString("RabbitMq"), mediator));
            services.AddSingleton<IMensageria, RabbitManager>(x => new RabbitManager(configuration.GetConnectionString("RabbitMq"), mediator));
            #endregion

            return services;
        }
    }
}
