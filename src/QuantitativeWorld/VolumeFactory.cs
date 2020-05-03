using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public class VolumeFactory : ILinearQuantityFactory<Volume, VolumeUnit>
    {
        public Volume Create(double value, VolumeUnit unit) =>
            new Volume(value, unit);
    }
}