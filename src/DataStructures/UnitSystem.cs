using Plant.QAM.BusinessLogic.PublishedLanguage;
using System;

namespace QuantitativeWorld
{
    public class UnitSystem
    {
        public static UnitSystem CGS => new UnitSystem(
            id: "CGS",
            baseWeightUnit: WeightUnit.Gram,
            baseLengthUnit: LengthUnit.Centimetre);
        public static UnitSystem IMPERIAL => new UnitSystem(
            id: "IMPERIAL",
            baseWeightUnit: WeightUnit.Pound,
            baseLengthUnit: LengthUnit.Foot);
        public static UnitSystem MKS => new UnitSystem(
            id: "MKS",
            baseWeightUnit: WeightUnit.Kilogram,
            baseLengthUnit: LengthUnit.Metre);
        public static UnitSystem MTS => new UnitSystem(
            id: "MTS",
            baseWeightUnit: WeightUnit.Ton,
            baseLengthUnit: LengthUnit.Metre);
        public static UnitSystem SI => new UnitSystem(
            id: "SI",
            baseWeightUnit: WeightUnit.Kilogram,
            baseLengthUnit: LengthUnit.Metre);

        public UnitSystem(
            string id,
            WeightUnit baseWeightUnit,
            LengthUnit baseLengthUnit)
        {
            ID = Assert.IsNotNullOrWhiteSpace(id, nameof(id));
            BaseWeightUnit = baseWeightUnit;
            BaseLengthUnit = baseLengthUnit;
        }

        public string ID { get; }
        public WeightUnit BaseWeightUnit { get; }
        public LengthUnit BaseLengthUnit { get; }

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