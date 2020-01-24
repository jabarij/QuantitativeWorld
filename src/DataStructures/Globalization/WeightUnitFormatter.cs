using Plant.QAM.BusinessLogic.PublishedLanguage;
using System;

namespace DataStructures.Globalization
{
    public class WeightUnitFormatter : IFormatProvider, ICustomFormatter
    {
        internal const string DefaultFormat = "s";

        public string Format(string format, WeightUnit weightUnit)
        {
            Assert.IsNotNullOrWhiteSpace(format, nameof(format));
            switch (format)
            {
                case "s":
                case "S":
                    return weightUnit.Abbreviation;
                case "f":
                case "F":
                    return weightUnit.Name;
                default:
                    throw new FormatException($"Unknown weight unit format '{format}'.");
            }
        }

        object IFormatProvider.GetFormat(Type formatType) =>
            formatType == GetType() ? this : null;

        string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider) =>
            Format(format, (WeightUnit)arg);
    }
}
