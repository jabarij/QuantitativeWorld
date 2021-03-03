using Common.Internals.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Parsing
{
#else
namespace QuantitativeWorld.Text.Parsing
{
#endif
    internal class SimpleWeightUnitParser : IParser<WeightUnit>
    {
        private readonly Dictionary<string, WeightUnit> _unitsByNames;
        private readonly Dictionary<string, WeightUnit> _unitsByAbbreviations;

        public SimpleWeightUnitParser()
            : this(GetParsableUnitsByNames(), GetParsableUnitsByAbbreviations()) { }
        private SimpleWeightUnitParser(
            Dictionary<string, WeightUnit> unitsByNames,
            Dictionary<string, WeightUnit> unitsByAbbreviations)
        {
            _unitsByNames = unitsByNames ?? throw new ArgumentNullException(nameof(unitsByNames));
            _unitsByAbbreviations = unitsByAbbreviations ?? throw new ArgumentNullException(nameof(unitsByAbbreviations));
        }

        public WeightUnit Parse(string value)
        {
            Assert.IsNotNullOrWhiteSpace(value, nameof(value));
            var weightUnit =
                ParseByName(value)
                ?? ParseByAbbreviation(value);

            return weightUnit
                ?? throw new FormatException($"'{value}' is not a correct weight unit.");
        }

        public bool TryParse(string value, out WeightUnit result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = default(WeightUnit);
                return false;
            }

            var weightUnit =
                ParseByName(value)
                ?? ParseByAbbreviation(value);

            if (weightUnit != null)
            {
                result = weightUnit.Value;
                return true;
            }
            else
            {
                result = default(WeightUnit);
                return false;
            }
        }

        private WeightUnit? ParseByName(string name) =>
            _unitsByNames.TryGetValue(name, out var unit)
            ? unit
            : (WeightUnit?)null;

        private WeightUnit? ParseByAbbreviation(string abbreviation) =>
            _unitsByAbbreviations.TryGetValue(abbreviation, out var unit)
            ? unit
            : (WeightUnit?)null;

        private static Dictionary<string, WeightUnit> GetParsableUnitsByNames() =>
            WeightUnit
            .GetPredefinedUnits()
            .ToDictionary(e => e.Name);

        private static Dictionary<string, WeightUnit> GetParsableUnitsByAbbreviations() =>
            WeightUnit
            .GetPredefinedUnits()
            .ToDictionary(e => e.Abbreviation);
    }
}