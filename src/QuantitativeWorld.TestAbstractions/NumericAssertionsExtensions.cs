using FluentAssertions;
using FluentAssertions.Numeric;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.TestAbstractions
{
#else
namespace QuantitativeWorld.TestAbstractions
{
#endif
    public static class NumericAssertionsExtensions
    {
        public static AndConstraint<NumericAssertions<decimal>> Be(this NullableNumericAssertions<decimal> parent, double? expectedValue, string because = "", params object[] becauseArgs) =>
            parent.Be((decimal?)expectedValue, because, becauseArgs);
        public static AndConstraint<NumericAssertions<decimal>> Be(this NumericAssertions<decimal> parent, double expectedValue, string because = "", params object[] becauseArgs) =>
            parent.Be((decimal)expectedValue, because, becauseArgs);

        public static AndConstraint<NumericAssertions<double>> Be(this NullableNumericAssertions<double> parent, decimal? expectedValue, string because = "", params object[] becauseArgs) =>
            parent.Be((double?)expectedValue, because, becauseArgs);
        public static AndConstraint<NumericAssertions<double>> Be(this NumericAssertions<double> parent, decimal expectedValue, string because = "", params object[] becauseArgs) =>
            parent.Be((double)expectedValue, because, becauseArgs);

        public static AndConstraint<NullableNumericAssertions<decimal>> BeApproximately(this NullableNumericAssertions<decimal> parent, decimal? expectedValue, string because = "", params object[] becauseArgs) =>
            parent.BeApproximately(expectedValue, Math.Abs(expectedValue * TestsBase.DecimalPrecisionPercentage ?? TestsBase.DecimalPrecision), because, becauseArgs);
        public static AndConstraint<NumericAssertions<decimal>> BeApproximately(this NumericAssertions<decimal> parent, decimal expectedValue, string because = "", params object[] becauseArgs) =>
            parent.BeApproximately(expectedValue, Math.Abs(expectedValue * TestsBase.DecimalPrecisionPercentage), because, becauseArgs);

        public static AndConstraint<NullableNumericAssertions<decimal>> BeApproximately(this NullableNumericAssertions<decimal> parent, double? expectedValue, string because = "", params object[] becauseArgs) =>
            parent.BeApproximately((decimal?)expectedValue, Math.Abs((decimal?)expectedValue * TestsBase.DecimalPrecisionPercentage ?? TestsBase.DecimalPrecision), because, becauseArgs);
        public static AndConstraint<NumericAssertions<decimal>> BeApproximately(this NumericAssertions<decimal> parent, double expectedValue, string because = "", params object[] becauseArgs) =>
            parent.BeApproximately((decimal)expectedValue, Math.Abs((decimal)expectedValue * TestsBase.DecimalPrecisionPercentage), because, becauseArgs);

        public static AndConstraint<NullableNumericAssertions<double>> BeApproximately(this NullableNumericAssertions<double> parent, double? expectedValue, string because = "", params object[] becauseArgs) =>
            parent.BeApproximately(expectedValue, Math.Abs(expectedValue * TestsBase.DoublePrecisionPercentage ?? TestsBase.DoublePrecision), because, becauseArgs);
        public static AndConstraint<NumericAssertions<double>> BeApproximately(this NumericAssertions<double> parent, double expectedValue, string because = "", params object[] becauseArgs) =>
            parent.BeApproximately(expectedValue, Math.Abs(expectedValue * TestsBase.DoublePrecisionPercentage), because, becauseArgs);

        public static AndConstraint<NullableNumericAssertions<double>> BeApproximately(this NullableNumericAssertions<double> parent, decimal? expectedValue, string because = "", params object[] becauseArgs) =>
            parent.BeApproximately((double?)expectedValue, Math.Abs((double?)expectedValue * TestsBase.DoublePrecisionPercentage ?? TestsBase.DoublePrecision), because, becauseArgs);
        public static AndConstraint<NumericAssertions<double>> BeApproximately(this NumericAssertions<double> parent, decimal expectedValue, string because = "", params object[] becauseArgs) =>
            parent.BeApproximately((double)expectedValue, Math.Abs((double)expectedValue * TestsBase.DoublePrecisionPercentage), because, becauseArgs);
    }
}
