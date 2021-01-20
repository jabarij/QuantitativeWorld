using QuantitativeWorld.DotNetExtensions;
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
