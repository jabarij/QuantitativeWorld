namespace DataStructures
{
    public class UnitConverter :
        IUnitConverter<Weight, WeightUnit>,
        IUnitConverter<Length, LengthUnit>
    {
        public Weight Convert(Weight weight, WeightUnit targetUnit) =>
            ConvertWeight(weight, targetUnit);
        public Length Convert(Length length, LengthUnit targetUnit) =>
            ConvertLength(length, targetUnit);

        internal static Weight ConvertWeight(Weight weight, WeightUnit targetUnit) =>
            new Weight(
                value: GetValue(weight, targetUnit),
                unit: targetUnit);
        internal static Length ConvertLength(Length length, LengthUnit targetUnit) =>
            new Length(
                value: GetValue(length, targetUnit),
                unit: targetUnit);

        internal static decimal GetValue<TQuantity, TUnit>(TQuantity quantity, TUnit targetUnit)
            where TQuantity : IQuantity<TUnit>
            where TUnit : IUnit =>
            quantity.Value * GetFactor(quantity.Unit, targetUnit);

        internal static decimal GetFactor<TUnit>(TUnit sourceUnit, TUnit targetUnit)
            where TUnit : IUnit =>
            sourceUnit.ValueInBaseUnit / targetUnit.ValueInBaseUnit;
    }
}