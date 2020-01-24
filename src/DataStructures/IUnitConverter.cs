namespace DataStructures
{
    public interface IUnitConverter<TQuantity, TUnit>
        where TQuantity : IQuantity<TUnit>
        where TUnit: IUnit
    {
        TQuantity Convert(TQuantity quantity, TUnit targetUnit);
    }
}