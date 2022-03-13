using Core.Attributes;

namespace Core.Interfaces;
public interface IMensageria
{
    Task Producer<TRequest>(TRequest request) where TRequest : PublishToQueueAttribute;
}

