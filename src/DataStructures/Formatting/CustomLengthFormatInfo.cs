using System;

namespace QuantitativeWorld.Formatting
{
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