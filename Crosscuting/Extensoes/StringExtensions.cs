namespace Crosscuting.Extensoes
{
    public static class StringExtensions
    {
        public static bool FullNumber(this string value)
        {
            if (value is null) return false;
            foreach (var item in value) if (!char.IsDigit(item)) return false;
            return true;
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
