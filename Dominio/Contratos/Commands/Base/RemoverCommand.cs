using Core.Base;

namespace Dominio.Contratos.Commands.Base
{
    public abstract class RemoverCommand : BaseCommand<bool>
    {
        public Guid Id { get; init; }
    }
}
