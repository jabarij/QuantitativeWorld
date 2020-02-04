using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.BasicImplementations
{
    public class LengthFactory : IQuantityFactory<Length, LengthUnit>
    {
        public Length Create(decimal value, LengthUnit unit) =>
            new Length(value, unit);
    }
}