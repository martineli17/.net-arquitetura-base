namespace Core.Attributes;

public class ConsumerFromQueueAttribute : Attribute
{
    public string QueueName { get; set; }
}
