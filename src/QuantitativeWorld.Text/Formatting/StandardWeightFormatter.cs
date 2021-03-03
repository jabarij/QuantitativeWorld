using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting
{
#else
namespace QuantitativeWorld.Text.Formatting
{
#endif
    class StandardWeightFormatter : FormatterBase<Weight>
    {
        public override bool TryFormat(string format, Weight weight, IFormatProvider formatProvider, out string result)
        {
            bool isSuccess = TryGetConverter(format, out var converter);
            result =
                isSuccess
                ? Format(weight, formatProvider, converter)
                : null;
            return isSuccess;
        }

        private string Format(Weight weight, IFormatProvider formatProvider, Converter<Weight, Weight> converter)
        {
            var formattedWeight = converter(weight);
            string valueStr = formattedWeight.Value.ToString("0.########", formatProvider);
            string baseUnitStr = formattedWeight.Unit.ToString();
            return string.Concat(valueStr, " ", baseUnitStr);
        }

        private bool TryGetConverter(string format, out Converter<Weight, Weight> converter)
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

        private Weight Identity(Weight weight) => weight;

        private Converter<Weight, Weight> GetConverterForUnitSystem(UnitSystem unitSystem) =>
             e => ConvertToUnitSystem(e, unitSystem);
        private Weight ConvertToUnitSystem(Weight weight, UnitSystem unitSystem) =>
            weight.Convert(unitSystem.BaseWeightUnit);

        private Converter<Weight, Weight> GetConverterForUnknownFormat(string format) =>
            _ => throw GetUnknownFormatException(format);

        protected internal override FormatException GetUnknownFormatException(string format) =>
            new FormatException($"Unknown standard weight format '{format}'.");
    }
}
