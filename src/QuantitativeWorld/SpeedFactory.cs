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

    public class SpeedFactory : ILinearQuantityFactory<Speed, SpeedUnit>
    {
        public Speed Create(number value, SpeedUnit unit) =>
            new Speed(value, unit);
    }
}