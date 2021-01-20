namespace QuantitativeWorld.Interfaces
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public interface ILinearQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        number BaseValue { get; }
        TUnit BaseUnit { get; }
        number Value { get; }
        TUnit Unit { get; }
    }
}