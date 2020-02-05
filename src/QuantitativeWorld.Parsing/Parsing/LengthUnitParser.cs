using System;

namespace QuantitativeWorld.Text.Parsing
{
    public class LengthUnitParser : IParser<LengthUnit>, IFormattedParser<LengthUnit>
    {
        private readonly SimpleLengthUnitParser _parser = new SimpleLengthUnitParser();
        private readonly FormattedLengthUnitParser _formattedParser = new FormattedLengthUnitParser();

        public LengthUnit Parse(string value) =>
            _parser.Parse(value);

        public bool TryParse(string value, out LengthUnit result) =>
            _parser.TryParse(value, out result);

        public LengthUnit ParseExact(string value, string format, IFormatProvider formatProvider) =>
            _formattedParser.ParseExact(value, format, formatProvider);

        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out LengthUnit result) =>
            _formattedParser.TryParseExact(value, format, formatProvider, out result);
    }
}
