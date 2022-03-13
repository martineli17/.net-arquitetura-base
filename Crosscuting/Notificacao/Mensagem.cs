namespace Crosscuting.Notificacao
{
    public class Mensagem
    {
        public string Texto { get; private set; }
        public EnumTipoMensagem Tipo { get; private set; }
        public string TipoDescricao { get; private set; }

        public Mensagem(string texto, EnumTipoMensagem tipo = EnumTipoMensagem.Info)
        {
            Texto = texto;
            Tipo = tipo;
            TipoDescricao = tipo.ToString();
        }
    }
}
