namespace QuantitativeWorld.Interfaces
{
    public interface IAngularQuantity<TUnit>
        where TUnit : IAngularUnit
    {
        double Turns { get; }
        double Value { get; }
        TUnit Unit { get; }
    }
}