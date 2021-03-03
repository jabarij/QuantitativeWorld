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

    public class VolumeFactory : ILinearQuantityFactory<Volume, VolumeUnit>
    {
        public Volume Create(number value, VolumeUnit unit) =>
            new Volume(value, unit);
    }
}