namespace QuantitativeWorld.Interfaces
{
    public interface IUnitConverter<TUnit>
    {
        double ConvertValue(double value, TUnit sourceUnit, TUnit targetUnit);
    }
}