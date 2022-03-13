using Core.Attributes;
using Core.Base;
using Crosscuting.Funcoes;
using MediatR;
using Rabbit.Setup.Contratos;
using Rabbit.Setup.Objetos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Setup;

public class RabbitManager : IRabbitManager
{
    private IConnection _connection;
    private IMediatorCustom _mediator;
    public RabbitManager(string connectionString, IMediatorCustom mediator)
    {
        _connection = CriarConexao(connectionString);
        _mediator = mediator;
    }

    public IModel CriarChannel() => _connection.CreateModel();

    public async Task<InfoQueue> CriarQueue(string fila, Dictionary<string, object> args = null, IModel canal = null)
    {
        return await Task.Run(() =>
        {
            var canalEscolhido = canal ?? CriarChannel();
            var queueCriada = new InfoQueue(canalEscolhido.QueueDeclare(queue: fila,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: args));
            if (canal is null)
                canalEscolhido.Dispose();

            return queueCriada;
        });
    }

    public async Task CriarExchange(string name, string type, IModel canal) 
        => await Task.Run(() => canal.ExchangeDeclare(name, type, true, false));

    public async Task CriarBindExchangeQueue(string queue, string exchange, string routerKey, IModel canal)
        => await Task.Run(() => canal.QueueBind(queue, exchange, routerKey));

    public async Task Producer<TRequest>(TRequest request) where TRequest : PublishToQueueAttribute
    {
        await Task.Run(() => 
        {
            var publishAttribute = (PublishToQueueAttribute)Attribute.GetCustomAttribute(typeof(TRequest), typeof(PublishToQueueAttribute));
            using var canalEscolhido = CriarChannel();
            canalEscolhido.BasicPublish(publishAttribute.Exchange, publishAttribute.RoutingKey, null, Encoding.UTF8.GetBytes(JsonFunc.SerializeObject(request)));
        });
    }

    public async Task Consumer<TResponse>(string fila, IModel channel = null) where TResponse : ConsumerFromQueueAttribute
    {
        await Task.Run(() =>
        {
            var canalEscolhido = channel ?? CriarChannel();
            canalEscolhido.BasicQos(0, 5, false);
            var consumer = new EventingBasicConsumer(canalEscolhido);
            consumer.Received += async (sender, events) => await HandlerRecebimento<TResponse>(canalEscolhido, events);
            canalEscolhido.BasicConsume(fila, false, consumer);
        });
    }

    #region Métodos Privados
    private IConnection CriarConexao(string connectionString)
    {
        var factory = new ConnectionFactory { Uri = new Uri(connectionString) };
        return factory.CreateConnection();
    }

    private async Task HandlerRecebimento<TResponse>(IModel canal, BasicDeliverEventArgs events) where TResponse : ConsumerFromQueueAttribute
    {
        try
        {
            await _mediator.EnviarComandoAsync(new ResponseHandler<TResponse>(events));
            canal.BasicAck(events.DeliveryTag, false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            canal.BasicNack(events.DeliveryTag, true, false);
        }
    }
    #endregion
}
