using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Area : ILinearQuantity<AreaUnit>
    {
        public static readonly Weight MinValue = new Weight(MinSquareMetres);
        public static readonly Weight MaxValue = new Weight(MaxSquareMetres);
        private const double MinSquareMetres = double.MinValue;
        private const double MaxSquareMetres = double.MaxValue;

        public static readonly AreaUnit DefaultUnit = AreaUnit.SquareMetre;

        private readonly AreaUnit? _formatUnit;

        public Area(double metres)
            : this(formatUnit: null, metres: metres) { }
        public Area(double value, AreaUnit unit)
            : this(formatUnit: unit, metres: GetSquareMetres(value, unit)) { }
        private Area(AreaUnit? formatUnit, double metres)
        {
            Assert.IsInRange(metres, MinSquareMetres, MaxSquareMetres, nameof(metres));

            _formatUnit = formatUnit;
            SquareMetres = metres;
        }

        public double SquareMetres { get; }
        public double Value => GetValue(SquareMetres, Unit);
        public AreaUnit Unit => _formatUnit ?? DefaultUnit;
        double ILinearQuantity<AreaUnit>.BaseValue => SquareMetres;
        AreaUnit ILinearQuantity<AreaUnit>.BaseUnit => DefaultUnit;

        public Area Convert(AreaUnit targetUnit) =>
            new Area(targetUnit, SquareMetres);

        public bool IsZero() =>
            SquareMetres == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Area, AreaUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Area, AreaUnit>(formatProvider, this);

        private static double GetSquareMetres(double value, AreaUnit sourceUnit) =>
            value * sourceUnit.ValueInSquareMetres;
        private static double GetValue(double metres, AreaUnit targetUnit) =>
            metres / targetUnit.ValueInSquareMetres;
    }
}
