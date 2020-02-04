using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Globalization;
using System;
using System.Collections.Generic;

namespace QuantitativeWorld.Formatting
{
    class StandardLengthUnitFormatter : FormatterBase<LengthUnit>
    {
        internal const string DefaultFormat = "s";

        private readonly IPluralizer _standardPluralizer;

        public StandardLengthUnitFormatter()
            : this(new DictionaryPluralizer(
                irregularPlurals: new Dictionary<string, string>
                {
                    { "foot", "feet" }
                }))
        { }
        public StandardLengthUnitFormatter(IPluralizer pluralizer)
        {
            Assert.IsNotNull(pluralizer, nameof(pluralizer));
            _standardPluralizer = pluralizer;
        }

        public override bool TryFormat(string format, LengthUnit lengthUnit, IFormatProvider formatProvider, out string result)
        {
            bool isSuccess = TryGetFormatter(format, out var formatter);
            result =
                isSuccess
                ? formatter(lengthUnit, formatProvider)
                : null;
            return isSuccess;
        }

        private bool TryGetFormatter(string format, out Func<LengthUnit, IFormatProvider, string> formatter)
        {
            if (string.IsNullOrEmpty(format))
                format = DefaultFormat;

            switch (format)
            {
                case "s":
                    formatter = FormatToAbbreviation;
                    return true;
                case "l":
                    formatter = FormatToName;
                    return true;
                case "ll":
                    formatter = FormatToPluralizedName;
                    return true;
                default:
                    formatter = GetFormatterForUnknownFormat(format);
                    return false;
            }
        }

        private string FormatToAbbreviation(LengthUnit lengthUnit, IFormatProvider formatProvider) => lengthUnit.Abbreviation;
        private string FormatToName(LengthUnit lengthUnit, IFormatProvider formatProvider) => lengthUnit.Name;
        private string FormatToPluralizedName(LengthUnit lengthUnit, IFormatProvider formatProvider) => Pluralize(lengthUnit.Name, formatProvider);

        private Func<LengthUnit, IFormatProvider, string> GetFormatterForUnknownFormat(string format) =>
            (_, __) => throw GetUnknownFormatException(format);

        private string Pluralize(string name, IFormatProvider formatProvider)
        {
            var pluralizer =
                formatProvider?.GetFormat<IPluralizer>()
                ?? _standardPluralizer;
            return pluralizer.Pluralize(name);
        }

        protected internal override FormatException GetUnknownFormatException(string format) =>
            new FormatException($"Unknown standard length unit format '{format}'.");
    }
}
