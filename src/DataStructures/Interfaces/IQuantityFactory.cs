namespace QuantitativeWorld.Interfaces
{
    public interface IQuantityFactory<TQuantity, TUnit>
        where TQuantity : IQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        TQuantity Create(decimal value, TUnit unit);
    }
}