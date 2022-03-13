using MediatR;

namespace Core.Base
{
    public interface IBaseRequestHandler<TRequest, TReturn> : IRequestHandler<TRequest, TReturn> where TRequest : IRequest<TReturn>
    {
    }

    public interface IBaseRequestHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
    }
}
