using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Text.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Text.Parsing
{
    internal class FormattedLengthUnitParser : IFormattedParser<LengthUnit>
    {
        private readonly Dictionary<string, LengthUnit> _unitsByNames;
        private readonly Dictionary<string, LengthUnit> _unitsByAbbreviations;
        private readonly Dictionary<string, LengthUnit> _unitsByPluralizedNames;

        public FormattedLengthUnitParser()
            : this(new DictionaryPluralizer(
                irregularPlurals: new Dictionary<string, string>
                {
                    { "foot", "feet" }
                }))
        { }
        public FormattedLengthUnitParser(IPluralizer pluralizer)
            : this(
                  unitsByNames: GetParsableUnitsByNames(),
                  unitsByAbbreviations: GetParsableUnitsByAbbreviations(),
                  unitsByPluralizedNames: GetParsableUnitsByPluralizedNames(pluralizer))
        { }
        private FormattedLengthUnitParser(
            Dictionary<string, LengthUnit> unitsByNames,
            Dictionary<string, LengthUnit> unitsByAbbreviations,
            Dictionary<string, LengthUnit> unitsByPluralizedNames)
        {
            _unitsByNames = unitsByNames;
            _unitsByAbbreviations = unitsByAbbreviations;
            _unitsByPluralizedNames = unitsByPluralizedNames;
        }

        public LengthUnit ParseExact(string value, string format, IFormatProvider formatProvider) =>
            TryGetParser(format, out var parser)
            ? parser(value, format, formatProvider)
            : throw GetUnknownFormatException(format);

        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out LengthUnit result)
        {
            bool isSuccess = TryGetParser(format, out var parser);
            result =
                isSuccess
                ? parser(value, format, formatProvider)
                : default(LengthUnit);
            return isSuccess;
        }

        private bool TryGetParser(string format, out Func<string, string, IFormatProvider, LengthUnit> parser)
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

        private LengthUnit ParseFromAbbreviation(string value, string format, IFormatProvider formatProvider) =>
            _unitsByAbbreviations.TryGetValue(value, out var lengthUnit)
            ? lengthUnit
            : throw Error.IncorrectFormat($"Could not parse length unit '{value}' using format '{format}'.");
        private LengthUnit ParseFromName(string value, string format, IFormatProvider formatProvider) =>
            _unitsByNames.TryGetValue(value, out var lengthUnit)
            ? lengthUnit
            : throw Error.IncorrectFormat($"Could not parse length unit '{value}' using format '{format}'.");
        private LengthUnit ParseFromPluralizedName(string value, string format, IFormatProvider formatProvider) =>
            _unitsByPluralizedNames.TryGetValue(value, out var lengthUnit)
            ? lengthUnit
            : throw Error.IncorrectFormat($"Could not parse length unit '{value}' using format '{format}'.");

        private Func<string, string, IFormatProvider, LengthUnit> GetFormatterForUnknownFormat(string format) =>
            (_, __, ___) => throw GetUnknownFormatException(format);

        private FormatException GetUnknownFormatException(string format) =>
            throw Error.IncorrectFormat($"Unknown standard length unit format '{format}'.");

        private static Dictionary<string, LengthUnit> GetParsableUnitsByNames() =>
            LengthUnit
            .GetPredefinedUnits()
            .ToDictionary(e => e.Name);

        private static Dictionary<string, LengthUnit> GetParsableUnitsByAbbreviations() =>
            LengthUnit
            .GetPredefinedUnits()
            .ToDictionary(e => e.Abbreviation);

        private static Dictionary<string, LengthUnit> GetParsableUnitsByPluralizedNames(IPluralizer pluralizer) =>
            LengthUnit
            .GetPredefinedUnits()
            .ToDictionary(e => pluralizer.Pluralize(e.Name));
    }
}