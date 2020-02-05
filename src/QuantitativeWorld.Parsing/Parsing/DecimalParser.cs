using QuantitativeWorld.DotNetExtensions;
using System;
using System.Globalization;

namespace QuantitativeWorld.Text.Parsing
{
    public class DecimalParser : IParser<decimal>
    {
        private readonly IFormatProvider _formatProvider;

        public DecimalParser()
            : this(CultureInfo.CurrentCulture) { }
        public DecimalParser(IFormatProvider formatProvider)
        {
            Assert.IsNotNull(formatProvider, nameof(formatProvider));
            _formatProvider = formatProvider;
        }

        public decimal Parse(string value) =>
            decimal.Parse(value, _formatProvider);

        public bool TryParse(string value, out decimal result) =>
            decimal.TryParse(value, NumberStyles.Any, _formatProvider, out result);
    }
}
