using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Parsing
{
    public class FormattedQuantityParser<TQuantity, TUnit> : IFormattedParser<TQuantity>
        where TQuantity : IQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        private readonly IFormattedParser<decimal> _valueParser;
        private readonly IFormattedParser<TUnit> _unitParser;
        private readonly IQuantityFactory<TQuantity, TUnit> _quantityFactory;

        public FormattedQuantityParser(
            IFormattedParser<TUnit> unitParser,
            IQuantityFactory<TQuantity, TUnit> quantityFactory)
            : this(
                 valueParser: new FormattedDecimalParser(),
                 unitParser: unitParser,
                 quantityFactory: quantityFactory)
        { }
        public FormattedQuantityParser(
            IFormattedParser<decimal> valueParser,
            IFormattedParser<TUnit> unitParser,
            IQuantityFactory<TQuantity, TUnit> quantityFactory)
        {
            Assert.IsNotNull(valueParser, nameof(valueParser));
            Assert.IsNotNull(unitParser, nameof(unitParser));
            Assert.IsNotNull(quantityFactory, nameof(quantityFactory));

            _valueParser = valueParser;
            _unitParser = unitParser;
            _quantityFactory = quantityFactory;
        }

        public TQuantity ParseExact(string text, string format, IFormatProvider formatProvider)
        {
            if (!TryExtractParts(text, format, formatProvider, out string valueStr, out string unitStr))
                throw Error.IncorrectFormat($"'{text}' is not a correct {typeof(TQuantity)}.");

            var value = _valueParser.ParseExact(valueStr, format, formatProvider);
            var unit = _unitParser.ParseExact(unitStr, format, formatProvider);
            return _quantityFactory.Create(value, unit);
        }

        public bool TryParseExact(string text, string format, IFormatProvider formatProvider, out TQuantity result)
        {
            if (string.IsNullOrWhiteSpace(text)
                || !TryExtractParts(text, format, formatProvider, out string valueStr, out string unitStr)
                || !_valueParser.TryParseExact(valueStr, format, formatProvider, out var value)
                || !_unitParser.TryParseExact(unitStr, format, formatProvider, out var unit))
            {
                result = default(TQuantity);
                return false;
            }

            result = _quantityFactory.Create(value, unit);
            return true;
        }

        protected virtual bool TryExtractParts(string text, string format, IFormatProvider formatProvider, out string valueStr, out string unitStr)
        {
            Assert.IsNotNullOrWhiteSpace(text, nameof(text));
            int firstWhiteSpaceIndex = text.IndexOf(char.IsWhiteSpace);
            if (firstWhiteSpaceIndex == -1)
            {
                valueStr = null;
                unitStr = null;
                return false;
            }

            (valueStr, unitStr) = text.SplitAt(firstWhiteSpaceIndex);
            unitStr = unitStr.TrimStart();
            return true;
        }
    }
}
