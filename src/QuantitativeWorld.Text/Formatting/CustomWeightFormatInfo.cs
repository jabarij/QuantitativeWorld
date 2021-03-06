﻿using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting
{
#else
namespace QuantitativeWorld.Text.Formatting
{
#endif
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