namespace Crosscuting.Extensoes
{
    public static class DoubleExtensions
    {
        public static string ToStringIfContainsValue(this double? value)
        {
            return value.HasValue ? value.ToString() : null;
        }
    }
}
