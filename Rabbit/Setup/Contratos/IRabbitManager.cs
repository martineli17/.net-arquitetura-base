using Core.Attributes;
using Core.Interfaces;
using Rabbit.Setup.Objetos;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rabbit.Setup.Contratos;

public interface IRabbitManager : IMensageria
{
    IModel CriarChannel();
    Task<InfoQueue> CriarQueue(string fila, Dictionary<string, object> args = null, IModel canal = null);
    Task Consumer<TResponse>(string fila, IModel channel = null) where TResponse : ConsumerFromQueueAttribute;
    Task CriarBindExchangeQueue(string queue, string exchange, string routerKey, IModel canal);
    Task CriarExchange(string name, string type, IModel canal);
}
