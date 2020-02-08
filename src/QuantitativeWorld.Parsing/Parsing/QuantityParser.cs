using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System.Linq;

namespace QuantitativeWorld.Text.Parsing
{
    public class QuantityParser<TQuantity, TUnit> : IParser<TQuantity>
        where TQuantity : ILinearQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        private readonly IParser<decimal> _valueParser;
        private readonly IParser<TUnit> _unitParser;
        private readonly ILinearQuantityFactory<TQuantity, TUnit> _quantityFactory;

        public QuantityParser(
            IParser<TUnit> unitParser,
            ILinearQuantityFactory<TQuantity, TUnit> quantityFactory)
            : this(
                 valueParser: new DecimalParser(),
                 unitParser: unitParser,
                 quantityFactory: quantityFactory)
        { }
        public QuantityParser(
            IParser<decimal> valueParser,
            IParser<TUnit> unitParser,
            ILinearQuantityFactory<TQuantity, TUnit> quantityFactory)
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
            if (!TryExtractParts(text, out var valueStr, out var unitStr))
                throw Error.IncorrectFormat(text, GetFormatName());

            if (!_valueParser.TryParse(valueStr, out var value))
                throw Error.IncorrectFormat(valueStr, string.Concat(GetFormatName(), " value"));
            if (!_unitParser.TryParse(unitStr, out var unit))
                throw Error.IncorrectFormat(unitStr, string.Concat(GetFormatName(), " unit"));
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
            if (string.IsNullOrWhiteSpace(text))
            {
                valueStr = null;
                unitStr = null;
                return false;
            }

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

        protected virtual string GetFormatName() =>
            typeof(TQuantity).Name
            .SplitBy(c => char.IsLetter(c) && char.IsUpper(c))
            .Select(StringExtensions.LowercaseFirstLetter)
            .Join(" ");
    }
}