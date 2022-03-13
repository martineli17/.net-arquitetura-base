namespace Crosscuting.Notificacao
{
    public interface INotificador
    {
        void Add(string mensagem, EnumTipoMensagem tipo = EnumTipoMensagem.Info);

        void AddRange(IReadOnlyList<string> mensagens, EnumTipoMensagem tipo);

        void Clear();

        bool Contain();

        IEnumerable<Mensagem> GetMensagens();
    }
}
