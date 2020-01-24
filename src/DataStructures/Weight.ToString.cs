using DataStructures.Globalization;
using System;

namespace DataStructures
{
    partial struct Weight
    {
        public override string ToString() =>
            ToString(WeightFormatter.DefaultFormat);
        public string ToString(string format) =>
            ToString(format, new WeightFormatter());
        public string ToString(IFormatProvider formatProvider) =>
            ToString(WeightFormatter.DefaultFormat, formatProvider);
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var formatter =
                (ICustomFormatter)formatProvider.GetFormat(typeof(ICustomFormatter))
                ?? new WeightFormatter();
            return formatter.Format(format, this, formatProvider);
        }
    }
}
