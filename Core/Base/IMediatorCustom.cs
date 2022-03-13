namespace Core.Base
{
    public interface IMediatorCustom
    {
        Task<TReturn> EnviarComandoAsync<TReturn>(BaseCommand<TReturn> command);
        Task EnviarComandoAsync(BaseCommand command);
        Task PublicarEventoAsync(BaseEvent evento);
    }
}
