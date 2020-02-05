using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Text.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Text.Parsing
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
            GetParser(format, out var _)?
            .Invoke(value, format, formatProvider)
            ?? throw GetUnknownFormatException(format);

        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out WeightUnit result)
        {
            var parser = GetParser(format, out bool isKnownFormat);
            result =
                isKnownFormat
                ? parser(value, format, formatProvider)
                : default(WeightUnit);
            return isKnownFormat;
        }

        private Func<string, string, IFormatProvider, WeightUnit> GetParser(string format, out bool isKnownFormat)
        {
            switch (format)
            {
                case "s":
                    isKnownFormat = true;
                    return ParseFromAbbreviation;
                case "l":
                    isKnownFormat = true;
                    return ParseFromName;
                case "ll":
                    isKnownFormat = true;
                    return ParseFromPluralizedName;
                default:
                    isKnownFormat = false;
                    return GetFormatterForUnknownFormat;
            }
        }

        private WeightUnit ParseFromAbbreviation(string value, string format, IFormatProvider formatProvider) =>
            _unitsByAbbreviations.TryGetValue(value, out var weightUnit)
            ? weightUnit
            : throw Error.IncorrectFormat($"'{value}' is not a correct abbreviation of weight unit.");
        private WeightUnit ParseFromName(string value, string format, IFormatProvider formatProvider) =>
            _unitsByNames.TryGetValue(value, out var weightUnit)
            ? weightUnit
            : throw Error.IncorrectFormat($"'{value}' is not a correct name of weight unit.");
        private WeightUnit ParseFromPluralizedName(string value, string format, IFormatProvider formatProvider) =>
            _unitsByPluralizedNames.TryGetValue(value, out var weightUnit)
            ? weightUnit
            : throw Error.IncorrectFormat($"'{value}' is not a correct pluralized name of weight unit.");

        private WeightUnit GetFormatterForUnknownFormat(string value, string format, IFormatProvider formatProvider) =>
            throw GetUnknownFormatException(format);

        private FormatException GetUnknownFormatException(string format) =>
            new FormatException($"'{format}' is not a correct format of weight unit.");

        private static Dictionary<string, WeightUnit> GetParsableUnitsByNames() =>
            WeightUnit
            .GetPredefinedUnits()
            .ToDictionary(e => e.Name);

        private static Dictionary<string, WeightUnit> GetParsableUnitsByAbbreviations() =>
            WeightUnit
            .GetPredefinedUnits()
            .ToDictionary(e => e.Abbreviation);

        private static Dictionary<string, WeightUnit> GetParsableUnitsByPluralizedNames(IPluralizer pluralizer) =>
            WeightUnit
            .GetPredefinedUnits()
            .ToDictionary(e => pluralizer.Pluralize(e.Name));
    }
}