using FluentValidation;

namespace Dominio.Entidades
{
    public abstract class Entity
    {
        private List<string> _errosValidacao;
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public bool IsValido { get; private set; }
        public IReadOnlyList<string> ErrosValidacao { get => _errosValidacao; }

        public Entity()
        {
            DataCriacao = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public abstract void Validar();

        protected void Validar<TObject>(AbstractValidator<TObject> validator, TObject dados) where TObject : Entity
        {
            _errosValidacao.Clear();
            if (dados is null)
            {
                _errosValidacao.Add($"Dados inválidos para {GetType().Name}.");
                IsValido = false;
            }
            var erros = new List<string>();
            var validacao = validator.Validate(dados);
            if (!validacao.IsValid)
                erros.AddRange(validacao.Errors.Select(x => x.ErrorMessage).ToList());
            _errosValidacao = erros;
            IsValido = validacao.IsValid;
        }
    }
}
