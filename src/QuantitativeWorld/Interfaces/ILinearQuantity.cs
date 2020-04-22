namespace QuantitativeWorld.Interfaces
{
    public interface ILinearQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        double BaseValue { get; }
        TUnit BaseUnit { get; }
        double Value { get; }
        TUnit Unit { get; }
    }
}