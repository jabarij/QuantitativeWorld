#if DECIMAL
namespace DecimalQuantitativeWorld
{
    using DecimalQuantitativeWorld.Interfaces;
    using number = System.Decimal;
#else
namespace QuantitativeWorld
{
    using QuantitativeWorld.Interfaces;
    using number = System.Double;
#endif

    public class AreaFactory : ILinearQuantityFactory<Area, AreaUnit>
    {
        public Area Create(number value, AreaUnit unit) =>
            new Area(value, unit);
    }
}