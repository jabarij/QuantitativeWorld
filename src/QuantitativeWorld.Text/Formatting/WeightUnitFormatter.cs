using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting
{
#else
namespace QuantitativeWorld.Text.Formatting
{
#endif
    public class WeightUnitFormatter : FormatterBase<WeightUnit>
    {
        private readonly StandardWeightUnitFormatter _standardFormatter = new StandardWeightUnitFormatter();

        public override bool TryFormat(string format, WeightUnit weightUnit, IFormatProvider formatProvider, out string result) =>
            _standardFormatter.TryFormat(format, weightUnit, formatProvider, out result);

        protected internal override FormatException GetUnknownFormatException(string unknownFormat) =>
            new FormatException($"Unknown weight unit format '{unknownFormat}'. See inner exception for more details.", _standardFormatter.GetUnknownFormatException(unknownFormat));
    }
}
