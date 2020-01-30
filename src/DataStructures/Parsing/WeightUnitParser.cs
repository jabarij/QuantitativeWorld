using System;

namespace DataStructures.Parsing
{
    public class WeightUnitParser : IParser<WeightUnit>, IFormattedParser<WeightUnit>
    {
        private readonly SimpleWeightUnitParser _parser = new SimpleWeightUnitParser();
        private readonly FormattedWeightUnitParser _formattedParser = new FormattedWeightUnitParser();

        public WeightUnit Parse(string value) =>
            _parser.Parse(value);

        public bool TryParse(string value, out WeightUnit result) =>
            _parser.TryParse(value, out result);

        public WeightUnit ParseExact(string value, string format, IFormatProvider formatProvider) =>
            _formattedParser.ParseExact(value, format, formatProvider);

        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out WeightUnit result) =>
            _formattedParser.TryParseExact(value, format, formatProvider, out result);
    }
}
