namespace QuantitativeWorld.Interfaces
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public interface IAngularQuantity<TUnit>
        where TUnit : IAngularUnit
    {
        number Turns { get; }
        number Value { get; }
        TUnit Unit { get; }
    }
}