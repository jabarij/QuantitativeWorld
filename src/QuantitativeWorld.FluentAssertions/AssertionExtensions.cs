using FluentAssertions.Numeric;

namespace QuantitativeWorld.FluentAssertions
{
    public static class AssertionExtensions
    {
        public static NumericAssertions<Length> Should(this Length actualValue) =>
            new NumericAssertions<Length>(actualValue);
        public static NullableNumericAssertions<Length> Should(this Length? actualValue) =>
            new NullableNumericAssertions<Length>(actualValue);
    }
}
