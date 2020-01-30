using System;

namespace DataStructures.Globalization
{
    class StandardWeightUnitFormatter : FormatterBase<WeightUnit>
    {
        internal const string DefaultFormat = "s";

        private readonly IPluralizer _standardPluralizer = new EnglishPluralizer();

        public override bool TryFormat(string format, WeightUnit weightUnit, IFormatProvider formatProvider, out string result)
        {
            bool isSuccess = TryGetFormatter(format, out var formatter);
            result =
                isSuccess
                ? formatter(weightUnit, formatProvider)
                : null;
            return isSuccess;
        }

        private bool TryGetFormatter(string format, out Func<WeightUnit, IFormatProvider, string> formatter)
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

        private string FormatToAbbreviation(WeightUnit weightUnit, IFormatProvider formatProvider) => weightUnit.Abbreviation;
        private string FormatToName(WeightUnit weightUnit, IFormatProvider formatProvider) => weightUnit.Name;
        private string FormatToPluralizedName(WeightUnit weightUnit, IFormatProvider formatProvider) => Pluralize(weightUnit.Name, formatProvider);

        private Func<WeightUnit, IFormatProvider, string> GetFormatterForUnknownFormat(string format) =>
            (_, __) => throw GetUnknownFormatException(format);

        private string Pluralize(string name, IFormatProvider formatProvider)
        {
            var pluralizer =
                formatProvider?.GetFormat<IPluralizer>()
                ?? _standardPluralizer;
            return pluralizer.Pluralize(name);
        }

        protected internal override FormatException GetUnknownFormatException(string format) =>
            new FormatException($"Unknown standard weight unit format '{format}'.");
    }
}
