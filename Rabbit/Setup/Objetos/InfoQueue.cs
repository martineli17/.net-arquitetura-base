using RabbitMQ.Client;

namespace Rabbit.Setup.Objetos
{
    public class InfoQueue
    {
        public string Nome { get; private set; }
        public uint QuantidadeConsumidores { get; private set; }
        public uint QuantidadeMensagens { get; private set; }
        public InfoQueue(QueueDeclareOk queueDeclare)
        {
            Nome = queueDeclare.QueueName;
            QuantidadeConsumidores = queueDeclare.ConsumerCount;
            QuantidadeMensagens = queueDeclare.MessageCount;
        }
        public InfoQueue()
        {

        }
    }
}
