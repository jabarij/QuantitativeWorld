using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Text.Parsing
{
    public class FormattedQuantityParser<TQuantity, TUnit> : IFormattedParser<TQuantity>
        where TQuantity : ILinearQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        private readonly IFormattedParser<double> _valueParser;
        private readonly IFormattedParser<TUnit> _unitParser;
        private readonly ILinearQuantityFactory<TQuantity, TUnit> _quantityFactory;

        public FormattedQuantityParser(
            IFormattedParser<TUnit> unitParser,
            ILinearQuantityFactory<TQuantity, TUnit> quantityFactory)
            : this(
                 valueParser: new FormattedDoubleParser(),
                 unitParser: unitParser,
                 quantityFactory: quantityFactory)
        { }
        public FormattedQuantityParser(
            IFormattedParser<double> valueParser,
            IFormattedParser<TUnit> unitParser,
            ILinearQuantityFactory<TQuantity, TUnit> quantityFactory)
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
            if (!TryExtractParts(text, format, formatProvider, out string valueStr, out string valueFormat, out string unitStr, out string unitFormat))
                throw Error.IncorrectFormat($"'{text}' is not a correct {typeof(TQuantity)}.");

            var value = _valueParser.ParseExact(valueStr, valueFormat, formatProvider);
            var unit = _unitParser.ParseExact(unitStr, unitFormat, formatProvider);
            return _quantityFactory.Create(value, unit);
        }

        public bool TryParseExact(string text, string format, IFormatProvider formatProvider, out TQuantity result)
        {
            if (string.IsNullOrWhiteSpace(text)
                || !TryExtractParts(text, format, formatProvider, out string valueStr, out string valueFormat, out string unitStr, out string unitFormat)
                || !_valueParser.TryParseExact(valueStr, valueFormat, formatProvider, out var value)
                || !_unitParser.TryParseExact(unitStr, unitFormat, formatProvider, out var unit))
            {
                result = default(TQuantity);
                return false;
            }

            result = _quantityFactory.Create(value, unit);
            return true;
        }

        protected virtual bool TryExtractParts(string text, string format, IFormatProvider formatProvider, out string valueStr, out string valueFormat, out string unitStr, out string unitFormat)
        {
            Assert.IsNotNullOrWhiteSpace(text, nameof(text));
            int firstWhiteSpaceIndex = text.IndexOf(char.IsWhiteSpace);
            if (firstWhiteSpaceIndex == -1)
            {
                valueStr = null;
                valueFormat = null;
                unitStr = null;
                unitFormat = null;
                return false;
            }

            (valueStr, unitStr) = text.SplitAt(firstWhiteSpaceIndex);
            valueFormat = "G29";
            unitStr = unitStr.TrimStart();
            unitFormat = format;
            return true;
        }
    }
}
