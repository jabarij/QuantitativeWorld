using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting
{
#else
namespace QuantitativeWorld.Text.Formatting
{
#endif
    internal class CustomLengthFormatInfo
    {
        public CustomLengthFormatInfo(
            Converter<Length, Length> lengthConverter,
            string valueFormat,
            string unitFormat)
        {
            LengthConverter = lengthConverter;
            ValueFormat = valueFormat;
            UnitFormat = unitFormat;
        }

        public Converter<Length,Length> LengthConverter { get; }
        public string ValueFormat { get; }
        public string UnitFormat { get; }
    }
}