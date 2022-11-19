#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
    using DecimalQuantitativeWorld.Interfaces;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
    using QuantitativeWorld.Interfaces;
    using number = System.Double;
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