using MediatR;

namespace Core.Base
{
    public class MediatorCustom : IMediatorCustom
    {
        private readonly IMediator _mediator;
        public MediatorCustom(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TReturn> EnviarComandoAsync<TReturn>(BaseCommand<TReturn> command)
        {
            return await _mediator.Send(command);
        }

        public async Task EnviarComandoAsync(BaseCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task PublicarEventoAsync(BaseEvent evento)
        {
            await _mediator.Publish(evento);
        }
    }
}
