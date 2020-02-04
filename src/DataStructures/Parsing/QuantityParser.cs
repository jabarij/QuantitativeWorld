using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Parsing
{
    public class QuantityParser<TQuantity, TUnit> : IParser<TQuantity>
        where TQuantity : IQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        private readonly IParser<decimal> _valueParser;
        private readonly IParser<TUnit> _unitParser;
        private readonly IQuantityFactory<TQuantity, TUnit> _quantityFactory;

        public QuantityParser(
            IParser<TUnit> unitParser,
            IQuantityFactory<TQuantity, TUnit> quantityFactory)
            : this(
                 valueParser: new DecimalParser(),
                 unitParser: unitParser,
                 quantityFactory: quantityFactory)
        { }
        public QuantityParser(
            IParser<decimal> valueParser,
            IParser<TUnit> unitParser,
            IQuantityFactory<TQuantity, TUnit> quantityFactory)
        {
            Assert.IsNotNull(valueParser, nameof(valueParser));
            Assert.IsNotNull(unitParser, nameof(unitParser));
            Assert.IsNotNull(quantityFactory, nameof(quantityFactory));

            _valueParser = valueParser;
            _unitParser = unitParser;
            _quantityFactory = quantityFactory;
        }

        public TQuantity Parse(string text)
        {
            if (!TryExtractParts(text, out string valueStr, out string unitStr))
                throw Error.IncorrectFormat($"'{text}' is not a correct {typeof(TQuantity)}.");

            var value = _valueParser.Parse(valueStr);
            var unit = _unitParser.Parse(unitStr);
            return _quantityFactory.Create(value, unit);
        }

        public bool TryParse(string text, out TQuantity result)
        {
            if (string.IsNullOrWhiteSpace(text)
                || !TryExtractParts(text, out string valueStr, out string unitStr)
                || !_valueParser.TryParse(valueStr, out var value)
                || !_unitParser.TryParse(unitStr, out var unit))
            {
                result = default(TQuantity);
                return false;
            }

            result = _quantityFactory.Create(value, unit);
            return true;
        }

        protected virtual bool TryExtractParts(string text, out string valueStr, out string unitStr)
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