using Common.Internals.DotNetExtensions;
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

    public class DoubleParser : IParser<number>
    {
        private readonly IFormatProvider _formatProvider;

        public DoubleParser()
            : this(CultureInfo.CurrentCulture) { }
        public DoubleParser(IFormatProvider formatProvider)
        {
            Assert.IsNotNull(formatProvider, nameof(formatProvider));
            _formatProvider = formatProvider;
        }

        public number Parse(string value) =>
            number.Parse(value, _formatProvider);

        public bool TryParse(string value, out number result) =>
            number.TryParse(value, NumberStyles.Any, _formatProvider, out result);
    }
}
