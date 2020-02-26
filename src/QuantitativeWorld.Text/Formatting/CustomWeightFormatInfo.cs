using System;

namespace QuantitativeWorld.Text.Formatting
{
    internal class CustomWeightFormatInfo
    {
        public CustomWeightFormatInfo(
            Converter<Weight, Weight> weightConverter,
            string valueFormat,
            string unitFormat)
        {
            WeightConverter = weightConverter;
            ValueFormat = valueFormat;
            UnitFormat = unitFormat;
        }

        public Converter<Weight,Weight> WeightConverter { get; }
        public string ValueFormat { get; }
        public string UnitFormat { get; }
    }
}