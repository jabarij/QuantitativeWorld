using QuantitativeWorld.DotNetExtensions;
using System;
using System.Globalization;

namespace QuantitativeWorld.Formatting
{
    public class LengthFormatter : FormatterBase<Length>, ICustomFormatter
    {
        private readonly StandardLengthFormatter _standardFormatter = new StandardLengthFormatter();
        private readonly CustomLengthFormatter _customFormatter = new CustomLengthFormatter();
        private readonly LengthUnitFormatter _lengthUnitFormatter = new LengthUnitFormatter();
        private readonly CultureInfo _cultureInfo;

        public LengthFormatter()
            : this(CultureInfo.CurrentCulture) { }
        public LengthFormatter(CultureInfo cultureInfo)
        {
            Assert.IsNotNull(cultureInfo, nameof(cultureInfo));

            _cultureInfo = cultureInfo;
        }

        public override bool TryFormat(string format, Length length, IFormatProvider formatProvider, out string result) =>
            _standardFormatter.TryFormat(format, length, formatProvider, out result)
            || _customFormatter.TryFormat(format, length, formatProvider, out result);

        protected override object GetFormatOrNull(Type formatType)
        {
            if (formatType == typeof(CultureInfo))
                return _cultureInfo;

            if (formatType == typeof(NumberFormatInfo))
                return _cultureInfo.NumberFormat;

            if (formatType == typeof(ICustomFormatter<LengthUnit>))
                return _lengthUnitFormatter;

            return base.GetFormatOrNull(formatType);
        }
    }
}
