using Plant.QAM.BusinessLogic.PublishedLanguage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Parsing
{
    internal class SimpleLengthUnitParser : IParser<LengthUnit>
    {
        private readonly Dictionary<string, LengthUnit> _unitsByNames;
        private readonly Dictionary<string, LengthUnit> _unitsByAbbreviations;

        public SimpleLengthUnitParser()
            : this(GetParsableUnitsByNames(), GetParsableUnitsByAbbreviations()) { }
        private SimpleLengthUnitParser(
            Dictionary<string, LengthUnit> unitsByNames,
            Dictionary<string, LengthUnit> unitsByAbbreviations)
        {
            _unitsByNames = unitsByNames ?? throw new ArgumentNullException(nameof(unitsByNames));
            _unitsByAbbreviations = unitsByAbbreviations ?? throw new ArgumentNullException(nameof(unitsByAbbreviations));
        }

        public LengthUnit Parse(string value)
        {
            Assert.IsNotNullOrWhiteSpace(value, nameof(value));
            var lengthUnit =
                ParseByName(value)
                ?? ParseByAbbreviation(value);

            return lengthUnit
                ?? throw new FormatException($"'{value}' is not a correct length unit.");
        }

        public bool TryParse(string value, out LengthUnit result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = default(LengthUnit);
                return false;
            }

            var lengthUnit =
                ParseByName(value)
                ?? ParseByAbbreviation(value);

            if (lengthUnit != null)
            {
                result = lengthUnit.Value;
                return true;
            }
            else
            {
                result = default(LengthUnit);
                return false;
            }
        }

        private LengthUnit? ParseByName(string name) =>
            _unitsByNames.TryGetValue(name, out var unit)
            ? unit
            : (LengthUnit?)null;

        private LengthUnit? ParseByAbbreviation(string abbreviation) =>
            _unitsByAbbreviations.TryGetValue(abbreviation, out var unit)
            ? unit
            : (LengthUnit?)null;

        private static Dictionary<string, LengthUnit> GetParsableUnitsByNames() =>
            LengthUnit
            .GetParsableUnits()
            .ToDictionary(e => e.Name);

        private static Dictionary<string, LengthUnit> GetParsableUnitsByAbbreviations() =>
            LengthUnit
            .GetParsableUnits()
            .ToDictionary(e => e.Abbreviation);
    }
}