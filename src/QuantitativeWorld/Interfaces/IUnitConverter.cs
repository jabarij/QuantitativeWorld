namespace QuantitativeWorld.Interfaces
{
    public interface IUnitConverter<TUnit>
    {
        decimal ConvertValue(decimal value, TUnit sourceUnit, TUnit targetUnit);
    }
}