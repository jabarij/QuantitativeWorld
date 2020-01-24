using DataStructures.Globalization;
using Plant.QAM.BusinessLogic.PublishedLanguage;
using System;

namespace DataStructures
{
    public partial struct WeightUnit : IUnit, IFormattable
    {
        public WeightUnit(string name, string abbreviation, decimal valueInKilograms)
        {
            Name = Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Abbreviation = Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            ValueInKilograms = Assert.IsGreaterThan(valueInKilograms, 0m, nameof(valueInKilograms));
        }

        public string Name { get; }
        public string Abbreviation { get; }
        public decimal ValueInKilograms { get; }

        decimal IUnit.ValueInBaseUnit => ValueInKilograms;

        public override string ToString() =>
            ToString(WeightUnitFormatter.DefaultFormat);
        public string ToString(string format) =>
            ToString(format, new WeightUnitFormatter());
        public string ToString(IFormatProvider formatProvider) =>
            ToString(WeightUnitFormatter.DefaultFormat, formatProvider);
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var formatter =
                (ICustomFormatter)formatProvider.GetFormat(typeof(ICustomFormatter))
                ?? new WeightUnitFormatter();
            return formatter.Format(format, this, formatProvider);
        }
    }
}