using System;
using System.Globalization;

namespace QuantitativeWorld.Text.Parsing
{
    public class FormattedDoubleParser : IFormattedParser<double>
    {
        public double ParseExact(string value, string format, IFormatProvider formatProvider) =>
            double.Parse(value, formatProvider);

        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out double result)
        {
            if (TryParseNumberStyles(value, format, formatProvider, out var style)
                && double.TryParse(value, style, formatProvider, out result))
            {
                return true;
            }

            result = default(double);
            return false;
        }

        protected virtual bool TryParseNumberStyles(string value, string format, IFormatProvider formatProvider, out NumberStyles style)
        {
            style = NumberStyles.Any;
            return true;
        }
    }
}