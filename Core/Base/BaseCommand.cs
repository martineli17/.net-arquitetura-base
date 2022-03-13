using MediatR;

namespace Core.Base
{
    public abstract class BaseCommand<TReturn> : IRequest<TReturn>
    {
    }

    public abstract class BaseCommand : IRequest
    {
    }
}
