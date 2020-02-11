using System;

namespace QuantitativeWorld.Text.Formatting
{
    class StandardLengthFormatter : FormatterBase<Length>
    {
        public override bool TryFormat(string format, Length length, IFormatProvider formatProvider, out string result)
        {
            bool isSuccess = TryGetConverter(format, out var converter);
            result =
                isSuccess
                ? Format(length, formatProvider, converter)
                : null;
            return isSuccess;
        }

        private string Format(Length length, IFormatProvider formatProvider, Converter<Length, Length> converter)
        {
            var formattedLength = converter(length);
            string valueStr = formattedLength.Value.ToString("0.########", formatProvider);
            string baseUnitStr = formattedLength.Unit.ToString();
            return string.Concat(valueStr, " ", baseUnitStr);
        }

        private bool TryGetConverter(string format, out Converter<Length, Length> converter)
        {
            if (string.IsNullOrEmpty(format))
            {
                converter = Identity;
                return true;
            }

            converter =
                StandardFormats.TryGetUnitSystemByStandardFormat(format, out var unitSystem)
                ? GetConverterForUnitSystem(unitSystem)
                : GetConverterForUnknownFormat(format);
            return unitSystem != null;
        }

        private Length Identity(Length length) => length;

        private Converter<Length, Length> GetConverterForUnitSystem(UnitSystem unitSystem) =>
             e => ConvertToUnitSystem(e, unitSystem);
        private Length ConvertToUnitSystem(Length length, UnitSystem unitSystem) =>
            length.Convert(unitSystem.BaseLengthUnit);

        private Converter<Length, Length> GetConverterForUnknownFormat(string format) =>
            _ => throw GetUnknownFormatException(format);

        protected internal override FormatException GetUnknownFormatException(string format) =>
            new FormatException($"Unknown standard length format '{format}'.");
    }
}
