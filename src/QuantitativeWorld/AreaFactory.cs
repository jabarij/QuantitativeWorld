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

    public class AreaFactory : ILinearQuantityFactory<Area, AreaUnit>
    {
        public Area Create(number value, AreaUnit unit) =>
            new Area(value, unit);
    }
}