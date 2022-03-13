using Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Rabbit.Setup.Contratos;

namespace WebApi.Core.Configuracoes;

public static class RabbitConfiguracao
{
    public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services) where TConsumer : ConsumerFromQueueAttribute
    {
        var consumerAttribute = (ConsumerFromQueueAttribute)Attribute.GetCustomAttribute(typeof(TConsumer), typeof(ConsumerFromQueueAttribute));
        var rabbitService = (IRabbitManager)services.BuildServiceProvider().GetRequiredService(typeof(IRabbitManager));

        rabbitService.CriarQueue(consumerAttribute.QueueName).Wait();
        rabbitService.Consumer<TConsumer>(consumerAttribute.QueueName).Wait();

        return services;
    }

    public static IServiceCollection AddExchangesTypes(this IServiceCollection services)
    {
        var rabbitService = (IRabbitManager)services.BuildServiceProvider().GetRequiredService(typeof(IRabbitManager));
        var exchangesTypes = Enum.GetValues<PublishToQueueAttribute.Type>();
        var channel = rabbitService.CriarChannel();

        foreach (var exchange in exchangesTypes)
            rabbitService.CriarExchange(exchange.ToString(), exchange.ToString(), channel);

        channel.Dispose();

        return services;
    }
}
