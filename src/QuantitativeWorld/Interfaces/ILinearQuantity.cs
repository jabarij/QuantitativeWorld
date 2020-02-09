namespace QuantitativeWorld.Interfaces
{
    public interface ILinearQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        decimal Value { get; }
        TUnit Unit { get; }
    }
}