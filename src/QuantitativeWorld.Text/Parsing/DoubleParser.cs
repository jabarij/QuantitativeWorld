using QuantitativeWorld.DotNetExtensions;
using System;
using System.Globalization;

namespace QuantitativeWorld.Text.Parsing
{
    public class DoubleParser : IParser<double>
    {
        private readonly IFormatProvider _formatProvider;

        public DoubleParser()
            : this(CultureInfo.CurrentCulture) { }
        public DoubleParser(IFormatProvider formatProvider)
        {
            Assert.IsNotNull(formatProvider, nameof(formatProvider));
            _formatProvider = formatProvider;
        }

        public double Parse(string value) =>
            double.Parse(value, _formatProvider);

        public bool TryParse(string value, out double result) =>
            double.TryParse(value, NumberStyles.Any, _formatProvider, out result);
    }
}
