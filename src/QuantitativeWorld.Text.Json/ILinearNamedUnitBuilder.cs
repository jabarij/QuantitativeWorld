using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Text.Json
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public interface ILinearNamedUnitBuilder<TUnit>
        where TUnit : ILinearUnit, INamedUnit
    {
        void SetName(string name);
        void SetAbbreviation(string abbreviation);
        void SetValueInBaseUnit(number valueInBaseUnit);
        bool TryBuild(out TUnit unit);
    }
}