namespace DataStructures
{
    public interface IQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        decimal Value { get; }
        TUnit Unit { get; }
    }
}