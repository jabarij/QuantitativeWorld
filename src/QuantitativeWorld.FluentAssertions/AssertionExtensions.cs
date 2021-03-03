using FluentAssertions.Numeric;

#if DECIMAL
namespace DecimalQuantitativeWorld.FluentAssertions
{
#else
namespace QuantitativeWorld.FluentAssertions
{
#endif
    public static class AssertionExtensions
    {
        public static NumericAssertions<Length> Should(this Length actualValue) =>
            new NumericAssertions<Length>(actualValue);
        public static NullableNumericAssertions<Length> Should(this Length? actualValue) =>
            new NullableNumericAssertions<Length>(actualValue);
    }
}
