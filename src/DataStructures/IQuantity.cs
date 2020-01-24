namespace DataStructures
{
    public interface IQuantity<TUnit>
        where TUnit : IUnit
    {
        decimal Value { get; }
        TUnit Unit { get; }
    }
}