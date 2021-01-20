using System;
using System.Globalization;

namespace QuantitativeWorld.Text.Parsing
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
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