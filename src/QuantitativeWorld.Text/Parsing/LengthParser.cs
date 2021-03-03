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

    public class LengthParser : IParser<Length>, IFormattedParser<Length>
    {
        private readonly IParser<Length> _parser;
        private readonly IFormattedParser<Length> _formattedParser;

        public LengthParser()
            : this(CultureInfo.CurrentCulture) { }
        public LengthParser(CultureInfo culture)
            : this(
                 parser: new QuantityParser<Length, LengthUnit>(
                     valueParser: new DoubleParser(culture),
                     unitParser: new LengthUnitParser(),
                     quantityFactory: new LengthFactory()),
                 formattedParser: new FormattedQuantityParser<Length, LengthUnit>(
                     valueParser: new FormattedDoubleParser(),
                     unitParser: new FormattedLengthUnitParser(),
                     quantityFactory: new LengthFactory()))
        { }
        public LengthParser(IParser<Length> parser, IFormattedParser<Length> formattedParser)
        {
            Assert.IsNotNull(parser, nameof(parser));
            Assert.IsNotNull(formattedParser, nameof(formattedParser));

            _parser = parser;
            _formattedParser = formattedParser;
        }

        public Length Parse(string value) =>
            _parser.Parse(value);

        public bool TryParse(string value, out Length result) =>
            _parser.TryParse(value, out result);

        public Length ParseExact(string value, string format, IFormatProvider formatProvider) =>
            _formattedParser.ParseExact(value, format, formatProvider);

        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out Length result) =>
            _formattedParser.TryParseExact(value, format, formatProvider, out result);
    }
}
