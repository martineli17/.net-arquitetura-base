using MediatR;

namespace Core.Base
{
    public interface IBaseEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : BaseEvent
    {
    }
}
