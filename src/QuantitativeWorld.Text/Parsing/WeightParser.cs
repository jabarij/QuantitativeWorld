using Common.Internals.DotNetExtensions;
using System;
using System.Globalization;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Parsing
{
#else
namespace QuantitativeWorld.Text.Parsing
{
#endif
    public class WeightParser : IParser<Weight>, IFormattedParser<Weight>
    {
        private readonly IParser<Weight> _parser;
        private readonly IFormattedParser<Weight> _formattedParser;

        public WeightParser()
            : this(CultureInfo.CurrentCulture) { }
        public WeightParser(IFormatProvider decimalFormatProvider)
            : this(
                 parser: new QuantityParser<Weight, WeightUnit>(
                     valueParser: new DoubleParser(decimalFormatProvider),
                     unitParser: new WeightUnitParser(),
                     quantityFactory: new WeightFactory()),
                 formattedParser: new FormattedQuantityParser<Weight, WeightUnit>(
                     valueParser: new FormattedDoubleParser(),
                     unitParser: new FormattedWeightUnitParser(),
                     quantityFactory: new WeightFactory()))
        { }
        public WeightParser(IParser<Weight> parser, IFormattedParser<Weight> formattedParser)
        {
            Assert.IsNotNull(parser, nameof(parser));
            Assert.IsNotNull(formattedParser, nameof(formattedParser));

            _parser = parser;
            _formattedParser = formattedParser;
        }

        public Weight Parse(string value) =>
            _parser.Parse(value);

        public bool TryParse(string value, out Weight result) =>
            _parser.TryParse(value, out result);

        public Weight ParseExact(string value, string format, IFormatProvider formatProvider) =>
            _formattedParser.ParseExact(value, format, formatProvider);

        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out Weight result) =>
            _formattedParser.TryParseExact(value, format, formatProvider, out result);
    }
}
