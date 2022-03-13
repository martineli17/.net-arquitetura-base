using Crosscuting.Notificacao;
using Repositorio.Contexto;

namespace Repositorio.Repositorios.Base
{
    public class BaseRepositoryInjector
    {
        internal ContextoMongo Context;
        internal INotificador Notificador;

        public BaseRepositoryInjector(
                    ContextoMongo context,
                    INotificador notificador)
        {
            Context = context;
            Notificador = notificador;
        }
    }
}
