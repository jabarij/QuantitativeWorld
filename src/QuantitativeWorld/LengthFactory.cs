using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public class LengthFactory : ILinearQuantityFactory<Length, LengthUnit>
    {
        public Length Create(number value, LengthUnit unit) =>
            new Length(value, unit);
    }
}