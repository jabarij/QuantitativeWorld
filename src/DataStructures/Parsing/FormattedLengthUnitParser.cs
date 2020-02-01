﻿using QuantitativeWorld.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Parsing
{
    internal class FormattedLengthUnitParser : IFormattedParser<LengthUnit>
    {
        private readonly Dictionary<string, LengthUnit> _unitsByNames;
        private readonly Dictionary<string, LengthUnit> _unitsByAbbreviations;
        private readonly Dictionary<string, LengthUnit> _unitsByPluralizedNames;

        public FormattedLengthUnitParser()
            : this(new EnglishUnitsPluralizer()) { }
        public FormattedLengthUnitParser(IPluralizer pluralizer)
            : this(
                  unitsByNames: GetParsableUnitsByNames(),
                  unitsByAbbreviations: GetParsableUnitsByAbbreviations(),
                  unitsByPluralizedNames: GetParsableUnitsByPluralizedNames(pluralizer ?? throw new ArgumentNullException(nameof(pluralizer))))
        { }
        private FormattedLengthUnitParser(
            Dictionary<string, LengthUnit> unitsByNames,
            Dictionary<string, LengthUnit> unitsByAbbreviations,
            Dictionary<string, LengthUnit> unitsByPluralizedNames)
        {
            _unitsByNames = unitsByNames ?? throw new ArgumentNullException(nameof(unitsByNames));
            _unitsByAbbreviations = unitsByAbbreviations ?? throw new ArgumentNullException(nameof(unitsByAbbreviations));
            _unitsByPluralizedNames = unitsByPluralizedNames ?? throw new ArgumentNullException(nameof(unitsByPluralizedNames));
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
            : throw new InvalidOperationException($"Could not parse length unit '{value}' using format '{format}'.");
        private LengthUnit ParseFromName(string value, string format, IFormatProvider formatProvider) =>
            _unitsByNames.TryGetValue(value, out var lengthUnit)
            ? lengthUnit
            : throw new InvalidOperationException($"Could not parse length unit '{value}' using format '{format}'.");
        private LengthUnit ParseFromPluralizedName(string value, string format, IFormatProvider formatProvider) =>
            _unitsByPluralizedNames.TryGetValue(value, out var lengthUnit)
            ? lengthUnit
            : throw new InvalidOperationException($"Could not parse length unit '{value}' using format '{format}'.");

        private Func<string, string, IFormatProvider, LengthUnit> GetFormatterForUnknownFormat(string format) =>
            (_, __, ___) => throw GetUnknownFormatException(format);

        private FormatException GetUnknownFormatException(string format) =>
            new FormatException($"Unknown standard length unit format '{format}'.");

        private static Dictionary<string, LengthUnit> GetParsableUnitsByNames() =>
            LengthUnit
            .GetParsableUnits()
            .ToDictionary(e => e.Name);

        private static Dictionary<string, LengthUnit> GetParsableUnitsByAbbreviations() =>
            LengthUnit
            .GetParsableUnits()
            .ToDictionary(e => e.Abbreviation);

        private static Dictionary<string, LengthUnit> GetParsableUnitsByPluralizedNames(IPluralizer pluralizer) =>
            LengthUnit
            .GetParsableUnits()
            .ToDictionary(e => pluralizer.Pluralize(e.Name));
    }
}