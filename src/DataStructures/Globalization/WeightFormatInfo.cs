using Plant.QAM.BusinessLogic.PublishedLanguage;
using System;
using System.Globalization;

namespace DataStructures.Globalization
{
    public class WeightFormatInfo
    {
        public WeightFormatInfo(
            UnitSystem unitSystem,
            IDecimalPrecisionInfo decimalPrecision,
            CultureInfo culture)
        {
            UnitSystem = unitSystem ?? throw new ArgumentNullException(nameof(unitSystem));
            DecimalPrecision = decimalPrecision ?? throw new ArgumentNullException(nameof(decimalPrecision));
            Culture = culture ?? throw new ArgumentNullException(nameof(culture));
        }

        public UnitSystem UnitSystem { get; }
        public IDecimalPrecisionInfo DecimalPrecision { get; }
        public CultureInfo Culture { get; }
        public IFormatProvider ValueFormatProvider => Culture;

        public static WeightFormatInfo CurrentInfo =>
            GetInstance(CultureInfo.CurrentCulture);

        public static WeightFormatInfo GetInstance(IFormatProvider formatProvider)
        {
            Assert.IsNotNull(formatProvider, nameof(formatProvider));

            var weightFormatInfo = (WeightFormatInfo)formatProvider.GetFormat(typeof(WeightFormatInfo));
            if (weightFormatInfo != null)
                return weightFormatInfo;

            var unitSystem = UnitSystem.GetInstance(formatProvider);
            var decimalPrecision = DecimalPrecisionInfo.GetInstance(formatProvider);
            var culture =
                (CultureInfo)formatProvider.GetFormat(typeof(CultureInfo))
                ?? formatProvider as CultureInfo
                ?? CultureInfo.CurrentCulture;
            return new WeightFormatInfo(
                unitSystem: unitSystem,
                decimalPrecision: decimalPrecision,
                culture: culture);
        }
    }
}