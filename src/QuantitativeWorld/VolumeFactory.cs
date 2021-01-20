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

    public class VolumeFactory : ILinearQuantityFactory<Volume, VolumeUnit>
    {
        public Volume Create(number value, VolumeUnit unit) =>
            new Volume(value, unit);
    }
}