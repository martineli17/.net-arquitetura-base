using Core.Base;
using Crosscuting.Funcoes;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Rabbit.Setup.Objetos
{
    public class ResponseHandler<TResponse> : BaseCommand
    {
        private ReadOnlyMemory<byte> _body;
        public TResponse Dados;
        public string ConsumerTag { get; private set; }
        public ulong DeliveryTag { get; private set; }
        public string Exchange { get; private set; }
        public bool Redelivered { get; private set; }
        public string RoutingKey { get; private set; }
        public ResponseHandler(BasicDeliverEventArgs eventArgs)
        {
            _body = eventArgs.Body;
            ConsumerTag = eventArgs.ConsumerTag;
            DeliveryTag = eventArgs.DeliveryTag;
            Exchange = eventArgs.Exchange;
            Redelivered = eventArgs.Redelivered;
            RoutingKey = eventArgs.RoutingKey;
            Dados = JsonFunc.DeserializeObject<TResponse>(Encoding.UTF8.GetString(_body.ToArray()));
        }

    }
}
