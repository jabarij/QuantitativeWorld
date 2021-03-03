using System;
using System.Globalization;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Parsing
{
    using number = Decimal;
#else
namespace QuantitativeWorld.Text.Parsing
{
    using number = Double;
#endif

    public class FormattedDoubleParser : IFormattedParser<number>
    {
        public number ParseExact(string value, string format, IFormatProvider formatProvider) =>
            number.Parse(value, formatProvider);

        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out number result)
        {
            if (TryParseNumberStyles(value, format, formatProvider, out var style)
                && number.TryParse(value, style, formatProvider, out result))
            {
                return true;
            }

            result = default(number);
            return false;
        }

        protected virtual bool TryParseNumberStyles(string value, string format, IFormatProvider formatProvider, out NumberStyles style)
        {
            style = NumberStyles.Any;
            return true;
        }
    }
}