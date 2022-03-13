namespace Core.Objetos
{
    public static class MensagensValidador
    {
        public const string NotNullGeneric = "{PropertyName} precisa ser informado.";
        public const string EmailInvalid = "{PropertyName} não está com e-mail válido.";
        public const string LengthInvalid = "{PropertyName} contém mais caracteres que o permitodo ({MaxLength}).";
        public const string MaxLengthInvalid = "{PropertyName} contém mais caracteres que o permitodo ({MaxLength}).";
        public const string MinLengthInvalid = "{PropertyName} contém menos caracteres que o permitodo ({MinLength}).";
        public const string GreaterThanOrEqualTo = "{PropertyName} precisa ser maior ou igual a {ComparisonValue}.";
        public const string GreaterThan = "{PropertyName} precisa ser maior que {ComparisonValue}.";
        public const string DontNumber = "{PropertyName} precisa conter somente números.";
        public const string NotFound = "Registro não encontrado.";
    }
}
