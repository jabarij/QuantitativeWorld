using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    public class UnitSystem
    {
        public static UnitSystem CGS => new UnitSystem(
            id: "CGS",
            baseWeightUnit: WeightUnit.Gram,
            baseLengthUnit: LengthUnit.Centimetre,
            basePowerUnit: PowerUnit.ErgPerSecond);
        public static UnitSystem IMPERIAL => new UnitSystem(
            id: "IMPERIAL",
            baseWeightUnit: WeightUnit.Pound,
            baseLengthUnit: LengthUnit.Foot,
            basePowerUnit: PowerUnit.MechanicalHorsepower);
        public static UnitSystem MKS => new UnitSystem(
            id: "MKS",
            baseWeightUnit: WeightUnit.Kilogram,
            baseLengthUnit: LengthUnit.Metre,
            basePowerUnit: PowerUnit.MetricHorsepower);
        public static UnitSystem MTS => new UnitSystem(
            id: "MTS",
            baseWeightUnit: WeightUnit.Ton,
            baseLengthUnit: LengthUnit.Metre,
            basePowerUnit: PowerUnit.StheneMetrePerSecond);
        public static UnitSystem SI => new UnitSystem(
            id: "SI",
            baseWeightUnit: WeightUnit.Kilogram,
            baseLengthUnit: LengthUnit.Metre,
            basePowerUnit: PowerUnit.Watt);

        public UnitSystem(
            string id,
            WeightUnit baseWeightUnit,
            LengthUnit baseLengthUnit,
            PowerUnit basePowerUnit)
        {
            ID = Assert.IsNotNullOrWhiteSpace(id, nameof(id));
            BaseWeightUnit = baseWeightUnit;
            BaseLengthUnit = baseLengthUnit;
            BasePowerUnit = basePowerUnit;
        }

        public string ID { get; }
        public WeightUnit BaseWeightUnit { get; }
        public LengthUnit BaseLengthUnit { get; }
        public PowerUnit BasePowerUnit { get; }

        public static UnitSystem GetInstance(IFormatProvider formatProvider)
        {
            Assert.IsNotNull(formatProvider, nameof(formatProvider));

            var unitSystem = (UnitSystem)formatProvider.GetFormat(typeof(UnitSystem));
            if (unitSystem != null)
                return unitSystem;

            return SI;
        }
    }
}