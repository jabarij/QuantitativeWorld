﻿using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting
{
#else
namespace QuantitativeWorld.Text.Formatting
{
#endif
    public class LengthUnitFormatter : FormatterBase<LengthUnit>
    {
        private readonly StandardLengthUnitFormatter _standardFormatter = new StandardLengthUnitFormatter();

        public override bool TryFormat(string format, LengthUnit lengthUnit, IFormatProvider formatProvider, out string result) =>
            _standardFormatter.TryFormat(format, lengthUnit, formatProvider, out result);

        protected internal override FormatException GetUnknownFormatException(string unknownFormat) =>
            new FormatException($"Unknown length unit format '{unknownFormat}'. See inner exception for more details.", _standardFormatter.GetUnknownFormatException(unknownFormat));
    }
}
