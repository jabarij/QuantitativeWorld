using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public class AngleFactory : ILinearQuantityFactory<Angle, AngleUnit>
    {
        public Angle Create(number value, AngleUnit unit) =>
            new Angle(value, unit);
    }
}