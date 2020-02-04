using System;
using System.Globalization;

namespace QuantitativeWorld.Parsing
{
    public class FormattedDecimalParser : IFormattedParser<decimal>
    {
        public decimal ParseExact(string value, string format, IFormatProvider formatProvider) =>
            decimal.Parse(value, formatProvider);

        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out decimal result)
        {
            if (TryParseNumberStyles(value, format, formatProvider, out var style)
                && decimal.TryParse(value, style, formatProvider, out result))
            {
                return true;
            }

            result = default(decimal);
            return false;
        }

        protected virtual bool TryParseNumberStyles(string value, string format, IFormatProvider formatProvider, out NumberStyles style)
        {
            style = NumberStyles.Any;
            return true;
        }
    }
}