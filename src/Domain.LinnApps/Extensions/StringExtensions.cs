namespace Linn.Production.Domain.LinnApps.Extensions
{
    public static class StringExtensions
    {
        public static decimal? ParseDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (decimal.TryParse(value, out var decimalValue))
            {
                return decimalValue;
            }

            return null;
        }
    }
}