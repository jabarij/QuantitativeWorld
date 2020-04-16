namespace QuantitativeWorld.Interfaces
{
    public interface IAngularQuantity<TUnit>
        where TUnit : IAngularUnit
    {
        decimal Turns { get; }
        decimal Value { get; }
        TUnit Unit { get; }
    }
}