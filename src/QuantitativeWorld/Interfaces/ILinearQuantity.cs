namespace QuantitativeWorld.Interfaces
{
    public interface ILinearQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        decimal BaseValue { get; }
        decimal Value { get; }
        TUnit Unit { get; }
    }
}