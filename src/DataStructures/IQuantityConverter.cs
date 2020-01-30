namespace DataStructures
{
    public interface IQuantityConverter<TQuantity, TUnit>
        where TUnit : ILinearUnit
    {
        TQuantity Convert(TQuantity quantity, TUnit targetUnit);
    }
}