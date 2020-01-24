using Plant.QAM.BusinessLogic.PublishedLanguage;
using System;
using System.Text;

namespace DataStructures.Globalization
{
    public class WeightFormatter : IFormatProvider, ICustomFormatter
    {
        internal const string DefaultFormat = StandardFormats.SI;

        private readonly IUnitConverter<Weight, WeightUnit> _unitConverter;

        public WeightFormatter()
            : this(new UnitConverter()) { }
        public WeightFormatter(IUnitConverter<Weight, WeightUnit> unitConverter)
        {
            _unitConverter = unitConverter ?? throw new ArgumentNullException(nameof(unitConverter));
        }

        public string Format(string format, Weight weight, WeightFormatInfo formatInfo)
        {
            Assert.IsNotNullOrWhiteSpace(format, nameof(format));
            Assert.IsNotNull(formatInfo, nameof(formatInfo));

            WeightUnit baseUnit;
            switch (format)
            {
                case StandardFormats.SI:
                    baseUnit = UnitSystem.SI.BaseWeightUnit;
                    break;
                case StandardFormats.MKS:
                    baseUnit = UnitSystem.MKS.BaseWeightUnit;
                    break;
                case StandardFormats.CGS:
                    baseUnit = UnitSystem.CGS.BaseWeightUnit;
                    break;
                case StandardFormats.MTS:
                    baseUnit = UnitSystem.MTS.BaseWeightUnit;
                    break;
                case StandardFormats.IMP:
                    baseUnit = UnitSystem.IMPERIAL.BaseWeightUnit;
                    break;
                default:
                    return FormatCustomized(weight, format, formatInfo);
            }

            var baseWeight = _unitConverter.Convert(weight, baseUnit);
            string valueStr = baseWeight.Value.ToString("G29", formatInfo.ValueFormatProvider);
            string baseUnitStr = baseWeight.Unit.ToString();
            return string.Concat(valueStr, " ", baseUnitStr);
        }

        object IFormatProvider.GetFormat(Type formatType) =>
            formatType == GetType() ? this : null;

        string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider) =>
            Format(format, (Weight)arg, WeightFormatInfo.GetInstance(formatProvider));

        internal static int ParseRepeatPattern(string format, int position, string pattern)
        {
            int index = position + pattern.Length;
            while ((index <= format.Length - pattern.Length) && (Matches(format, index, pattern.Length, pattern)))
                index += pattern.Length;
            return (index - position);
        }

        private static bool Matches(string input, int position, int length, string pattern)
        {
            for (int index = position; index < length; index++)
                if (input[index] != pattern[index - position])
                    return false;
            return true;
        }

        private string FormatCustomized(Weight weight, string format, WeightFormatInfo formatInfo)
        {
            var system = formatInfo.UnitSystem;
            var result = new StringBuilder();

            int index = 0;
            int tokenLength;

            while (index < format.Length)
            {
                bool isSingleCharFormat = true;
                string singleCharFormat = format.Substring(index, 1);
                switch (singleCharFormat)
                {
                    case "g":
                        result.Append(_unitConverter.Convert(weight, WeightUnit.Gram).Value);
                        break;
                    case "t":
                        result.Append(_unitConverter.Convert(weight, WeightUnit.Ton).Value);
                        break;
                    default:
                        isSingleCharFormat = false;
                        break;

                }

                if (isSingleCharFormat)
                {
                    tokenLength = ParseRepeatPattern(format, index, singleCharFormat);
                    index += tokenLength;
                    continue;
                }

                bool isTwoCharsFormat = true;
                string twoCharsFormat = format.Substring(index, 2);
                switch (twoCharsFormat)
                {
                    case "kg":
                        result.Append(_unitConverter.Convert(weight, WeightUnit.Kilogram).Value);
                        break;
                    case "lb":
                        result.Append(_unitConverter.Convert(weight, WeightUnit.Pound).Value);
                        break;
                    default:
                        isTwoCharsFormat = false;
                        break;
                }

                if (isTwoCharsFormat)
                {
                    tokenLength = ParseRepeatPattern(format, index, twoCharsFormat);
                    index += tokenLength;
                    continue;
                }

                result.Append(format[index]);
                tokenLength = 1;
                index += tokenLength;
            }

            return result.ToString();
        }

        public static class StandardFormats
        {
            public const string CGS = nameof(CGS);
            public const string IMP = nameof(IMP);
            public const string MKS = nameof(MKS);
            public const string MTS = nameof(MTS);
            public const string SI = nameof(SI);
        }
    }
}
