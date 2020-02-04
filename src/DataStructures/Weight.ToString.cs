using QuantitativeWorld.Formatting;
using QuantitativeWorld.Formatting.Interfaces;
using System;

namespace QuantitativeWorld
{
    partial struct Weight : IFormattableAsFuck
    {
        public override string ToString() =>
            ToString(format: null);
        public string ToString(string format)
        {
            var formatter = new WeightFormatter();
            return ToString(format, formatter, formatter);
        }
        public string ToString(IFormatProvider formatProvider) =>
            ToString(null, formatProvider);
        public string ToString(string format, IFormatProvider formatProvider) =>
            ToString(
                format: format,
                formatter:
                    formatProvider?.GetFormat<ICustomFormatter<Weight>>()
                    ?? formatProvider?.GetFormat<ICustomFormatter>()
                    ?? new WeightFormatter(),
                formatProvider: formatProvider);
        private string ToString(string format, ICustomFormatter formatter, IFormatProvider formatProvider) =>
            formatter.Format(format, this, formatProvider);
    }
}
