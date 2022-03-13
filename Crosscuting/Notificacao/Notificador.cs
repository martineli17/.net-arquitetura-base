namespace Crosscuting.Notificacao
{
    public class Notificador : INotificador
    {
        private IList<Mensagem> _mensagemList = new List<Mensagem>();
        public void Add(string mensagem, EnumTipoMensagem tipo = EnumTipoMensagem.Info)
        {
            _mensagemList.Add(new Mensagem(mensagem, tipo));
        }

        public void AddRange(IReadOnlyList<string> mensagens, EnumTipoMensagem tipo)
        {
            mensagens.ToList().ForEach(x => _mensagemList.Add(new Mensagem(x, tipo)));
        }

        public void Clear()
        {
            _mensagemList.Clear();
        }

        public bool Contain() => _mensagemList.Any();

        public IEnumerable<Mensagem> GetMensagens() => _mensagemList;
    }
}
