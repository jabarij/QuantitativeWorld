#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
    using DecimalQuantitativeWorld.Interfaces;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Angular
{
    using QuantitativeWorld.Interfaces;
    using number = System.Double;
#endif

    public class AngleFactory : ILinearQuantityFactory<Angle, AngleUnit>
    {
        public Angle Create(number value, AngleUnit unit) =>
            new Angle(value, unit);
    }
}