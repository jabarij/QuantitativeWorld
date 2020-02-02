using QuantitativeWorld.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Parsing
{
    internal class FormattedWeightUnitParser : IFormattedParser<WeightUnit>
    {
        private readonly Dictionary<string, WeightUnit> _unitsByNames;
        private readonly Dictionary<string, WeightUnit> _unitsByAbbreviations;
        private readonly Dictionary<string, WeightUnit> _unitsByPluralizedNames;

        public FormattedWeightUnitParser()
            : this(new DictionaryPluralizer()) { }
        public FormattedWeightUnitParser(IPluralizer pluralizer)
            : this(
                  unitsByNames: GetParsableUnitsByNames(),
                  unitsByAbbreviations: GetParsableUnitsByAbbreviations(),
                  unitsByPluralizedNames: GetParsableUnitsByPluralizedNames(pluralizer))
        { }
        private FormattedWeightUnitParser(
            Dictionary<string, WeightUnit> unitsByNames,
            Dictionary<string, WeightUnit> unitsByAbbreviations,
            Dictionary<string, WeightUnit> unitsByPluralizedNames)
        {
            _unitsByNames = unitsByNames ?? throw new ArgumentNullException(nameof(unitsByNames));
            _unitsByAbbreviations = unitsByAbbreviations ?? throw new ArgumentNullException(nameof(unitsByAbbreviations));
            _unitsByPluralizedNames = unitsByPluralizedNames ?? throw new ArgumentNullException(nameof(unitsByPluralizedNames));
        }

        public WeightUnit ParseExact(string value, string format, IFormatProvider formatProvider) =>
            TryGetParser(format, out var parser)
            ? parser(value, format, formatProvider)
            : throw GetUnknownFormatException(format);

        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out WeightUnit result)
        {
            bool isSuccess = TryGetParser(format, out var parser);
            result =
                isSuccess
                ? parser(value, format, formatProvider)
                : default(WeightUnit);
            return isSuccess;
        }

        private bool TryGetParser(string format, out Func<string, string, IFormatProvider, WeightUnit> parser)
        {
            switch (format)
            {
                case "s":
                    parser = ParseFromAbbreviation;
                    return true;
                case "l":
                    parser = ParseFromName;
                    return true;
                case "ll":
                    parser = ParseFromPluralizedName;
                    return true;
                default:
                    parser = GetFormatterForUnknownFormat(format);
                    return false;
            }
        }

        private WeightUnit ParseFromAbbreviation(string value, string format, IFormatProvider formatProvider) =>
            _unitsByAbbreviations.TryGetValue(value, out var weightUnit)
            ? weightUnit
            : throw new InvalidOperationException($"Could not parse weight unit '{value}' using format '{format}'.");
        private WeightUnit ParseFromName(string value, string format, IFormatProvider formatProvider) =>
            _unitsByNames.TryGetValue(value, out var weightUnit)
            ? weightUnit
            : throw new InvalidOperationException($"Could not parse weight unit '{value}' using format '{format}'.");
        private WeightUnit ParseFromPluralizedName(string value, string format, IFormatProvider formatProvider) =>
            _unitsByPluralizedNames.TryGetValue(value, out var weightUnit)
            ? weightUnit
            : throw new InvalidOperationException($"Could not parse weight unit '{value}' using format '{format}'.");

        private Func<string, string, IFormatProvider, WeightUnit> GetFormatterForUnknownFormat(string format) =>
            (_, __, ___) => throw GetUnknownFormatException(format);

        private FormatException GetUnknownFormatException(string format) =>
            new FormatException($"Unknown standard weight unit format '{format}'.");

        private static Dictionary<string, WeightUnit> GetParsableUnitsByNames() =>
            WeightUnit
            .GetParsableUnits()
            .ToDictionary(e => e.Name);

        private static Dictionary<string, WeightUnit> GetParsableUnitsByAbbreviations() =>
            WeightUnit
            .GetParsableUnits()
            .ToDictionary(e => e.Abbreviation);

        private static Dictionary<string, WeightUnit> GetParsableUnitsByPluralizedNames(IPluralizer pluralizer) =>
            WeightUnit
            .GetParsableUnits()
            .ToDictionary(e => pluralizer.Pluralize(e.Name));
    }
}