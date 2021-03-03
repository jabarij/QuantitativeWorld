using System;
using System.Globalization;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting
{
#else
namespace QuantitativeWorld.Text.Formatting
{
#endif
    public class WeightFormatter : FormatterBase<Weight>, ICustomFormatter
    {
        private readonly StandardWeightFormatter _standardFormatter = new StandardWeightFormatter();
        private readonly CustomWeightFormatter _customFormatter = new CustomWeightFormatter();
        private readonly WeightUnitFormatter _weightUnitFormatter = new WeightUnitFormatter();
        private readonly CultureInfo _cultureInfo;

        public WeightFormatter()
            : this(CultureInfo.CurrentCulture) { }
        public WeightFormatter(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo ?? throw new ArgumentNullException(nameof(cultureInfo));
        }

        public override bool TryFormat(string format, Weight weight, IFormatProvider formatProvider, out string result) =>
            _standardFormatter.TryFormat(format, weight, formatProvider, out result)
            || _customFormatter.TryFormat(format, weight, formatProvider, out result);

        protected override object GetFormatOrNull(Type formatType)
        {
            if (formatType == typeof(CultureInfo))
                return _cultureInfo;

            if (formatType == typeof(NumberFormatInfo))
                return _cultureInfo.NumberFormat;

            if (formatType == typeof(ICustomFormatter<WeightUnit>))
                return _weightUnitFormatter;

            return base.GetFormatOrNull(formatType);
        }
    }
}
